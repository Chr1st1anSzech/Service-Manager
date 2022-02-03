using Dienste_Verwaltung.src.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dienste_Verwaltung.src.DataReader
{
    public class ServiceIO
    {
        protected readonly string filePath = Path.Combine(Util.GetApplicationRoot(), "service-groups.json");
    }
}
