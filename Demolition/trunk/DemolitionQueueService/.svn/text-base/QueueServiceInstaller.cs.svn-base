using System.Configuration.Install;
using System.ComponentModel;
using System.ServiceProcess;

namespace DemolitionQueueService
{
    /// <summary>
    /// To install, run: 
    /// C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil.exe DemolitionQueueService.exe
    ///  in bin/Release
    /// To uninstall, run:
    /// C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil.exe /u DemolitionQueueService.exe
    ///  in bin/Release
    /// </summary>
    [RunInstaller(true)]
    public class QueueServiceInstaller : Installer
    {
        public QueueServiceInstaller()
        {
            var processInstaller = new ServiceProcessInstaller();
            var serviceInstaller = new ServiceInstaller();

            // privileges
            processInstaller.Account = ServiceAccount.LocalSystem;

            serviceInstaller.DisplayName = DemolitionQueueService.SERVICE_NAME;
            serviceInstaller.StartType = ServiceStartMode.Manual;

            serviceInstaller.ServiceName = DemolitionQueueService.SERVICE_NAME;

            this.Installers.Add(processInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}
