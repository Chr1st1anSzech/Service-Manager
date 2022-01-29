using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Dienste_Verwaltung.src.Controller
{
    public class SvcController
    {
        public enum Operations
        {
            Start,
            Stop,
            Pause,
            Restart,
            Cotninue
        }

        public static void StartService(ServiceController service)
        {
            service.Start();
        }

        public static void StopService(ServiceController service)
        {
            if (service.CanStop)
            {
                service.Stop();
            }
        }

        public static void ContinueService(ServiceController service)
        {
            if (service.CanPauseAndContinue)
            {
                service.Continue();
            }
        }

        public static void PauseService(ServiceController service)
        {
            if (service.CanPauseAndContinue)
            {
                service.Pause();
            }
        }

        public static void RestartService(ServiceController service)
        {
            StopService(service);
            try
            {
                service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10d));
                StartService(service);
            }
            catch
            {
                throw;
            }
        }

    }
}
