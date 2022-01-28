using Dienste_Verwaltung.src.DataModels;
using Dienste_Verwaltung.src.DataReader;
using Dienste_Verwaltung.src.UserControls;
using Microsoft.UI.Xaml;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml.Controls;
using ListViewHeaderItem = Dienste_Verwaltung.src.UserControls.ListViewHeaderItem;
using System.Collections.Generic;
using System.ServiceProcess;

namespace Dienste_Verwaltung.src.Viewmodels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private Dictionary<string, Action<ServiceController>> serviceFunctions = new()
        {
            { "Start", (ServiceController s1) => { s1.Start(); } },
            { "Stop", (ServiceController s1) => { s1.Stop(); } },
            { "Pause", (ServiceController s1) => { s1.Pause(); } },
            { "Continue", (ServiceController s1) => { s1.Continue(); } },
            { "Restart", (ServiceController s1) => { s1.Refresh(); } }
        };

        private Dictionary<string, Func<ServiceItem, string>> orderFunctions = new()
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
            ServiceItem.FillServices(Services);
            new ServiceGroupsReader().FillServiceGroups(ServiceGroups, Services);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        public void HandleServiceOperation(ServiceController service, string operation, Action<string> notifyUserFunction)
        {
            try
            {
                serviceFunctions[operation](service);
            }
            catch (Exception e)
            {
                notifyUserFunction(e.Message);
            }
        }

    }
}
