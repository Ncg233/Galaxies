namespace Galaxies.Core.World.Entities;
public class HitBox
{
    public float minX { get; private set; }
    public float minY { get; private set; }
    public float maxX { get; private set; }
    public float maxY { get; private set; }
    public static HitBox Empty()
    {
        return new HitBox(0, 0, 0, 0);
    }
    public HitBox(float minX, float minY, float maxX, float maxY)
    {
        this.minX = minX;
        this.minY = minY;
        this.maxX = maxX;
        this.maxY = maxY;
    }
    public bool IsEmpty()
    {
        return GetWidth() <= 0 || GetHeight() <= 0;
    }
    public double GetWidth()
    {
        return maxX - minX;
    }

    public double GetHeight()
    {
        return maxY - minY;
    }
    public HitBox Copy()
    {
        return new HitBox(minX, minY, maxX, maxY);
    }
    public HitBox Add(float x, float y)
    {
        minX += x;
        maxX += x;
        minY += y;
        maxY += y;
        return this;
    }
    public bool intersects(float minX, float minY, float maxX, float maxY)
    {
        return this.minX < maxX && this.maxX > minX && this.minY < maxY && this.maxY > minY;
    }
    public bool intersectsX(HitBox hitBox)
    {
        return maxX > hitBox.minX && minX < hitBox.maxX;
    }
    public bool intersectsY(HitBox hitBox)
    {
        return maxY > hitBox.minY && minY < hitBox.maxY;
    }
}