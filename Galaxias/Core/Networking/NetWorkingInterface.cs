using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking;
public class NetWorkingInterface
{
    protected readonly EventBasedNetListener Listener = new();
    protected readonly NetManager Manager;
    public NetPeer? Server { get; private set; }

    protected NetWorkingInterface()
    {

        Manager = new NetManager(Listener);
        
    }
    public void StartServer(int port)
    {
        Manager.Start(port);
    }
    public void Connect(string address, int port, string key = "key")
    {
        if (Server != null)
        {
            return;
        }
        Console.WriteLine("Connecting...");
        Manager.Start();
        Server = Manager.Connect(address, port, key);
    }
    public void Stop()
    {
        Manager.Stop();
    }
    public void Update()
    {
        Manager.PollEvents();
    }
}
