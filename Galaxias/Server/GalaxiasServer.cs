using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxias.Server;
public class GalaxiasServer
{
    public static int Port = 9050;
    private readonly Server server = new();
    private Thread serverThread;
    private bool isServerRunning = false;
    private int _continueRun = 1;
    public GalaxiasServer() {
        
    }
    public void StartServerThread()
    {
        serverThread = new Thread(Run);
        isServerRunning = true;
        server.StartServer(Port);
        serverThread.Start();
    }
    public void StopServer()
    {
        isServerRunning = false;
        Interlocked.Exchange(ref _continueRun, 0);
        server.Stop();
    }
    public void Run()
    {
        while (Interlocked.Exchange(ref _continueRun, 1) == 1) {       
            server.Update();
            Thread.Sleep(1000);
            
        }
    }

}
