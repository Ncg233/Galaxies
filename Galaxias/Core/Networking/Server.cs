using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking;
internal class Server : NetWorkingInterface
{
    public Server() : base()
    {
        Listener.ConnectionRequestEvent += NewConnection;
        Listener.PeerConnectedEvent += NewPeer;
    }
    private void NewConnection(ConnectionRequest request)
    {
        Console.WriteLine("New Connection");
        request.AcceptIfKey("key");
    }
    private void NewPeer(NetPeer netPeer)
    {
        Console.WriteLine("New Peer");

    }
    public void StartServer(int port)
    {
        Manager.Start(port);
    }
}

