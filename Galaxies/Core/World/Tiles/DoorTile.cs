using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Tiles;
public class DoorTile : FurnitureTile
{
    private static readonly List<HitBox> closeBox = [new HitBox(0, 0, 1, 4)];
    private static readonly List<HitBox> openBox = [new HitBox(0, 0, 0, 0)];
    public DoorTile() : base(2, 4)
    {

    }
    public override void AddProp(StateHandler handler)
    {
        base.AddProp(handler);
        handler.AddProp("close");
        handler.AddProp("open");
    }
    public override void OnUse(TileState tileState, AbstractWorld world, int x, int y)
    {
        if(tileState.GetState() == "close")
        {
            world.SetTileState(TileLayer.Main, x, y, tileState.ChangeState("open"));
        }else
        {
            world.SetTileState(TileLayer.Main, x, y, tileState.ChangeState("close"));
        }
        
    }
    public override bool CanCollide()
    {
        return true;
    }
    public override List<HitBox> GetHitBoxes(TileState tileState)
    {
        if(tileState.GetState() == "close")
        {
            return closeBox;
        }else
        {
            return openBox;
        }
    }
}
