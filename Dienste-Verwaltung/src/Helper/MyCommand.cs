using System;
using System.Windows.Input;

namespace Dienste_Verwaltung.src.Helper
{
    public class MyCommand : ICommand
    {
        private readonly Action targetExecuteMethod;
        private readonly Func<bool> targetCanExecuteMethod;

        public MyCommand(Action executeMethod)
        {
            targetExecuteMethod = executeMethod;
        }

        public MyCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            targetExecuteMethod = executeMethod;
            targetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {

            if (targetCanExecuteMethod != null)
            {
                return targetCanExecuteMethod();
            }

            return targetExecuteMethod != null;
        }
		

        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            targetExecuteMethod?.Invoke();
        }
    }
}
