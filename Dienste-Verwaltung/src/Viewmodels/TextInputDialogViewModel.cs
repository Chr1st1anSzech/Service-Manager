using Dienste_Verwaltung.src.Helper;
using Dienste_Verwaltung.src.Validation;

namespace Dienste_Verwaltung.src.Viewmodels
{
    public class TextInputDialogViewModel : ObservableObject
    {
        #region properties
        private string inputText;
        public string InputText {
            get
            {
                return inputText;
            }
            set
            {
                if (value != inputText)
                {
                    inputText = value;
                    Validate();
                    NotifyPropertyChanged();
                }
            }
        }


        private bool isPrimaryButtonEnabled = false;
        public bool IsPrimaryButtonEnabled {
            get
            {
                return isPrimaryButtonEnabled;
            }

            set
            {
                if (value != isPrimaryButtonEnabled)
                {
                    isPrimaryButtonEnabled = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private string title = "";
        public string Title {
            get
            {
                return title;
            }

            set
            {
                if (value != title)
                {
                    title = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private string description = "";
        public string Description {
            get
            {
                return description;
            }

            set
            {
                if (value != description)
                {
                    description = value;
                    NotifyPropertyChanged();
                }
            }
        }


        #endregion

        private readonly Validator inputValidator;
        public TextInputDialogViewModel(Validator validator)
        {
            inputValidator = validator;
        }

        private void Validate()
        {
            if (inputValidator.Validate(inputText))
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
