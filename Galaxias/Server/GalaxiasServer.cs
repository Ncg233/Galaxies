using Galaxias.Core.Networking;
using Galaxias.Core.Networking.Packet.S2C;
using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using Galaxias.Core.World.Planets;
using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxias.Server;
public class GalaxiasServer
{
    private Dictionary<PlanetType, ServerWorld> worlds = [];
    private readonly List<ServerPlayer> players = new(128);
    private Thread serverThread;
    private bool isServerRunning = false;
    private int _continueRun = 1;
    public GalaxiasServer()
    {
        LoadAllWorlds();
    }

    private void LoadAllWorlds()
    {
        worlds[PlanetType.Earth] = new ServerWorld();
    }

    public void StartServerThread()
    {
        serverThread = new Thread(Run);
        serverThread.Name = "Server";
        isServerRunning = true;
        
        serverThread.Start();
    }
    public void StopServer()
    {
        isServerRunning = false;
        Interlocked.Exchange(ref _continueRun, 0);
        NetPlayManager.StopServer();
    }
    public void Run()
    {
        while (Interlocked.Exchange(ref _continueRun, 1) == 1)
        {
            NetPlayManager.UpdateServer();
            Thread.Sleep(15);

        }
    }

    public void InitConnectionPlayer(NetPeer peer)
    {
        var world = worlds[PlanetType.Earth];
        var player = new ServerPlayer(world, peer);
        players.Add(player);
        player.SendPacket(new S2CJoinWorldPacket());
        player.SendPacket(new S2CWorldDataPacket(world));

    }

    public ServerPlayer GetPlayer(int id)
    {
        return players[id];
    }
}