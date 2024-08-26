using Galaxias.Client.Main;
using Galaxias.Core.Networking.Packet;
using Galaxias.Core.Networking.Packet.C2S;
using Galaxias.Server;
using Galaxias.Util;
using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking;
public class Server : NetWorkingInterface
{
    private readonly NetPeer[] connetionClient = new NetPeer[128];
    public GalaxiasServer GalaxiasServer;
    public Server(GalaxiasServer server) : base()
    {
        GalaxiasServer = server;
        Listener.ConnectionRequestEvent += NewConnection;
        Listener.PeerConnectedEvent += NewPeer;
    }
    private void NewConnection(ConnectionRequest request)
    {
        Log.Info("New Connection");
        request.AcceptIfKey("key");
    }
    private void NewPeer(NetPeer netPeer)
    {
        Log.Info("New Peer");
        connetionClient[netPeer.Id] = netPeer;

    }
    public void StartServer(int port)
    {
        Manager.Start(IPAddress.Any, IPAddress.IPv6Any, port, false);
    }
    public void SendToAllClients(IPacket packet)
    {
        foreach (var peer in connetionClient)
        {
            if(peer != null)
            {
                SendPacket(packet, peer);
            }
        }
    }
    //process packet
    public void ProcessLoginGame(C2SLoginGamePacket packet)
    {
        var peer = connetionClient[packet._id];
        if (peer == null) {
            Log.Error("?");
        }else
        {
            GalaxiasServer.InitConnectionPlayer(peer);
        }

    }
    public void ProcessDigging(C2SPlayerDiggingPacket packet)
    {
        var player = GalaxiasServer.GetPlayer(packet._id);
        player.interactionManager.DestroyTile(packet.x, packet.y);
    }

    
}

