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

        public void StartService(ServiceController service)
        {
            service.Start();
        }

        public void StopService(ServiceController service)
        {
            if (service.CanStop)
            {
                service.Stop();
            }
        }

        public void ContinueService(ServiceController service)
        {
            if (service.CanPauseAndContinue)
            {
                service.Continue();
            }
        }

        public void PauseService(ServiceController service)
        {
            if (service.CanPauseAndContinue)
            {
                service.Pause();
            }
        }
        public void RestartService(ServiceController service)
        {
            service.Refresh();
        }

    }
}
