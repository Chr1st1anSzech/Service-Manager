using System.Threading.Tasks;

namespace Dienste_Verwaltung.src.Service
{
    public interface ITextInputDialogService
    {
        public Task<string> ShowDefaultDialogAsync(string title, string content, string[] invalidInputs);
    }
}
