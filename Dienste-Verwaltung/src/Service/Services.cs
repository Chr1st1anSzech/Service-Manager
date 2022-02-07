using Dienste_Verwaltung.src.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace Dienste_Verwaltung.src.Service
{
    public class Services : INotifyPropertyChanged
    {
        #region properties


        public ObservableCollection<DataModels.Service> Collection { get; private set; } = new ObservableCollection<DataModels.Service>();


        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Dictionary<string, Func<DataModels.Service, string>> orderFunctions = new()
        {
            { "Anzeigename", (DataModels.Service s1) => { return s1.ServiceController.DisplayName; } },
            { "Dienstname", (DataModels.Service s1) => { return s1.ServiceName; } },
            { "Status", (DataModels.Service s1) => { return s1.ServiceController.Status.ToString(); } },
            { "Starttyp", (DataModels.Service s1) => { return s1.ServiceController.StartType.ToString(); } },
            { "Anmelden als", (DataModels.Service s1) => { return s1.StartName; } }
        };


        #region public methods


        public void Clear()
        {
            Collection.Clear();
        }

        public async void ReadFromStorage()
        {
            ServiceController[] services = ServiceController.GetServices();
            Array.Sort(services, (service1, service2) => service1.DisplayName.CompareTo(service2.DisplayName));
            foreach (ServiceController service in services)
            {
                Collection.Add(new DataModels.Service(service));
            }

            await Task.Run(() => LoadDescription());
        }

        public void SortList(string orderIdentifier, int sortOrder)
        {
            if (sortOrder == 1)
            {
                Collection = new ObservableCollection<DataModels.Service>(Collection.OrderBy(orderFunctions[orderIdentifier]));
            }
            else
            {
                Collection = new ObservableCollection<DataModels.Service>(Collection.OrderByDescending(orderFunctions[orderIdentifier]));
            }
            NotifyPropertyChanged(nameof(Collection));
        }


        #endregion


        #region private methods


        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void LoadDescription()
        {
            foreach (DataModels.Service serviceItem in Collection)
            {
                try
                {
                    var serviceObject = new ManagementObject(new ManagementPath(string.Format("Win32_Service.Name='{0}'", serviceItem.ServiceController.ServiceName)));
                    var props = serviceObject.Properties;
                    serviceItem.Description = serviceObject["Description"]?.ToString();
                    serviceItem.Status = serviceObject["Status"]?.ToString();
                    serviceItem.StartName = serviceObject["StartName"]?.ToString();
                    serviceItem.Caption = serviceObject["Caption"]?.ToString();
                    serviceItem.Path = serviceObject["PathName"]?.ToString();
                }
                catch
                {
                    // TODO: Fehlerbehandlung
                }
            }
        }

        #endregion
    }
}
