using Galaxias.Client.Main;
using Galaxias.Core.Networking.Packet;
using Galaxias.Core.Networking.Packet.C2S;
using Galaxias.Core.Networking.Packet.S2C;
using Galaxias.Server;
using LiteNetLib;
using System;

namespace Galaxias.Core.Networking;
public class NetPlayManager
{
    public static bool IsRomate { get; private set; }
    public static Client RomateClient {get; private set;}
    public static Server RomateServer {get; private set;}
    public static void InitClient(string ip, int port)
    {
        IsRomate = true;
        RomateClient = new Client();
        RomateClient.Connect(ip, port);
    }
    public static void InitServer(string ip, int port, GalaxiasServer server)
    {
        IsRomate = true;
        RomateServer = new Server(server);
        RomateServer.StartServer(port);

        RomateClient = new Client();
        RomateClient.Connect(ip, port);
    }
    public static void InitLocalServer(GalaxiasServer server)
    {
        IsRomate = false;
        RomateServer = new Server(server);
        //RomateServer.StartServer(port);

        RomateClient = new Client();
        RomateClient.Connect();
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

    public static void SendToClient(S2CPacket packet,NetPeer target)
    {
        RomateServer.SendToClient(packet, target);
    }
    public static void SendToAllClients(S2CPacket packet)
    {
        RomateServer.SendToAllClients(packet);
    }
}

