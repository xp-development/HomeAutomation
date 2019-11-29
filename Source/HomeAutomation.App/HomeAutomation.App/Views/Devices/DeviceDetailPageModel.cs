using System.Threading.Tasks;
using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
using HomeAutomation.App.Models;
using HomeAutomation.App.Mvvm;
using HomeAutomation.Protocols.App.v0.Requests.Devices;
using HomeAutomation.Protocols.App.v0.Responses;
using HomeAutomation.Protocols.App.v0.Responses.Devices;

namespace HomeAutomation.App.Views.Devices
{
  public class DeviceDetailPageModel : ViewModelBase
  {
    private readonly ICommunicator _communicator;
    private readonly IEventAggregator _eventAggregator;
    private DeviceViewModel _device;

    public DeviceDetailPageModel(ICommunicator communicator, IEventAggregator eventAggregator)
    {
      _communicator = communicator;
      _eventAggregator = eventAggregator;
      SaveDeviceCommand = new DelegateCommand<object, object>(OnSaveDevice);
      DeleteDeviceCommand = new DelegateCommand<object, object>(OnDeleteDevice);
    }

    public DelegateCommand<object, object> SaveDeviceCommand { get; }
    public DelegateCommand<object, object> DeleteDeviceCommand { get; }

    public DeviceViewModel Device
    {
      get => _device;
      set
      {
        _device = value;
        InvokePropertyChanged();
      }
    }

    private Task OnSaveDevice(object arg)
    {
      return _communicator.SendAsync(new RenameDeviceDescriptionDataRequest {Identifier = Device.Id, Description = Device.Description});
    }

    private Task OnDeleteDevice(object arg)
    {
      return _communicator.SendAsync(new DeleteDeviceDataRequest {Identifier = Device.Id});
    }

    protected override Task OnLoadedAsync(object parameter)
    {
      _communicator.ReceiveData += OnReceiveData;
      return _communicator.SendAsync(new GetDeviceDescriptionDataRequest {Identifier = (int) parameter});
    }

    protected override Task OnUnloadedAsync()
    {
      _communicator.ReceiveData -= OnReceiveData;
      return base.OnUnloadedAsync();
    }

    private void OnReceiveData(IResponse response)
    {
      if (response is GetDeviceDescriptionDataResponse getDeviceDescriptionDataResponse)
        Device = new DeviceViewModel { Id = getDeviceDescriptionDataResponse.Identifier, Description = getDeviceDescriptionDataResponse.Description };
      else if (response is DeleteDeviceDataResponse deleteDeviceDataResponse && deleteDeviceDataResponse.Identifier == Device?.Id)
        _eventAggregator.PublishAsync(new NavigationEvent(typeof(DeviceOverviewPage)));
    }
  }
}