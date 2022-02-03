using Dienste_Verwaltung.src.DataReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dienste_Verwaltung.src.DataModels
{
    public class ServiceGroup : INotifyPropertyChanged
    {
        public string GroupName { get; set; }
        private ObservableCollection<Service> services = new();
        public ObservableCollection<Service> Services
        {
            get
            {
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

        public ServiceGroup(string name)
        {
            GroupName = name;
        }
        public ServiceGroup(string name, IEnumerable<Service> services)
        {
            GroupName = name;
            Services = new ObservableCollection<Service>(Services.Concat(services));
        }

        public bool AddService(Service service)
        {
            if( !Services.Contains(service))
            {
                Services.Add(service);
                return true;
            }
            return false;
        }
    }
}
