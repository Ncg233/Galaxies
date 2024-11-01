using Galaxies.Core.World.Tiles;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Galaxies.Core.World.Gen;
public class TreeGen(int seed, Random random) : AbstractWorldGen(seed, random)
{
    private readonly Dictionary<int, bool> treePos = [];

    public override void Generate(AbstractWorld world)
    {
        Random random = new Random();
        for (int x = 0; x < world.Width; x++)
        {
            for (int y = (int)world.GetGenSuerfaceHeight(TileLayer.Main, x); y < GameConstants.ChunkHeight; y++)
            {
                var state = world.GetTileState(TileLayer.Main, x, y - 1);
                if (random.NextDouble() < 0.7f && state != null && state.GetTile() == AllTiles.GrassTile && world.GetTileState(TileLayer.Main, x, y).IsAir())
                {
                    world.SetTileState(TileLayer.Main, x, y, AllTiles.Grass.GetDefaultState());
                }
                if (random.NextDouble() < 0.1f && state != null && state.GetTile() == AllTiles.GrassTile && !HasTree(x))
                {
                    PlaceTree(x, y, world);
                    continue;
                }

            }
        }
    }
    //private bool CanTreeGrow()
    //{
    //
    //}
    private void PlaceTree(int x, int y, AbstractWorld world)
    {
        var height = GetHeight(random);
        for (int ly = y; ly < y + height; ly++)
        {
            world.SetTileState(TileLayer.Main, x, ly, AllTiles.Log.GetDefaultState());
        }
        if (true)
        {
            world.SetTileState(TileLayer.Main, x, y + height, AllTiles.Leaves.GetDefaultState());
        }
        treePos[x] = true;
    }
    private bool HasTree(int x)
    {
        foreach (var pair in treePos)
        {
            if (Math.Pow(pair.Key - x, 2) < 16) return true;
        }
        return false;
    }
    private int GetHeight(Random random)
    {
        return random.Next(6, 10);
    }

}
