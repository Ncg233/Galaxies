using Galaxies.Client;
using Galaxies.Core.World;
using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Tiles;
using Galaxies.Util;
using LiteNetLib;
using MonoGame.Framework.Utilities.Deflate;
using System;
using System.IO;

namespace Galaxies.Core.Networking.Server;
public class ServerWorld : AbstractWorld
{
    private DirectoryInfo worldDirectory;
    public ServerWorld(DirectoryInfo directoryInfo, IWorldListener listener) : base(false, listener)
    {
        worldDirectory = directoryInfo;
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
        var fileStream = new FileStream(worldDirectory + "world.dat", FileMode.OpenOrCreate, FileAccess.Write);
        using var compressor = new GZipStream(fileStream, CompressionMode.Compress);
        BinaryWriter binaryWriter = new BinaryWriter(compressor);
        binaryWriter.Write(currnetTime);
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                binaryWriter.Write((byte)Tile.TileStateId.Get(GetTileState(TileLayer.Background, x, y)));
                binaryWriter.Write((byte)Tile.TileStateId.Get(GetTileState(TileLayer.Main, x, y)));
                binaryWriter.Write(GetSkyLight(x, y));
                binaryWriter.Write(GetTileLight(x, y));
            }
        }
        binaryWriter.Close();
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
            var fileStream = new FileStream(worldDirectory + "world.dat", FileMode.OpenOrCreate, FileAccess.Read);
            using var decompressor = new GZipStream(fileStream, CompressionMode.Decompress);
            BinaryReader binaryReader = new BinaryReader(decompressor);
            try
            {
                isGenerated = true;
                currnetTime = binaryReader.ReadSingle();
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        SetTileState(TileLayer.Background, x, y, Tile.TileStateId.Get(binaryReader.ReadByte()));
                        SetTileState(TileLayer.Main, x, y, Tile.TileStateId.Get(binaryReader.ReadByte()));
                        SetSkyLight(x, y, binaryReader.ReadByte());
                        SetTileLight(x, y, binaryReader.ReadByte());
                    }
                }
                binaryReader.Close();
                isGenerated = false;
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Can't Load World data", ex);
                binaryReader.Close();
                return false;
            }

        }
    }

    public override AbstractPlayerEntity CreatePlayer(NetPeer peer)
    {
        if (peer == null)
        {
            return new PlayerEntity(this);
        }
        else
        {
            return new ConnectPlayer(this, peer);
        }
    }
}
