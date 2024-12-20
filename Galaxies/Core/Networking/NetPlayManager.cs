﻿using Galaxies.Client;
using Galaxies.Core.Networking.Client;
using Galaxies.Core.Networking.Packet.C2S;
using Galaxies.Core.Networking.Packet.S2C;
using Galaxies.Core.Networking.Server;
using LiteNetLib;
using System;

namespace Galaxies.Core.Networking;
public class NetPlayManager
{
    public static ClientManager RomateClient { get; private set; }
    public static ServerManager RomateServer { get; private set; }
    public static void InitClient(string ip, int port)
    {

        RomateClient = new ClientManager(Main.GetInstance());
        RomateClient.Connect(ip, port);
    }
    public static void InitServer(string ip, int port)
    {
        RomateServer = new ServerManager(Main.GetInstance());
        RomateServer.StartServer(port);
    }
    public static void Update()
    {
        if (IsClient())
        {
            RomateClient.Update();
        }
        if (IsServer())
        {
            RomateServer.Update();
        }
    }
    public static void Stop()
    {
        if (IsClient())
        {
            RomateClient.Stop();
        }
        if (IsServer())
        {
            RomateServer.Stop();
        }
    }

    public static bool IsClient()
    {
        return RomateClient != null;
    }
    public static bool IsServer()
    {
        return RomateServer != null;
    }
    public static void SendToServer(C2SPacket packet)
    {
        RomateClient?.SendToServer(packet);
    }

    public static void SendToClient(S2CPacket packet, NetPeer target)
    {
        RomateServer?.SendToClient(packet, target);
    }
    public static void SendToAllClients(S2CPacket packet)
    {
        RomateServer?.SendToAllClients(packet);
    }

    internal static bool IsInit()
    {
        return RomateClient != null || RomateServer != null;
    }
}

