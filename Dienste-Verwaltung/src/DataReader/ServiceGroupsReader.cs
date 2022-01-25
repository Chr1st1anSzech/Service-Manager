using Dienste_Verwaltung.src.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Dienste_Verwaltung.src.DataReader
{
    public class ServiceGroupsReader : ServiceGroupsIO
    {
        public void FillServiceGroups(
            ObservableCollection<ServiceGroupItem> groups,
            ObservableCollection<ServiceItem> services)
        {
            try
            {
                SimpleServiceGroupItem[] simpleList = ReadSimpleList();
                RecreateCollection(groups, services, simpleList);
            }
            catch
            {

            }
        }

        private SimpleServiceGroupItem[] ReadSimpleList()
        {
            if (!File.Exists(filePath))
            {
                return Array.Empty<SimpleServiceGroupItem>();
            }
            string jsonString = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<SimpleServiceGroupItem[]>(jsonString);
        }

        private void RecreateCollection(
            ObservableCollection<ServiceGroupItem> groups,
            ObservableCollection<ServiceItem> services,
            SimpleServiceGroupItem[] simpleList)
        {
            foreach (SimpleServiceGroupItem simpleItem in simpleList)
            {
                groups.Add(new ServiceGroupItem(
                    simpleItem.GroupName,
                    services.Where(service => simpleItem.ServiceNames.Contains(service.ServiceName))));
            }
        }
    }
}
