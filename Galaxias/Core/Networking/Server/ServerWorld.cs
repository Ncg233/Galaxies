using Galaxias.Client;
using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using Galaxias.Core.World.Gen;
using Galaxias.Core.World.Tiles;
using Galaxias.Util;
using LiteNetLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Galaxias.Core.Networking.Server;
public class ServerWorld : AbstractWorld
{
    private DirectoryInfo worldDirectory;
    public ServerWorld(DirectoryInfo directoryInfo) : base(false)
    {
        worldDirectory = directoryInfo;
        LoadData();
    }

    public override void SaveData()
    {
        Log.Info("Save worlds at:" + worldDirectory);
        if (!worldDirectory.Exists)
        {
            worldDirectory.Create();
        }
        BinaryWriter binaryWriter = new BinaryWriter(new FileStream(worldDirectory + "world.dat", FileMode.OpenOrCreate, FileAccess.Write));
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
            BinaryReader binaryReader = new BinaryReader(new FileStream(worldDirectory + "world.dat", FileMode.OpenOrCreate, FileAccess.Read));
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
