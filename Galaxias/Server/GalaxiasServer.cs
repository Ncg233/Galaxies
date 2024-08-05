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
    public GalaxiasServer() {
        server.StartServer(Port);
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
        server.Stop();
    }
    public void Run()
    {
        while (isServerRunning) {       
            server.Update();
        }
    }

}
