using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Util;
public class Facing
{
    public static readonly Facing None = new Facing("none", SpriteEffects.None, 0);
    public static readonly Facing Right = new Facing("right",SpriteEffects.None, 0);
    public static readonly Facing Left = new Facing("left", SpriteEffects.FlipHorizontally, 0);
    public static readonly Facing Up = new Facing("up", SpriteEffects.None, 270);
    public static readonly Facing Down = new Facing("down", SpriteEffects.None, 90);
    public string Name { get; private set; }
    public SpriteEffects Effect { get; private set; }
    public int Rotation { get; private set; }
    public Facing(string name, SpriteEffects effect, int rotation)
    {
        Name = name;
        Effect = effect;
        Rotation = rotation;
    }
    public bool IsHorTurn()
    {
        return Effect == SpriteEffects.FlipHorizontally;
    }
}
