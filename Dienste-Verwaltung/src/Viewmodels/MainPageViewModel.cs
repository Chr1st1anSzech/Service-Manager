using Dienste_Verwaltung.src.DataModels;
using Dienste_Verwaltung.src.DataReader;
using Microsoft.UI.Xaml;
using System;
using ListViewHeaderItem = Dienste_Verwaltung.src.UserControls.ListViewHeaderItem;
using System.Collections.Generic;
using System.ServiceProcess;
using Dienste_Verwaltung.src.Service;
using Dienste_Verwaltung.src.Helper;

namespace Dienste_Verwaltung.src.Viewmodels
{
    public class MainPageViewModel : ObservableObject
    {
        #region properties

        public MyCommand RefreshListCommand { get; set; }
        public MyCommand NewGroupCommand { get; set; }

        public ServiceGroups ServiceGroups { get; private set; } = new ServiceGroups(
            new ServiceToFileWriter(), 
            new ServiceFromFileReader());


        public Services Services { get; private set; } = new Services();


        private Visibility previewVisible = Visibility.Collapsed;
        public Visibility PreviewVisible
        {
            get
            {
                return previewVisible;
            }

            set
            {
                if (value != previewVisible)
                {
                    previewVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private ListViewHeaderItem selectedHeader;
        public ListViewHeaderItem SelectedHeader
        {
            get
            {
                return selectedHeader;
            }
            set
            {
                if (value != selectedHeader)
                {
                    selectedHeader = value;
                    NotifyPropertyChanged();
                }
            }
        }


        #endregion

        

        private readonly Dictionary<string, Action<ServiceController>> serviceFunctions = new()
        {
            { "Start", (ServiceController s1) => { ServiceOperations.StartService(s1); } },
            { "Stop", (ServiceController s1) => { ServiceOperations.StopService(s1); } },
            { "Pause", (ServiceController s1) => { ServiceOperations.PauseService(s1); } },
            { "Continue", (ServiceController s1) => { ServiceOperations.ContinueService(s1); } },
            { "Restart", (ServiceController s1) => { ServiceOperations.RestartService(s1); } }
        };

        public ITextInputDialogService TextInputDialogService { get; set; }

        #region public methods

        public MainPageViewModel()
        {
            SetCommands();
        }

        public void OnNavigatedTo()
        {
            UpdateServiceItemSource();
        }


        public void UpdateServiceItemSource()
        {
            Services.Clear();
            ServiceGroups.Clear();
            Services.ReadFromStorage();
            ServiceGroups.ReadFromStorage(Services.Collection);
        }


        public void SortList(string orderIdentifier, int sortOrder)
        {
            Services.SortList(orderIdentifier, sortOrder);
        }


        public void HandleServiceOperation(ServiceController service, string operation)
        {
            serviceFunctions[operation](service);
        }


        public string[] GetGroupNames()
        {
            return ServiceGroups.ListNames();
        }


        public async void CreateNewGroup()
        {
            string result = await TextInputDialogService.ShowDefaultDialogAsync("Neue Gruppe", "Geben Sie den Namen ein:", GetGroupNames());
            ServiceGroups.Create(result);
        }


        public void AddToGroup(ServiceGroup group, IEnumerable<DataModels.Service> services, out string notAddedServiceNames)
        {
            ServiceGroups.AddService(group, services, out notAddedServiceNames);
        }


        #endregion


        #region private methods


        private void SetCommands()
        {
            RefreshListCommand = new MyCommand(UpdateServiceItemSource);
            NewGroupCommand = new MyCommand(CreateNewGroup);
        }


        #endregion
    }
}
