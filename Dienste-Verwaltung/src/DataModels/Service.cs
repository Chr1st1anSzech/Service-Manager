using System.ServiceProcess;

namespace Dienste_Verwaltung.src.DataModels
{
    public class Service
    {
        #region properties


        public ServiceController ServiceController { get; set; }


        public string ServiceName { get; set; } = "";


        public string Description { get; set; }


        public string Caption { get; set; }


        public string StartName { get; set; }


        public string Status { get; set; }


        public string Path { get; set; }


        public string GroupMembership { get; set; }


        #endregion


        public Service(ServiceController service)
        {
            ServiceController = service;
            ServiceName = service.ServiceName;
        }
    }
}
