using System.Collections.Generic;

namespace Galaxies.Core.World.Tiles;
public class StateHandler
{
    private readonly Tile tile;
    private readonly List<string> tileProps = [];

    private readonly TileState defaultState;
    private readonly Dictionary<string, TileState> subState = [];
    private readonly List<TileState> allState = [];
    public StateHandler(Tile tile)
    {
        this.tile = tile;

        defaultState = new TileState(this.tile, "default");

        subState.Add("default", defaultState);
        allState.Add(defaultState);
        foreach (var prop in tileProps)
        {
            TileState s = new(tile, prop);
            subState.Add(prop, s);
            allState.Add(s);
        }

    }

    public TileState GetDefaultState()
    {
        return defaultState;
    }
    public void AddProp(string prop)
    {
        tileProps.Add(prop);
    }
    public TileState GetState(string prop)
    {
        return subState.GetValueOrDefault(prop, defaultState);
    }

    public List<TileState> GetAllState()
    {
        return allState;
    }
}
