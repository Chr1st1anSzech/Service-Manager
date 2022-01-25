using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Dienste_Verwaltung.src.UserControls
{
    public partial class ListViewHeaderItem : UserControl, INotifyPropertyChanged
    {
        public string HeaderText { get; set; } = "";

        public int SortOrder { get; set; } = 1;

        public event EventHandler HeaderClicked;

        protected virtual void OnHeaderClicked(EventArgs e)
        {
            EventHandler handler = HeaderClicked;
            handler?.Invoke(this, e);
        }


        private double arrowVisibility = 0d;
        public double ArrowVisibility
        {
            get
            {
                return arrowVisibility;
            }

            set
            {
                if (value != arrowVisibility)
                {
                    arrowVisibility = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ListViewHeaderItem()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ArrowVisibility == 1)
            {
                SortOrder *= -1;
                IconRotation.Angle *= -1;
            }
            OnHeaderClicked(null);
        }
    }
}
