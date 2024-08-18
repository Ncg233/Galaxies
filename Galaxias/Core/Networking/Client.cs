using Galaxias.Client;
using Galaxias.Client.Main;
using Galaxias.Core.Networking.Packet;
using Galaxias.Core.Networking.Packet.C2S;
using Galaxias.Core.Networking.Packet.S2C;
using Galaxias.Util;
using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking;
public class Client : NetWorkingInterface
{
    public int RemoteId { get; private set; }
    public NetPeer? Server { get; private set; }
    private GalaxiasClient gClient;
    private ClientWorld world;
    public Client(GalaxiasClient gClient) : base()
    {
        this.gClient = gClient;
        Listener.PeerConnectedEvent += PeerConnected;
    }
    public void Connect(string address, int port, string key = "key")
    {
        if (Server != null)
        {
            return;
        }
        Log.Info("Connecting Server");
        Manager.Start();
        Server = Manager.Connect(address, port, key);
    }
    public void SendToServer(IPacket packet)
    {
        SendPacket(packet, Server);
    }

    private void PeerConnected(NetPeer peer)
    {
        Log.Info($"Connected to server {peer.Address}:{peer.Port} as {peer.RemoteId}");
        RemoteId = peer.RemoteId;
        SendToServer(new C2SLoginGamePacket(RemoteId));
    }

    public void ProcessJoinWorld(S2CJoinWorldPacket packet)
    {
        Log.Info("Join Game!");
        world = new ClientWorld();
        GalaxiasClient.GetInstance().JoinWorld(world);
    }
}
