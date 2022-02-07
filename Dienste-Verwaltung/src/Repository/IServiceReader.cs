using Dienste_Verwaltung.src.DataModels;
using System.Collections.ObjectModel;

namespace Dienste_Verwaltung.src.DataReader
{
    public interface IServiceReader
    {
        public SimpleServiceGroup[] ReadSimpleGroups();
    }
}
