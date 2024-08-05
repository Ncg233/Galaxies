using Galaxias.Core.Networking;
using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGalaxias.Client.Networking;
public class Client : NetWorkingInterface
{
    public Client() : base()
    {
        Listener.PeerConnectedEvent += PeerConnected;
    }

    private void PeerConnected(NetPeer peer)
    {
        Console.WriteLine($"Connected to server {peer.Address}:{peer.Port} as {peer.RemoteId}");
    }
}
