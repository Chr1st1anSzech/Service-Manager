using Dienste_Verwaltung.src.Views;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dienste_Verwaltung.src.Service
{
    internal class TextInputDialogService : ITextInputDialogService
    {
        private readonly XamlRoot root;

        public TextInputDialogService(XamlRoot root)
        {
            this.root = root;
        }

        public async Task<string> ShowDefaultDialogAsync(string title, string content, string[] forbiddenInputs)
        {
            TextInputDialog dialog = new(root, title, content, forbiddenInputs);
            await dialog.ShowAsync();
            return dialog.ViewModel.InputText;
        }
    }
}
