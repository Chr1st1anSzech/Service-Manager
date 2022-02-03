﻿using Dienste_Verwaltung.src.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace Dienste_Verwaltung.src.Controller
{
    public class Services : INotifyPropertyChanged
    {
        #region properties


        public ObservableCollection<Service> Collection { get; private set; } = new ObservableCollection<Service>();


        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Dictionary<string, Func<Service, string>> orderFunctions = new()
        {
            { "Anzeigename", (Service s1) => { return s1.ServiceController.DisplayName; } },
            { "Dienstname", (Service s1) => { return s1.ServiceName; } },
            { "Status", (Service s1) => { return s1.ServiceController.Status.ToString(); } },
            { "Starttyp", (Service s1) => { return s1.ServiceController.StartType.ToString(); } },
            { "Anmelden als", (Service s1) => { return s1.StartName; } }
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
                Collection.Add(new Service(service));
            }

            await Task.Run(() => LoadDescription());
        }

        public void SortList(string orderIdentifier, int sortOrder)
        {
            if (sortOrder == 1)
            {
                Collection = new ObservableCollection<Service>(Collection.OrderBy(orderFunctions[orderIdentifier]));
            }
            else
            {
                Collection = new ObservableCollection<Service>(Collection.OrderByDescending(orderFunctions[orderIdentifier]));
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
            foreach (Service serviceItem in Collection)
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
