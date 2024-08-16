using Galaxias.Core.Networking.Packet;
using Galaxias.Core.Networking.Packet.C2S;
using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking;
public class Client : NetWorkingInterface
{
    public Client() : base()
    {
        Listener.PeerConnectedEvent += PeerConnected;
    }

    public void SendToServer(IPacket packet)
    {
        SendPacket(packet, Server);
    }

    private void PeerConnected(NetPeer peer)
    {
        Console.WriteLine($"Connected to server {peer.Address}:{peer.Port} as {peer.RemoteId}");
        NetPlayManager.Instance.SendToServer(new C2SLoginGamePacket());
    }
}
