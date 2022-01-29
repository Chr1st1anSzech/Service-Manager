using Dienste_Verwaltung.src.Controller;
using Dienste_Verwaltung.src.DataModels;
using Dienste_Verwaltung.src.Viewmodels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Dienste_Verwaltung.src.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel
        {
            get => (MainPageViewModel)DataContext;
            set => DataContext = value;
        }

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.OnNavigatedTo();
        }

        private void ServiceListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if( sender is ListView listView && listView.SelectedItem is ServiceItem serviceItem)
            {
                ViewModel.SelectedServiceItem = serviceItem;
                ViewModel.PreviewVisible = Visibility.Visible;
            }
            else
            {
                ViewModel.PreviewVisible = Visibility.Collapsed;
            }
        }

        private void ListViewHeaderItem_HeaderClicked(object sender, EventArgs e)
        {
            if (sender is UserControls.ListViewHeaderItem item)
            {
                ViewModel.SelectedHeader = item;
                foreach (var child in HeaderGrid.Children)
                {
                    if (child is UserControls.ListViewHeaderItem headerItem)
                    {
                        headerItem.ArrowVisibility = 0d;
                    }
                }
                ViewModel.SelectedHeader.ArrowVisibility = 1d;
                ViewModel.SortList(item.HeaderText, item.SortOrder);
            }
        }
        private void ServiceHandler(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement control && ServiceListView.SelectedItem is ServiceItem serviceItem)
            {
                ViewModel.HandleServiceOperation(serviceItem.Service, control.Tag.ToString(), NotifyUser);
            }
        }

        private void ServiceFlyout_Opened(object sender, object e)
        {
            if (sender is MenuFlyout menueFlyout)
            {
                ServiceListView.SelectedItem = menueFlyout.Target.DataContext;
            }
        }

        private async void NotifyUser(string reason)
        {
            ContentDialog noWifiDialog = new()
            {
                Title = "Aktion fehlgeschlagen",
                Content = $"Die angeforderte Aktion konnte nicht durchgeführt werden. Grund:\n\n{reason}",
                CloseButtonText = "Ok",
                XamlRoot = ServiceListView.XamlRoot
                
            };
            await noWifiDialog.ShowAsync();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RefreshView();
        }
    }
}
