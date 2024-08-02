using Galaxias.Core.Networking;
using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Server;
internal class Server : NetWorkingInterface
{
    public Server()
    {
        Listener.ConnectionRequestEvent += NewConnection;
    }
    private void NewConnection(ConnectionRequest request)
    {
        Console.WriteLine("New Connection");
        request.AcceptIfKey("key");
    }
}

