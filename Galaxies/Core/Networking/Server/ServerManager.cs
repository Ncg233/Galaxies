﻿using Galaxies.Client;
using Galaxies.Core.Networking;
using Galaxies.Core.Networking.Packet;
using Galaxies.Core.Networking.Packet.C2S;
using Galaxies.Core.Networking.Packet.S2C;
using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Items;
using Galaxies.Util;
using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Networking.Server;
public class ServerManager : NetWorkingInterface
{
    private readonly NetPeer[] connetionClient = new NetPeer[128];
    private readonly AbstractPlayerEntity[] connetionPlayers = new AbstractPlayerEntity[128];
    private Main mainServer;
    public ServerManager(Main mainServer) : base()
    {
        Listener.ConnectionRequestEvent += NewConnection;
        Listener.PeerConnectedEvent += NewPeer;
        this.mainServer = mainServer;
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
            if (peer != null)
            {
                SendToClient(packet, peer);
            }
        }


    }
    public void SendToClient(IPacket packet, NetPeer peer)
    {
        SendPacket(packet, peer);
    }

    //process packet
    public void ProcessLoginGame(C2SLoginGamePacket packet)
    {
        var peer = connetionClient[packet._id];
        var world = mainServer.GetWorld();
        var player = world.CreatePlayer(peer, default);
        world.AddEntity(player);
        connetionPlayers[packet._id] = player;
        SendPacket(new S2CJoinWorldPacket(), peer);
        SendPacket(new S2CWorldDataPacket(mainServer.GetWorld()), peer);

    }
    public void ProcessDigging(C2SPlayerDiggingPacket packet)
    {
        var player = connetionPlayers[packet._id];
        //InteractionManager.PlayerDigging(mainServer.GetWorld(), player, packet.x, packet.y);
    }

    public void ProcessUseItem(C2SUseItemPacket packet)
    {
        var player = connetionPlayers[packet._id];
        ItemPile pile = player.GetItemOnHand();
        if (!pile.IsEmpty())
        {
            //InteractionManager.PlayerUseItem(mainServer.GetWorld(),player, packet.X, packet.Y);
        }
    }

    public void ProcessSyncHeldItem(C2SSyncHeldItemPacket packet)
    {
        //var player = GalaxiasServer.GetPlayer(packet._id);
        //player.Inventory.onHand = packet.CurrentItem;
    }

    internal void ProcessPlayerMove(C2SPlayerMovePacket packet)
    {
        var player = connetionPlayers[packet._id];
        player.vx = packet.vx;
        player.vy = packet.vy;
        player.direction = packet.isRight ? Direction.Right : Direction.Left;
        player.SetPos(packet.x, packet.y);

    }
}

