using Dienste_Verwaltung.src.DataModels;
using Dienste_Verwaltung.src.DataReader;
using Dienste_Verwaltung.src.UserControls;
using Microsoft.UI.Xaml;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ListViewHeaderItem = Dienste_Verwaltung.src.UserControls.ListViewHeaderItem;
using System.Collections.Generic;
using System.ServiceProcess;
using Dienste_Verwaltung.src.Controller;
using Dienste_Verwaltung.src.Views;
using System.Text;

namespace Dienste_Verwaltung.src.Viewmodels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<string, Action<ServiceController>> serviceFunctions = new()
        {
            { "Start", (ServiceController s1) => { SvcController.StartService(s1); } },
            { "Stop", (ServiceController s1) => { SvcController.StopService(s1); } },
            { "Pause", (ServiceController s1) => { SvcController.PauseService(s1); } },
            { "Continue", (ServiceController s1) => { SvcController.ContinueService(s1); } },
            { "Restart", (ServiceController s1) => { SvcController.RestartService(s1); } }
        };

        private readonly Dictionary<string, Func<ServiceItem, string>> orderFunctions = new()
        {
            {"Anzeigename", (ServiceItem s1) => { return s1.Service.DisplayName; }  },
            {"Dienstname", (ServiceItem s1) => { return s1.ServiceName; }  },
            {"Status", (ServiceItem s1) => { return s1.Service.Status.ToString(); }  },
            {"Starttyp",(ServiceItem s1) => { return s1.Service.StartType.ToString(); }  },
            {"Anmelden als", (ServiceItem s1) => { return s1.StartName; }  }
        };
        public bool ServiceListReady { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ServiceGroupItem> ServiceGroups { get; private set; } = new ObservableCollection<ServiceGroupItem>();

        public ObservableCollection<ServiceItem> Services { get; private set; } = new ObservableCollection<ServiceItem>();

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

        private ServiceItem selectedServiceItem;
        public ServiceItem SelectedServiceItem
        {
            get
            {
                return selectedServiceItem;
            }

            set
            {
                if (value != selectedServiceItem)
                {
                    selectedServiceItem = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public  void OnNavigatedTo()
        {
            FillServiceList();
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FillServiceList()
        {
            Services.Clear();
            ServiceGroups.Clear();
            ServiceItem.FillServices(Services);
            new ServiceGroupsReader().FillServiceGroups(ServiceGroups, Services);
        }

        internal void RefreshView()
        {
            FillServiceList();
        }

        public void SortList(string orderIdentifier, int sortOrder)
        {
            if(sortOrder == 1)
            {
                Services = new ObservableCollection<ServiceItem>(Services.OrderBy(orderFunctions[orderIdentifier]));
            }
            else
            {
                Services = new ObservableCollection<ServiceItem>(Services.OrderByDescending(orderFunctions[orderIdentifier]));
            }
            NotifyPropertyChanged(nameof(Services));
        }

        public void HandleServiceOperation(ServiceController service, string operation)
        {
            serviceFunctions[operation](service);
        }

        public string[] GetGroupNames()
        {
            return ServiceGroups.Select(group => group.GroupName).ToArray();
        }

        public void CreateNewGroup(string groupName)
        {
            if(groupName != null)
            {
                ServiceGroups.Add(new ServiceGroupItem(groupName));
                new ServiceGroupsWriter().WriteServiceGroups(ServiceGroups);
            }
        }

        public void AddToGroup(ServiceGroupItem group, IEnumerable<ServiceItem> services, out string notAddedServiceNames)
        {
            notAddedServiceNames = "";
            if (group == null || !services.Any()) return;

            StringBuilder builder = new();
            foreach (ServiceItem service in services)
            {
                if (!group.AddService(service))
                {
                    builder.Append($"\n\u2012 {service.ServiceName}");
                }
            }
            notAddedServiceNames = builder.ToString();
            new ServiceGroupsWriter().WriteServiceGroups(ServiceGroups);
        }
    }
}
