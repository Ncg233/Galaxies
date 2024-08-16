using Galaxias.Client.Render;
using Galaxias.Core.Networking.Packet;
using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking;
public class NetWorkingInterface
{
    protected readonly EventBasedNetListener Listener = new();
    protected readonly NetManager Manager;
    public NetPeer? Server { get; private set; }

    protected NetWorkingInterface()
    {

        Manager = new NetManager(Listener);
        Listener.NetworkReceiveEvent += ReceivePacket;
    }
    
    public void Connect(string address, int port, string key = "key")
    {
        if (Server != null)
        {
            return;
        }
        Console.WriteLine("Connecting...");
        Manager.Start();
        Server = Manager.Connect(address, port, key);
    }
    public void Stop()
    {
        Manager.Stop();
    }
    public void Update()
    {
        Manager.PollEvents();
    }
    public void SendPacket(IPacket packet, NetPeer target, DeliveryMethod method = DeliveryMethod.ReliableUnordered)
    {
        NetDataWriter writer = new();
        writer.Put(PacketManager.GetId(packet.GetType()));
        writer.Put(packet);

        target.Send(writer, method);
    }
    public void ReceivePacket(NetPeer peer, NetDataReader reader, byte channelNumber, DeliveryMethod method) {
        //Console.WriteLine($"Received packet {peer.Id}");
        int packetId = reader.GetInt();
        var packet = PacketManager.GetPacket(packetId);
        packet.Deserialize(reader);
        
        packet.Process(peer);
    }
}
