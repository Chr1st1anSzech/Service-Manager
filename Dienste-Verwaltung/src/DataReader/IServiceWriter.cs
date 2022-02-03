using Dienste_Verwaltung.src.DataModels;
using System.Collections.ObjectModel;

namespace Dienste_Verwaltung.src.DataReader
{
    public interface IServiceWriter
    {
        public void WriteServiceGroups(ObservableCollection<ServiceGroup> serviceGroups);
    }
}
