﻿using Galaxies.Core.Networking.Packet.C2S;
using Galaxies.Core.Networking.Packet.S2C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Networking.Packet;
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
        //join game
        Register(0, typeof(C2SLoginGamePacket));
        Register(1, typeof(S2CJoinWorldPacket));
        Register(2, typeof(S2CWorldDataPacket));

        //playe
        Register(3, typeof(C2SPlayerDiggingPacket));
        Register(4, typeof(S2CTileChangePacket));
        Register(5, typeof(C2SPlayerMovePacket));
        Register(6, typeof(C2SUseItemPacket));
    }
}
