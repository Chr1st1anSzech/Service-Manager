using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dienste_Verwaltung.src.Service
{
    public interface IDialogService
    {
        public void ShowDefaultDialog(string title, string content, string close);
    }
}
