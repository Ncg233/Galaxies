namespace Galaxias.Core.World.Entities;
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
        return new HitBox(this.minX, this.minY, this.maxX, this.maxY);
    }
    public HitBox Add(float x, float y)
    {
        this.minX += x;
        this.maxX += x;
        this.minY += y;
        this.maxY += y;
        return this;
    }
    public bool intersects(float minX, float minY, float maxX, float maxY)
    {
        return this.minX < maxX && this.maxX > minX && this.minY < maxY && this.maxY > minY;
    }
    public bool intersectsX(HitBox hitBox)
    {
        return this.maxX > hitBox.minX && this.minX < hitBox.maxX;
    }
    public bool intersectsY(HitBox hitBox)
    {
        return this.maxY > hitBox.minY && this.minY < hitBox.maxY;
    }
}