using Dienste_Verwaltung.src.DataModels;
using Dienste_Verwaltung.src.DataReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Dienste_Verwaltung.src.Controller
{
    public class ServiceGroups
    {
        #region properties


        public ObservableCollection<ServiceGroup> Collection { get; private set; } = new ObservableCollection<ServiceGroup>();


        #endregion

        private readonly IServiceWriter writer;
        private readonly IServiceReader reader;

        public ServiceGroups(IServiceWriter writer, IServiceReader reader)
        {
            this.writer = writer ?? throw new ArgumentNullException($"{writer.GetType().Name} ist null.");
            this.reader = reader ?? throw new ArgumentNullException($"{reader.GetType().Name} ist null.");
        }


        #region public methods


        public void ReadFromStorage(ObservableCollection<Service> services)
        {
            SimpleServiceGroup[] simpleList = reader.ReadSimpleGroups();
            foreach (SimpleServiceGroup simpleItem in simpleList)
            {
                Collection.Add(new ServiceGroup(
                    simpleItem.GroupName,
                    services.Where(service => simpleItem.ServiceNames.Contains(service.ServiceName))));
            }
        }


        public void Clear()
        {
            Collection.Clear();
        }


        public void Create(string groupName)
        {
            if (groupName != null)
            {
                Collection.Add(new ServiceGroup(groupName));
                writer.WriteServiceGroups(Collection);
            }
        }


        public void AddService(ServiceGroup group, IEnumerable<Service> services, out string notAddedServiceNames)
        {
            notAddedServiceNames = "";
            if (group == null || !services.Any()) return;

            StringBuilder builder = new();
            foreach (Service service in services)
            {
                if (!group.AddService(service))
                {
                    builder.Append($"\n\u2012 {service.ServiceName}");
                }
            }
            notAddedServiceNames = builder.ToString();
            writer.WriteServiceGroups(Collection);
        }


        public string[] ListNames()
        {
            return Collection.Select(group => group.GroupName).ToArray();
        }


        #endregion
    }
}
