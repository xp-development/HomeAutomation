using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.App.Communication;
using HomeAutomation.App.Events;
using HomeAutomation.App.Models;
using HomeAutomation.App.Mvvm;
using HomeAutomation.Protocols.App.v0.Requests;
using HomeAutomation.Protocols.App.v0.Responses;

namespace HomeAutomation.App.Views
{
  public abstract class ObjectOverviewPageModelBase<TObjectDetailPage, TObjectViewModel, TGetAllObjectsDataRequest, TGetAllObjectsDataResponse, TGetObjectDescriptionDataRequest, TGetObjectDescriptionDataResponse, TCreateObjectDataRequest, TCreateObjectDataResponse>
    : ViewModelBase
    where TGetAllObjectsDataRequest : IRequest, new()
    where TGetAllObjectsDataResponse : GetAllObjectsDataResponseBase, new()
    where TObjectViewModel : ObjectViewModelBase, new()
    where TGetObjectDescriptionDataRequest : GetObjectDescriptionDataRequestBase, new()
    where TGetObjectDescriptionDataResponse : GetObjectDescriptionDataResponseBase, new()
    where TCreateObjectDataRequest : CreateObjectDataRequestBase, new()
    where TCreateObjectDataResponse : CreateObjectDataResponseBase, new()
  {
    private readonly ICommunicator _communicator;
    private readonly IEventAggregator _eventAggregator;
    private readonly Dictionary<byte, string> _newObjects = new Dictionary<byte, string>();
    private byte _clientObjectIdentifierForNewObjects;

    protected ObjectOverviewPageModelBase(ICommunicator communicator, IEventAggregator eventAggregator)
    {
      _communicator = communicator;
      _eventAggregator = eventAggregator;
      NewObjectCommand = new DelegateCommand<object, object>(OnNewObjectAsync);
      NavigateToObjectCommand = new DelegateCommand<object, object>(OnNavigateToObjectAsync);
    }

    protected abstract string NewObjectDescription { get; }

    public ObservableCollection<TObjectViewModel> Objects { get; } = new ObservableCollection<TObjectViewModel>();
    public DelegateCommand<object, object> NewObjectCommand { get; }
    public DelegateCommand<object, object> NavigateToObjectCommand { get; }

    private Task OnNavigateToObjectAsync(object arg)
    {
      return _eventAggregator.PublishAsync(new NavigationEvent(typeof(TObjectDetailPage), arg));
    }

    private Task OnNewObjectAsync(object arg)
    {
      var clientObjectIdentifier = ++_clientObjectIdentifierForNewObjects;
      _newObjects.Add(clientObjectIdentifier, NewObjectDescription);
      return _communicator.SendAsync(new TCreateObjectDataRequest { ClientObjectIdentifier = clientObjectIdentifier, Description = NewObjectDescription});
    }

    private async void OnReceiveData(IResponse response)
    {
      if (response is TGetAllObjectsDataResponse getAllObjectsResponse)
      {
        Objects.Clear();
        foreach (var identifier in getAllObjectsResponse.Identifiers)
        {
          Objects.Add(new TObjectViewModel {Id = identifier});
          await _communicator.SendAsync(new TGetObjectDescriptionDataRequest {Identifier = identifier});
        }
      }

      if (response is TGetObjectDescriptionDataResponse getObjectDescriptionResponse)
      {
        var objectViewModel = Objects.FirstOrDefault(x => x.Id == getObjectDescriptionResponse.Identifier);
        if (objectViewModel != null)
          objectViewModel.Description = getObjectDescriptionResponse.Description;
      }

      if (response is TCreateObjectDataResponse createObjectResponse && _newObjects.TryGetValue(createObjectResponse.ClientObjectIdentifier, out var description))
        Objects.Add(new TObjectViewModel { Id = createObjectResponse.Identifier, Description = description});
    }

    protected override Task OnLoadedAsync(object parameter)
    {
      _communicator.ReceiveData += OnReceiveData;
      _communicator.SendAsync(new TGetAllObjectsDataRequest());
      _newObjects.Clear();

      return base.OnLoadedAsync(parameter);
    }

    protected override Task OnUnloadedAsync()
    {
      _communicator.ReceiveData -= OnReceiveData;
      return base.OnUnloadedAsync();
    }
  }
}