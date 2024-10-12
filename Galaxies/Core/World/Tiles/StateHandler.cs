using Galaxies.Util;
using System.Collections.Generic;

namespace Galaxies.Core.World.Tiles;
public class StateHandler
{
    private readonly Tile tile;
    private readonly List<string> tileProps = [];
    private readonly List<Facing> facings = [];

    private readonly TileState defaultState;
    private readonly Table<string, Facing, TileState> subState = new Table<string, Facing, TileState>();
    private readonly List<TileState> allState = [];
    public StateHandler(Tile tile)
    {
        this.tile = tile;
        
        tileProps.Add("default");
        tile.AddProp(this);
        if (facings.Count == 0)
        {
            facings.Add(Facing.None);
        }
        //defaultState = new TileState(this.tile, "default");
        //
        //subState.Add("default", defaultState);
        //allState.Add(defaultState);

        foreach (var prop in tileProps)
        {
            foreach (var facing in facings)
            {
                TileState s = new(tile, prop, facing);
                subState.Put(prop, facing, s);
                allState.Add(s);
            }
            
        }
        defaultState = allState[0];

    }

    public TileState GetDefaultState()
    {
        return defaultState;
    }
    public void AddProp(string prop)
    {
        tileProps.Add(prop);
    }
    public void AddFacing(Facing facing)
    {
        facings.Add(facing);
    }
    public TileState GetState(string prop, Facing facing)
    {
        return subState.Get(prop, facing);
    }

    public List<TileState> GetAllState()
    {
        return allState;
    }
}
