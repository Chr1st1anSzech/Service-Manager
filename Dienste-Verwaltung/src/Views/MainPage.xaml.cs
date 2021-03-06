using Dienste_Verwaltung.src.DataModels;
using Dienste_Verwaltung.src.Viewmodels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.DataTransfer;
using Dienste_Verwaltung.src.Service;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Dienste_Verwaltung.src.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region properties


        public MainPageViewModel ViewModel
        {
            get => (MainPageViewModel)DataContext;
            set => DataContext = value;
        }


        #endregion


        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.TextInputDialogService = new TextInputDialogService(ServiceListView.XamlRoot);
            ViewModel.OnNavigatedTo();
        }

        private void ServiceListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if( sender is ListView listView && listView.SelectedItem is DataModels.Service serviceItem)
            {
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
            if (sender is FrameworkElement control && ServiceListView.SelectedItem is DataModels.Service serviceItem)
            {
                try
                {
                    ViewModel.HandleServiceOperation(serviceItem.ServiceController, control.Tag.ToString());
                }
                catch (Exception ex)
                {
                    ShowDefaultDialog("Aktion fehlgeschlagen",
                        $"Die angeforderte Aktion konnte nicht durchgeführt werden. Grund:\n\n{ex.InnerException?.Message} - {ex.Message}",
                        "Ok");
                }
                
            }
        }

        private void ServiceFlyout_Opened(object sender, object e)
        {
            if (sender is MenuFlyout menueFlyout)
            {
                ServiceListView.SelectedItem = menueFlyout.Target.DataContext;
            }
        }

        private async void ShowDefaultDialog(string title, string content, string close)
        {
            ContentDialog serviceNotAddDialog = new()
            {
                Title = title,
                Content = content,
                CloseButtonText = close,
                XamlRoot = ServiceListView.XamlRoot

            };
            await serviceNotAddDialog.ShowAsync();
        }

        private void AddToGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if(GroupsTreeView.SelectedItem is ServiceGroup group)
            {
                IEnumerable<DataModels.Service> services = ServiceListView.SelectedItems.Cast<DataModels.Service>();
                ViewModel.AddToGroup(group, services, out string notAddedServiceNames);

                if(!string.IsNullOrEmpty(notAddedServiceNames))
                {
                    ShowDefaultDialog("Hinzufügen fehlgeschlagen",
                        $"Diese Services sind bereits in der Gruppe enthalten und können nicht erneut hinzugefügt werden: {notAddedServiceNames}",
                        "Ok");
                }
            }
        }
    }
}
