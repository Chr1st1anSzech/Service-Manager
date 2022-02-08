using Dienste_Verwaltung.src.DataReader;
using Dienste_Verwaltung.src.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dienste_Verwaltung.src.DataModels
{
    public class ServiceGroup : ObservableObject
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


        public ServiceGroup(string name)
        {
            GroupName = name;
        }
        public ServiceGroup(string name, IEnumerable<Service> services)
        {
            GroupName = name;
            foreach (Service service in services)
            {
                AddService(service);
            }
        }

        public bool AddService(Service service)
        {
            if( !Services.Contains(service))
            {
                Services.Add(service);
                service.GroupMembership = GroupName;
                return true;
            }
            return false;
        }
    }
}
