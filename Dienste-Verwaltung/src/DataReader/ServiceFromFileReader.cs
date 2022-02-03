using Dienste_Verwaltung.src.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Dienste_Verwaltung.src.DataReader
{
    public class ServiceFromFileReader : ServiceIO, IServiceReader
    {
        public SimpleServiceGroup[] ReadSimpleGroups()
        {
            return DeserializeSimpleGroups();
        }

        private SimpleServiceGroup[] DeserializeSimpleGroups()
        {
            if (!File.Exists(filePath))
            {
                return Array.Empty<SimpleServiceGroup>();
            }
            try
            {
                string jsonString = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<SimpleServiceGroup[]>(jsonString);
            }
            catch (Exception ex)
            {
                // TODO: Fehlermeldung
            }
            return Array.Empty<SimpleServiceGroup>();
        }
    }
}
