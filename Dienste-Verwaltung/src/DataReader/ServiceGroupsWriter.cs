using Dienste_Verwaltung.src.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dienste_Verwaltung.src.DataReader
{
    public class ServiceGroupsWriter : ServiceGroupsIO
    {
        public void WriteServiceGroups(ObservableCollection<ServiceGroupItem> serviceGroups)
        {
            //try
            //{
            string outputJson = JsonConvert.SerializeObject(SimplifyList(serviceGroups));
            File.WriteAllText(filePath, outputJson);
            //}
            //catch
            //{

            //}
        }

        private SimpleServiceGroupItem[] SimplifyList(ObservableCollection<ServiceGroupItem> serviceGroups)
        {
            SimpleServiceGroupItem[] groups = new SimpleServiceGroupItem[serviceGroups.Count];
            for (int i = 0; i < serviceGroups.Count; i++)
            {
                ServiceGroupItem serviceGroupItem = serviceGroups[i];
                groups[i] = new SimpleServiceGroupItem
                {
                    GroupName = serviceGroupItem.GroupName,
                    ServiceNames = serviceGroupItem.Services.Select(service => service.ServiceName)
                };
            }

            return groups;
        }
    }
}
