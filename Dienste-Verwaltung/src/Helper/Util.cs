using System.IO;
using System.Reflection;

namespace Dienste_Verwaltung.src.Helper
{
    public class Util
    {
        public static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return exePath;
        }
    }
}
