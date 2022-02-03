using Dienste_Verwaltung.src.DataModels;
using Dienste_Verwaltung.src.DataReader;
using Microsoft.UI.Xaml;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ListViewHeaderItem = Dienste_Verwaltung.src.UserControls.ListViewHeaderItem;
using System.Collections.Generic;
using System.ServiceProcess;
using Dienste_Verwaltung.src.Controller;

namespace Dienste_Verwaltung.src.Viewmodels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        #region properties


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

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Dictionary<string, Action<ServiceController>> serviceFunctions = new()
        {
            { "Start", (ServiceController s1) => { SvcController.StartService(s1); } },
            { "Stop", (ServiceController s1) => { SvcController.StopService(s1); } },
            { "Pause", (ServiceController s1) => { SvcController.PauseService(s1); } },
            { "Continue", (ServiceController s1) => { SvcController.ContinueService(s1); } },
            { "Restart", (ServiceController s1) => { SvcController.RestartService(s1); } }
        };


        #region public methods


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


        public void CreateNewGroup(string groupName)
        {
            ServiceGroups.Create(groupName);
        }


        public void AddToGroup(ServiceGroup group, IEnumerable<Service> services, out string notAddedServiceNames)
        {
            ServiceGroups.AddService(group, services, out notAddedServiceNames);
        }


        #endregion


        #region private methods

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion
    }
}
