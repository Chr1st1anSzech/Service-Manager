using Dienste_Verwaltung.src.Validation;
using Dienste_Verwaltung.src.Viewmodels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Dienste_Verwaltung.src.Views
{
    public sealed partial class TextInputDialog : ContentDialog
    {
        public TextInputDialogViewModel ViewModel{get;set;}

        public TextInputDialog(XamlRoot root, string title, string description, string[] forbiddenInputs)
        {
            ViewModel = new TextInputDialogViewModel(new Validator(Validator.OnlyCharsAndDash, forbiddenInputs))
            {
                Title = title,
                Description = description
            };
            DataContext = ViewModel;
            XamlRoot = root;
            InitializeComponent();

        }
    }
}
