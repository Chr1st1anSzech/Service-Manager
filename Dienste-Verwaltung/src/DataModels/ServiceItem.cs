using System.Collections.ObjectModel;
using System.Management;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace Dienste_Verwaltung.src.DataModels
{
    public class ServiceItem
    {
        public ServiceController Service { get; set; }

        public string ServiceName { get; set; } = "";
        public string Description { get; set; }
        public string Caption { get; set; }
        public string StartName { get; set; }
        public string Status { get; set; }

        public string Path { get; set; }

        public ServiceItem(ServiceController service)
        {
            Service = service;
            ServiceName = service.ServiceName;
        }

        public static async void FillServices(Collection<ServiceItem> servicesCollection)
        {
            ServiceController[] services = ServiceController.GetServices();
            System.Array.Sort(services, (service1, service2) => service1.DisplayName.CompareTo(service2.DisplayName) );
            foreach (ServiceController service in services)
            {
                servicesCollection.Add(new ServiceItem(service));
            }

            await Task.Run( () => LoadDescription(servicesCollection));
        }

        public static void LoadDescription(Collection<ServiceItem> servicesCollection)
        {
            foreach (ServiceItem serviceItem in servicesCollection)
            {
                try
                {
                    var serviceObject = new ManagementObject(new ManagementPath(string.Format("Win32_Service.Name='{0}'", serviceItem.Service.ServiceName)));
                    var props = serviceObject.Properties;
                    serviceItem.Description = serviceObject["Description"]?.ToString();
                    serviceItem.Status = serviceObject["Status"]?.ToString();
                    serviceItem.StartName = serviceObject["StartName"]?.ToString();
                    serviceItem.Caption = serviceObject["Caption"]?.ToString();
                    serviceItem.Path = serviceObject["PathName"]?.ToString();
                }
                catch
                {

                }
            }
        }
    }
}
