using Galaxias.Core.Networking.Packet.C2S;
using Galaxias.Core.Networking.Packet.S2C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking.Packet;
public class PacketManager
{
    private static readonly Dictionary<int, Type> intToPacket = [];
    private static readonly Dictionary<Type, int> packetToInt = [];

    private static void Register(int id, Type type)
    {
        intToPacket[id] = type;
        packetToInt[type] = id;
    }

    internal static int GetId(Type packeType)
    {
        return packetToInt[packeType];
    }
    internal static IPacket GetPacket(int id)
    {
        return Activator.CreateInstance(intToPacket[id]) as IPacket;
    }

    static PacketManager()
    {
        Register(0, typeof(C2SLoginGamePacket));
        Register(1, typeof(S2CJoinWorldPacket));
    }
}
