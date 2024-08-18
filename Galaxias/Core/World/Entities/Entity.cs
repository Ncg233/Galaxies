using Galaxias.Client.Render;
using Galaxias.Core.World.Tiles;
using Galaxias.Util;
using System.Collections.Generic;

namespace Galaxias.Core.World.Entities;
public abstract class Entity
{
    public readonly EntityType Type;
    private float friction = 0.5f;
    public float x { get; protected set; }
    public float y { get; protected set; }
    public float vx { get; protected set; }
    public float vy { get; protected set; }
    public Direction direction { get; protected set; }
    public HitBox hitbox { get; protected set; } = HitBox.Empty();
    private World world;
    public float speed;
    //private EntityRenderer renderer;
    public bool onGround;
    public bool collidedHor;
    public bool collidedVert;
    protected double lastY;
    public Entity(EntityType entity, World world)
    {
        Type = entity;
        this.world = world;
        //renderer = new EntityRenderer();
    }
    public virtual void Update(float dTime)
    {
        lastY = y;
        if (y < -20)
        {
            Die();
        }
        PreMovement(dTime);
        HandleMovement(dTime);
        HandleCollision(dTime);
        TpToOtherSide();
        if (onGround)
        {
            vy = 0;
        }
    }
    private void PreMovement(float dTime)
    {
        vx *= friction;
        if (vy > -3) vy -= GameConstants.Gravity * dTime;
    }
    public virtual void Die()
    {

    }

    protected virtual void HandleMovement(float dTime)
    {

    }
    //public EntityRenderer GetRenderer()
    //{
    //    return renderer;
    //}
    private void HandleCollision(float dTime)
    {
        float motionY = vy;
        float motionX = vx;
        HitBox ownBoxMotion = hitbox.Copy().Add(motionX, motionY);
        HitBox ownBox = hitbox;
        if (true)
        {
            List<HitBox> blockBoxes = new();
            for (int x = Utils.Floor(ownBoxMotion.minX); x < Utils.Ceil(ownBoxMotion.maxX); x++)
            {
                for (int y = Utils.Floor(ownBoxMotion.minY); y < Utils.Ceil(ownBoxMotion.maxY); y++)
                {

                    TileState id = world.GetTileState(TileLayer.Main, x, y);
                    if (id.GetTile().CanCollide())
                    {
                        blockBoxes.Add(new HitBox(x, y, x + 1, y + 1));
                    }
                }
            }

            if (motionY != 0 && blockBoxes.Count != 0)
            {
                foreach (HitBox box in blockBoxes)
                {
                    if (!box.IsEmpty() && ownBox.intersectsX(box))
                    {
                        if (motionY > 0 && ownBox.maxY <= box.minY)
                        {
                            float diff = box.minY - ownBox.maxY;
                            if (diff < motionY)
                            {
                                motionY = diff;
                            }
                        }
                        else if (motionY < 0 && ownBox.minY >= box.maxY)
                        {
                            float diff = box.maxY - ownBox.minY;
                            if (diff > motionY)
                            {
                                motionY = diff;
                            }
                        }
                    }
                }
            }
            if (motionX != 0 && blockBoxes.Count != 0)
            {
                foreach (HitBox box in blockBoxes)
                {
                    if (!box.IsEmpty() && ownBox.intersectsY(box))
                    {
                        if (motionX > 0 && ownBox.maxX <= box.minX)
                        {
                            float diff = box.minX - ownBox.maxX;
                            if (diff < motionX)
                            {
                                motionX = diff;
                            }
                        }
                        else if (motionX < 0 && ownBox.minX >= box.maxX)
                        {
                            float diff = box.maxX - ownBox.minX;
                            if (diff > motionX)
                            {
                                motionX = diff;
                            }
                        }
                    }
                }
            }
        }

        collidedVert = motionY != vy;
        collidedHor = motionX != vx;
        onGround = collidedVert && vy < 0;
        SetPos(x + motionX, y + motionY);
    }
    public void SetPos(float x, float y)
    {
        this.x = x;
        this.y = y;
        SetHitBox(MakeHitBox());
    }

    private void SetHitBox(HitBox hit)
    {
        hitbox = hit;
    }
    private HitBox MakeHitBox()
    {
        float u = GetWidth() / 2;
        float h = GetHeight();
        return new HitBox(x - u, y, x + u, y + h);
    }

    public virtual float GetWidth()
    {
        return 1;
    }
    public virtual float GetHeight()
    {
        return 1;
    }
    public World GetWorld() {
        return world;
    }
    public void TpToOtherSide()
    {
        if (x > world.GetWidth())
        {
            SetPos(x - world.GetWidth(), y);
        }
        else if (x < 0)
        {
            
            SetPos(x + world.GetWidth(), y);
        }
    }
}

