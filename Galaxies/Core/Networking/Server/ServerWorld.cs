using Galaxies.Client;
using Galaxies.Core.Data;
using Galaxies.Core.World;
using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Entities.Monsters;
using Galaxies.Core.World.Tiles;
using Galaxies.Core.World.Tiles.Entity;
using Galaxies.Util;
using Galaxies.Utill;
using LiteNetLib;
using MonoGame.Framework.Utilities.Deflate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Galaxies.Core.Networking.Server;
public class ServerWorld : AbstractWorld
{
    private DirectoryInfo worldDirectory;
    private DirectoryInfo playerDirectory;
    public ServerWorld(DirectoryInfo directoryInfo, IWorldListener listener) : base(false, listener)
    {
        worldDirectory = directoryInfo;
        playerDirectory = new DirectoryInfo(directoryInfo + "players\\");
        if (!LoadData())
        {
            Generate();
        }
    }

    public override void SaveData()
    {
        Log.Info("Save worlds at:" + worldDirectory);
        if (!worldDirectory.Exists)
        {
            worldDirectory.Create();
        }
        if (!playerDirectory.Exists)
        {
            playerDirectory.Create();
        }
        Stopwatch stopwatch = Stopwatch.StartNew();

        DataSet worldData = new();
        worldData.PutFloat("time", currnetTime);
        WriteTileData(out var tileData, out var skyLight, out var tileLight);
        worldData.PutIntArray("tileData", tileData);
        worldData.PutByteArray("skyLight", skyLight);
        worldData.PutByteArray("tileLight", tileLight);

        List<DataSet> tileEntityDatas = [];
        foreach (TileEntity tile in tileEntityGrid.Values)
        {
            
            DataSet tileSet = new();
            tileSet.PutInt("x", tile.Pos.X);
            tileSet.PutInt("y", tile.Pos.Y);
            tileSet.PutInt("layer", (int)tile.Pos.Layer);
            tile.Save(tileSet);
            tileEntityDatas.Add(tileSet);
        }
        worldData.PutList("tileEntities", tileEntityDatas);

        DataUtils.WriteDataSet(worldData, worldDirectory + "world.dat");

        
        foreach (var player in players)
        {
            var dataSet = new DataSet();
            dataSet.PutFloat("x", player.X);
            dataSet.PutFloat("y", player.Y);
            DataUtils.WriteDataSet(dataSet, playerDirectory.FullName + player.Id + ".dat");
        }

        stopwatch.Stop();
        Log.Info("Take " + stopwatch.ElapsedMilliseconds + "ms to save world");
    }
    public bool LoadData()
    {
        if (!File.Exists(worldDirectory + "world.dat"))
        {
            return false;
        }
        else
        {
            Log.Info("Load world");
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                isGenerated = true;
                
                DataUtils.ReadDataSet(out var worldData, worldDirectory + "world.dat");
                currnetTime = worldData.GetData<float>("time");
                var tileData = worldData.GetData<int[]>("tileData");
                var skyLight = worldData.GetData<byte[]>("skyLight");
                var tileLight = worldData.GetData<byte[]>("tileLight");
                ReadTileData(tileData, skyLight, tileLight);

                List<DataSet> tileEntityDatas = worldData.GetData<List<DataSet>>("tileEntities");
                tileEntityDatas.ForEach(data =>
                {
                    var x = data.GetData<int>("x");
                    var y = data.GetData<int>("y");
                    var layer = data.GetData<int>("layer");
                    TilePos pos = new(x, y, (TileLayer)layer);
                    var tileEntity = GetTileEntity(pos);
                    tileEntity.Read(data);
                });
                isGenerated = false;
                stopwatch.Stop();
                Log.Info("Take " + stopwatch.ElapsedMilliseconds + "ms to load world");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Can't Load World data", ex);
                return false;
            }

        }
    }

    public override AbstractPlayerEntity CreatePlayer(NetPeer peer, Guid id)
    {
        AbstractPlayerEntity player = null;
        FileInfo playerFile = new FileInfo(playerDirectory.FullName + id + ".dat");
        if (playerFile.Exists)
        {
            player = LoadPlayer(playerFile, id, peer);
            Log.Info("Loading player " + " with unique id " + id + '!');
        }
        else
        {
            player = MakePlayer(peer, id);
        }
        return player;
    }

    private AbstractPlayerEntity LoadPlayer(FileInfo file, Guid id, NetPeer peer)
    {
        DataUtils.ReadDataSet(out var dataSet, file.FullName);
        float x = dataSet.GetData<float>("x");
        float y = dataSet.GetData<float>("y");
        var player = MakePlayer(peer, id);
        player.SetPos(x, y);
        return player;
    }
    private AbstractPlayerEntity MakePlayer(NetPeer peer, Guid id)
    {
        if (peer == null)
        {
            return new PlayerEntity(this, id);
        }
        else
        {
            return new ConnectPlayer(this, peer, id);
        }
    }
}
