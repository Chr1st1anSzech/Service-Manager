using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dienste_Verwaltung.src.DataModels
{
    public class ServiceGroupItem : INotifyPropertyChanged
    {
        public string GroupName { get; set; }
        private ObservableCollection<ServiceItem> services;
        public ObservableCollection<ServiceItem> Services
        {
            get
            {
                if (services == null)
                {
                    services = new ObservableCollection<ServiceItem>();
                }
                return services;
            }
            set
            {
                services = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ServiceGroupItem() { }

        public ServiceGroupItem(string name, IEnumerable<ServiceItem> services)
        {
            GroupName = name;
            Services = new ObservableCollection<ServiceItem>(Services.Concat(services));
        }
    }
}
