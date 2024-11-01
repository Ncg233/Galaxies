using Galaxies.Client.Render;
using Galaxies.Core.World;
using Galaxies.Core.World.Entities;
using LiteNetLib;
using System;

namespace Galaxies.Core.Networking.Client;
public class ClientWorld : AbstractWorld
{
    //private readonly List<ParticleType> particles = [];
    public ClientWorld(IWorldListener listener) : base(true, listener)
    {
    }

    public override AbstractPlayerEntity CreatePlayer(NetPeer peer, Guid id)
    {
        if (peer != null)
        {
            throw new Exception("Cannot create a connected player in a client world");
        }
        else
        {
            return new ClientPlayer(this, id);
        }
    }
}
