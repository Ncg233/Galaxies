using Galaxias.Client;
using Galaxias.Client.Render;
using Galaxias.Core.Networking;
using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using LiteNetLib;
using System;

namespace Galaxias.Client;
public class ClientWorld : AbstractWorld
{
    private WorldRenderer renderer;
    //private readonly List<ParticleType> particles = [];
    public ClientWorld(WorldRenderer renderer) : base(true)
    {
        this.renderer = renderer;
    }

    public override AbstractPlayerEntity CreatePlayer(NetPeer peer)
    {
        if (peer != null)
        {
            throw new Exception("Cannot create a connected player in a client world");
        }
        else
        {
            return new ClientPlayer(this);
        }
    }
}
