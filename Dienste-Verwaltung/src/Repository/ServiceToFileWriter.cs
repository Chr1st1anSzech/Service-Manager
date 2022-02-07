using Dienste_Verwaltung.src.DataModels;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Dienste_Verwaltung.src.DataReader
{
    public class ServiceToFileWriter : ServiceIO, IServiceWriter
    {
        public void WriteServiceGroups(ObservableCollection<ServiceGroup> serviceGroups)
        {
            string outputJson = JsonConvert.SerializeObject(SimplifyList(serviceGroups));
            File.WriteAllText(filePath, outputJson);
        }

        private SimpleServiceGroup[] SimplifyList(ObservableCollection<ServiceGroup> serviceGroups)
        {
            SimpleServiceGroup[] groups = new SimpleServiceGroup[serviceGroups.Count];
            for (int i = 0; i < serviceGroups.Count; i++)
            {
                ServiceGroup serviceGroupItem = serviceGroups[i];
                groups[i] = new SimpleServiceGroup
                {
                    GroupName = serviceGroupItem.GroupName,
                    ServiceNames = serviceGroupItem.Services.Select(service => service.ServiceName)
                };
            }

            return groups;
        }
    }
}
