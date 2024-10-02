using Galaxies.Client.Render;
using Galaxies.Core.Networking;
using Galaxies.Core.World;
using Galaxies.Core.World.Entities;
using LiteNetLib;
using System;

namespace Galaxies.Client;
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
