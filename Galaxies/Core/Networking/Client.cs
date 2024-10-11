﻿using Galaxies.Client;
using Galaxies.Core.Networking.Packet.C2S;
using Galaxies.Core.Networking.Packet.S2C;
using Galaxies.Core.World.Tiles;
using Galaxies.Util;
using LiteNetLib;
using LiteNetLib.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Networking;
public class Client : NetWorkingInterface
{

    public int RemoteId { get; private set; }
    public NetPeer Server { get; private set; }
    private Main gClient;
    private ClientWorld world;
    public Client(Main gClient) : base()
    {
        this.gClient = gClient;
        Listener.PeerConnectedEvent += PeerConnected;
        Listener.PeerDisconnectedEvent += PeerDisconnected;

    }
    private void PeerConnected(NetPeer peer)
    {
        Log.Info($"Connected to server {peer.Address}:{peer.Port} as {peer.RemoteId}");
        RemoteId = peer.RemoteId;
        SendToServer(new C2SLoginGamePacket());
    }
    public void PeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        gClient.QuitWorld();
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
    public void ProcessJoinWorld(S2CJoinWorldPacket packet)
    {
        Log.Info("Join Game!");
        world = new ClientWorld(gClient.WorldRenderer);
        gClient.LoadWorld(world);
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

    public void ProcessTimeSync(S2CTimeSyncPacket packet)
    {
        world.SetCurrentTime(packet.CurrentTime);
    }
}