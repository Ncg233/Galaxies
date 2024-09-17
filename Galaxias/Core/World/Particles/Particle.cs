using Galaxias.Client.Render;
using Galaxias.Core.World.Entities;
using Microsoft.Xna.Framework;

namespace Galaxias.Core.World.Particles;
public abstract class Particle : Entity
{
    protected float maxLife;
    protected float life;

    protected bool dead;

    public Particle(AbstractWorld world, float x, float y, float motionX, float motionY, float maxLife) : base(null, world)
    {
        vx = motionX;
        vy = motionY;
        this.maxLife = maxLife;

        SetPos(x, y);
    }
    public override void Update(float dTime)
    {
        base.Update(dTime);
        life += dTime;

        if (life >= maxLife)
        {
            SetDead();
        }
    }
    public override void PreMovement(float dTime)
    {
        vy -= 0.02f;

        vx *= onGround ? 0.8f : 0.98f;
        vy *= 0.99f;
    }
    public abstract void Render(IntegrationRenderer renderer, Color light);
}