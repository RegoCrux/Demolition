Process to Install and Run DemolitionQueueService
-------------------------------------------------
INSTALL:
* In bin/Release run 
	C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil.exe DemolitionQueueService.exe
* Go into the services and manually start "DemolitionQueueService". Automatic start is possible, just not in there.
* Log at c:/LOGS/ServiceLog.txt

INSTALL GOTCHYAS
* Right click on cmd.exe and 'Run as Administrator' to get elevated privledges
* Change the 'var path' in Model.cs to have absolute path to Demolition/Web.config
	
UNINSTALL:
* Stop service. Uninstalling before stopping will not always create an error, but whatever just stop it k thnx.
* In bin/Release run
	C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil.exe /u DemolitionQueueService.exe
	
ERRORS I'VE SEEN
* Service stopping immediately after starting. This is caused by windows thinking the service is done, since no
threads are running on it. This is probably an exception.
* Uninstallable service. Manually uninstalling doesn't work, and uninstalling leaves service in perpetual 'Disabled'
state. It is impossibe to reinstall in this state. This is due to resources being held up, eg. exception caused 
file stream to be left open or summat. Restart computer.
* I just got an error the first time I ran the service that there was no file c:/LOGS/Queuelog.txt  Added it 
manually and the error went away, service is still running but nothing seems to be happening

COMMANDS:
Install: 
	C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil.exe ./DemolitionQueueService.exe
Uninstall:
	C:\Windows\Microsoft.NET\Framework\v2.0.50727\installutil.exe /u ./DemolitionQueueService.exe
Start Service
	net start DemolitionQueueService
Stop Service
	net stop DemolitionQueueService
	