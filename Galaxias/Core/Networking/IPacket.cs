﻿using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking;
public interface IPacket : INetSerializable
{
    public void Process();

}
