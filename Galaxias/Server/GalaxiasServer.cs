using Galaxias.Core.Networking;
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
    private Thread serverThread;
    private bool isServerRunning = false;
    private int _continueRun = 1;
    public GalaxiasServer()
    {

    }
    public void StartServerThread()
    {
        serverThread = new Thread(Run);
        isServerRunning = true;
        
        serverThread.Start();
    }
    public void StopServer()
    {
        isServerRunning = false;
        Interlocked.Exchange(ref _continueRun, 0);
        NetPlayManager.Instance.StopServer();
    }
    public void Run()
    {
        while (Interlocked.Exchange(ref _continueRun, 1) == 1)
        {
            NetPlayManager.Instance.UpdateServer();
            //Thread.Sleep(1000);

        }
    }
}
