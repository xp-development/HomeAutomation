using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
using HomeAutomation.App.Models;
using HomeAutomation.Protocols.App.v0.Requests.Devices;
using HomeAutomation.Protocols.App.v0.Responses.Devices;

namespace HomeAutomation.App.Views.Devices
{
  public class DeviceOverviewPageModel
    : ObjectOverviewPageModelBase<DeviceDetailPage, DeviceViewModel, GetAllDevicesDataRequest, GetAllDevicesDataResponse, GetDeviceDescriptionDataRequest, GetDeviceDescriptionDataResponse, CreateDeviceDataRequest, CreateDeviceDataResponse>
  {
    public DeviceOverviewPageModel(ICommunicator communicator, IEventAggregator eventAggregator)
      : base(communicator, eventAggregator)
    {
      
    }

    protected override string NewObjectDescription { get; } = "New device";
  }
}