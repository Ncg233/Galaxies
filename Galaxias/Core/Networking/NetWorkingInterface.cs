using Galaxias.Client.Render;
using Galaxias.Core.Networking.Packet;
using Galaxias.Core.Networking.Packet.C2S;
using Galaxias.Core.Networking.Packet.S2C;
using Galaxias.Util;
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

    protected NetWorkingInterface()
    {
        Manager = new NetManager(Listener);
        Listener.NetworkReceiveEvent += ReceivePacket;
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
        if (packet is C2SPacket c2spacket)
        {
            c2spacket.Process(NetPlayManager.Instance.RomateServer);
        }
        else if (packet is S2CPacket s2cpacket)
        {
            
            s2cpacket.Process(NetPlayManager.Instance.RomateClient);
            
        }else
        {
            Log.Error("Bad Packet");
        }
    }
}
