using Galaxias.Client.Main;
using Galaxias.Core.Networking.Packet;
using Galaxias.Core.Networking.Packet.C2S;
using Galaxias.Server;
using LiteNetLib;
using System;

namespace Galaxias.Core.Networking;
public class NetPlayManager
{
    public static Client RomateClient {get; private set;}
    public static Server RomateServer {get; private set;}
    public static void InitClient(string ip, int port)
    {
        RomateClient = new Client();
        RomateClient.Connect(ip, port);
    }
    public static void InitServer(string ip, int port, GalaxiasServer server)
    {
        RomateServer = new Server(server);
        RomateServer.StartServer(port);

        RomateClient = new Client();
        RomateClient.Connect(ip, port);
    }
    public static void UpdateClient()
    {
        RomateClient?.Update();
    }
    public static void UpdateServer()
    {
        RomateServer?.Update();
    }

    internal static void StopServer()
    {
        RomateServer?.Stop();
    }
    internal static void StopClient()
    {
        RomateClient?.Stop();
    }

    public static void SendToServer(C2SPacket packet)
    {
        RomateClient.SendToServer(packet);
    }

    public static void SendToClient(IPacket packet,NetPeer target)
    {
        RomateServer.SendPacket(packet, target);
    }
    public static void SendToAllClients(IPacket packet)
    {
        RomateServer.SendToAllClients(packet);
    }
}

