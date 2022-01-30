using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dienste_Verwaltung.src.Views
{
    public sealed partial class TextInputDialog : ContentDialog
    {
        public string Result { get; private set; }

        private readonly string[] existingNames;

        public TextInputDialog(string[] existingNames)
        {
            InitializeComponent();
            this.existingNames = existingNames;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Result = GroupTextBox.Text;
        }

        private void GroupTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = GroupTextBox.Text;
            if (Regex.IsMatch(text, "^[a-zA-Z0-9]([a-zA-Z0-9_\\- ]*[a-zA-Z0-9]+)?$") && !existingNames.Contains(text))
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }
    }
}
