namespace Galaxias.Util;
public class Direction
{
    public static readonly Direction None = new(0, 0);
    public static readonly Direction Left = new(-1, 0);
    public static readonly Direction Right = new(1, 0);
    public static readonly Direction Up = new(0, 1);
    public static readonly Direction Down = new(0, -1);
    public static readonly Direction LeftUp = new(-1, 1);
    public static readonly Direction LeftDown = new(-1, -1);
    public static readonly Direction RightUp = new(1, 1); 
    public static readonly Direction RightDown = new(1, -1);
    public static readonly Direction[] Surrounding = {LeftUp, Up, RightUp, Right, RightDown, Down, LeftDown, Left};
    public static readonly Direction[] Adjacent = {Up, Right, Down, Left};
    public static readonly Direction[] SurroundingIncludNone = {None, LeftUp, Up, RightUp, Right, RightDown, Down, LeftDown, Left};
    public int X { get; private set; }
    public int Y { get; private set; }
    private Direction (int x, int y)
    {
        X = x;
        Y = y;
    }


}
