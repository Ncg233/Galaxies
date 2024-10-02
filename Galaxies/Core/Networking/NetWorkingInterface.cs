using Galaxies.Core.Networking.Packet;
using Galaxies.Core.Networking.Packet.C2S;
using Galaxies.Core.Networking.Packet.S2C;
using Galaxies.Util;
using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Networking;
public class NetWorkingInterface
{
    //this is used for single mode
    public static List<C2SPacket> localC2SPacket = [];
    public static List<S2CPacket> localS2CPacket = [];

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
    public void SendLoaclPacket(IPacket packet, NetWorkingInterface bound)
    {
        bound.ReciveLocalPacket(packet);
    }
    public void ReceivePacket(NetPeer peer, NetDataReader reader, byte channelNumber, DeliveryMethod method)
    {
        int packetId = reader.GetInt();
        var packet = PacketManager.GetPacket(packetId);
        packet.Deserialize(reader);
        if (packet is C2SPacket c2spacket)
        {
            c2spacket._id = peer.Id;
            c2spacket.Process(NetPlayManager.RomateServer);
        }
        else if (packet is S2CPacket s2cpacket)
        {
            s2cpacket.Process(NetPlayManager.RomateClient);

        }
        else
        {
            Log.Error("Bad Packet");
        }
    }
    public void ReciveLocalPacket(IPacket packet)
    {
        if (packet is C2SPacket c2spacket)
        {
            c2spacket._id = 0;
            localC2SPacket.Add(c2spacket);
        }
        else if (packet is S2CPacket s2cpacket)
        {
            localS2CPacket.Add(s2cpacket);

        }
        else
        {
            Log.Error("Bad Packet");
        }
    }
}
