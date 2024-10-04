

namespace Galaxies.Core.Networking.Server;
public class GalaxiasServer
{
    //private Dictionary<PlanetType, ServerWorld> worlds = [];
    //private readonly List<ServerPlayer> players = new(128);
    //private Thread serverThread;
    //private bool isServerRunning = false;
    //private long serverTime;
    //DirectoryInfo rootPath = new DirectoryInfo("C:\\Users\\MC\\Documents\\.galaxias\\");
    //public GalaxiasServer()
    //{
    //}
    //
    //private void LoadAllWorlds()
    //{
    //    ServerWorld world = new ServerWorld(rootPath);
    //    if (!world.LoadData())
    //    {
    //        Log.Info("Generat World");
    //        world.Generate();
    //    }
    //    worlds[PlanetType.Earth] = world;
    //}
    //
    //public void StartServerThread()
    //{
    //    serverThread = new Thread(Run);
    //    serverThread.Name = "Server";
    //    isServerRunning = true;
    //    
    //    serverThread.Start();
    //}
    //public void ServerShutdown()
    //{
    //    isServerRunning = false;
    //    
    //}
    //public void Run()
    //{
    //    LoadAllWorlds();
    //    //serverTime = 0;
    //    while (isServerRunning)
    //    {
    //        Update();
    //        Thread.Sleep(50);
    //
    //    }
    //    StopServer();
    //}
    //private void Update()
    //{
    //    NetPlayManager.UpdateServer();
    //    foreach (var world in worlds.Values)
    //    {
    //        world.Update(1 / 20f);
    //    }
    //}
    //public void StopServer()
    //{
    //    NetPlayManager.StopServer();
    //    SaveData();
    //}
    //
    //private void SaveData()
    //{
    //    Log.Info("Save worlds at:" + rootPath);
    //    foreach (var item in worlds)
    //    {
    //        item.Value.SaveData();
    //    }
    //}
    //
    //public void InitConnectionPlayer(NetPeer peer)
    //{
    //    Log.Info("Prepare player");
    //    var world = worlds[PlanetType.Earth];
    //    var player = new ServerPlayer(world, peer);
    //    
    //    player.SendPacket(new S2CJoinWorldPacket());
    //    player.SendPacket(new S2CWorldDataPacket(world));
    //
    //    player.SendPacket(new S2CTimeSyncPacket(world.currnetTime));
    //    players.Add(player);
    //
    //}
    //
    //public ServerPlayer GetPlayer(int id)
    //{
    //    return players[id];
    //}
}