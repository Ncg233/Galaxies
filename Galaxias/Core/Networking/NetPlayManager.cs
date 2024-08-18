using Galaxias.Client.Main;
using Galaxias.Core.Networking.Packet;
using Galaxias.Core.Networking.Packet.C2S;
using Galaxias.Server;
using LiteNetLib;
using System;

namespace Galaxias.Core.Networking;
public class NetPlayManager
{
    public static readonly NetPlayManager Instance = new();
    public Client RomateClient {get; private set;}
    public Server RomateServer {get; private set;}
    public void InitClient(string ip, int port)
    {
        RomateClient = new Client(GalaxiasClient.GetInstance());
        RomateClient.Connect(ip, port);
    }
    public void InitServer(string ip, int port, GalaxiasServer server)
    {
        RomateServer = new Server(server);
        RomateServer.StartServer(port);

        RomateClient = new Client(GalaxiasClient.GetInstance());
        RomateClient.Connect(ip, port);
    }
    public void UpdateClient()
    {
        RomateClient?.Update();
    }
    public void UpdateServer()
    {
        RomateServer?.Update();
    }

    internal void StopServer()
    {
        RomateServer?.Stop();
    }
    internal void StopClient()
    {
        RomateClient?.Stop();
    }

    public void SendToServer(IPacket packet)
    {
        RomateClient.SendToServer(packet);
    }

    public void SendToClient(IPacket packet,NetPeer target)
    {
        RomateServer.SendPacket(packet, target);
    }
}

