using Galaxias.Client.Main;
using Galaxias.Core.Networking.Packet;
using Galaxias.Core.Networking.Packet.C2S;
using Galaxias.Core.World.Items;
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
    public override void Update()
    {
        if (NetPlayManager.IsRomate)
        {
            Manager.PollEvents();
        }
        else
        {
            for (int i = 0; i < localC2SPacket.Count; i++)
            {
                localC2SPacket[i].Process(this);
                localC2SPacket.RemoveAt(i);
            }
        }
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
        if (NetPlayManager.IsRomate) {
            foreach (var peer in connetionClient)
            {
                if (peer != null)
                {
                    SendToClient(packet, peer);
                }
            }
        }else
        {
            SendLoaclPacket(packet, NetPlayManager.RomateClient);
        }
        
    }
    public void SendToClient(IPacket packet, NetPeer peer)
    {
        if (NetPlayManager.IsRomate) {
            SendPacket(packet, peer);
        }else
        {
            SendLoaclPacket(packet, NetPlayManager.RomateClient);
        }
    }


    //process packet
    public void ProcessLoginGame(C2SLoginGamePacket packet)
    {
        var peer = connetionClient[packet._id];
        
        GalaxiasServer.InitConnectionPlayer(peer);
        

    }
    public void ProcessDigging(C2SPlayerDiggingPacket packet)
    {
        var player = GalaxiasServer.GetPlayer(packet._id);
        player.interactionManager.DestroyTile(packet.x, packet.y);
    }

    public void ProcessUseItem(C2SUseItemPacket packet)
    {
        var player = GalaxiasServer.GetPlayer(packet._id);
        ItemPile pile = player.GetItemOnHand();
        if (!pile.isEmpty()) {
            player.interactionManager.UseItem(pile, packet.X, packet.Y);
        }
    }

    public void ProcessSyncHeldItem(C2SSyncHeldItemPacket packet)
    {
        var player = GalaxiasServer.GetPlayer(packet._id);
        player.Inventory.onHand = packet.CurrentItem;
    }
}

