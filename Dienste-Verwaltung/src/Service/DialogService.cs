using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dienste_Verwaltung.src.Service
{
    public class DialogService : IDialogService
    {
        public XamlRoot Root { get; set; }

        public DialogService(XamlRoot root)
        {
            Root = root;
        }

        public async void ShowDefaultDialog(string title, string content, string close)
        {
            ContentDialog serviceNotAddDialog = new()
            {
                Title = title,
                Content = content,
                CloseButtonText = close,
                XamlRoot = Root

            };
            await serviceNotAddDialog.ShowAsync();
        }
    }
}
