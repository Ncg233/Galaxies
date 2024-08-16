using Galaxias.Core.Networking.Packet;
using Galaxias.Core.Networking.Packet.C2S;
using LiteNetLib;
using System;

namespace Galaxias.Core.Networking;
public class NetPlayManager
{
    public static readonly NetPlayManager Instance = new();
    private Client client;
    private Server server;

    public void Init(string ip, int port, bool isServer)
    {
        if (client != null || server != null)
        {
            Console.WriteLine("Cannot init NetPlayerManager");
        }
        else
        {
            if (isServer)
            {
                server = new Server();
                server.StartServer(port);

                client = new Client();
                client.Connect(ip, port);
            }
            else
            {
                client = new Client();
                client.Connect(ip, port);
            }
        }
    }
    public void UpdateClient()
    {
        client?.Update();
    }
    public void UpdateServer()
    {
        server?.Update();
    }

    internal void StopServer()
    {
        server?.Stop();
    }
    internal void StopClient()
    {
        client?.Stop();
    }

    public void SendToServer(IPacket packet)
    {
        client.SendToServer(packet);
    }

    public void SendToClient(IPacket packet,NetPeer target)
    {
        server.SendPacket(packet, target);
    }
}

