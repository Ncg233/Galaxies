using Galaxias.Client;
using Galaxias.Client.Main;
using Galaxias.Core.Networking.Packet;
using Galaxias.Core.Networking.Packet.C2S;
using Galaxias.Core.Networking.Packet.S2C;
using Galaxias.Core.World.Tiles;
using Galaxias.Util;
using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking;
public class Client : NetWorkingInterface
{
    public int RemoteId { get; private set; }
    public NetPeer? Server { get; private set; }
    private GalaxiasClient gClient;
    private ClientWorld world;
    public Client() : base()
    {
        gClient = GalaxiasClient.GetInstance();
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
        Server = Manager.Connect(new IPEndPoint(IPAddress.Parse(address), port), key);
    }
    public void SendToServer(C2SPacket packet)
    {
        SendPacket(packet, Server);
    }

    private void PeerConnected(NetPeer peer)
    {
        Log.Info($"Connected to server {peer.Address}:{peer.Port} as {peer.RemoteId}");
        RemoteId = peer.RemoteId;
        SendToServer(new C2SLoginGamePacket());
    }

    public void ProcessJoinWorld(S2CJoinWorldPacket packet)
    {
        Log.Info("Join Game!");
        world = new ClientWorld();
        GalaxiasClient.GetInstance().JoinWorld(world);
    }

    public void ProcessWorldData(S2CWorldDataPacket packet)
    {
        Log.Info("Load world data");
        world.ReadTileData(packet.tileData, packet.skyLight, packet.tileLight);

    }

    public void ProcessTileChange(S2CTileChangePacket packet)
    {
        world.SetTileState(TileLayer.Main, packet.x, packet.y, packet.state);
    }
}
