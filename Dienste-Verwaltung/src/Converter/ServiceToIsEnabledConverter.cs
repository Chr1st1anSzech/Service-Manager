using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Dienste_Verwaltung.src.Converter
{
    internal class ServiceToIsEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ServiceController service && parameter is string operation)
            {
                if (service.Status == ServiceControllerStatus.Stopped && operation.Equals("Start") )
                {
                    return true;
                }
                else if (service.Status == ServiceControllerStatus.Running)
                {
                    if (service.CanStop && operation.Equals("Stop"))
                        return true;
                    else if (service.CanPauseAndContinue && operation.Equals("Pause"))
                        return true;
                    else if (operation.Equals("Restart"))
                        return true;
                }
                else if (service.Status == ServiceControllerStatus.Paused)
                {
                    if (service.CanPauseAndContinue && operation.Equals("Continue"))
                        return true;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
