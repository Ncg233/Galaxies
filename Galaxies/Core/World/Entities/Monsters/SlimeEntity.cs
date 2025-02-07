using Galaxies.Client.Render;
using Galaxies.Core.World.Entities.Spawn;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Entities.Monsters;
public class SlimeEntity : LivingEntity
{
    private static readonly SlimeRenderer slimeEntityRenderer = new();
    public static readonly ISpawnBehaviour<SlimeEntity> SpawnBehaviour = new SlimeSpawnBehaviour();
    public SlimeEntity(AbstractWorld world) : base(world)
    {
        health = maxHealth = 50;
    }

    public override void Render(IntegrationRenderer renderer, Color color)
    {
        slimeEntityRenderer.Render(renderer, this, 1, color);
    }
    private class SlimeSpawnBehaviour : ISpawnBehaviour<SlimeEntity>
    {
        public SlimeEntity CreateEntity(AbstractWorld world, float x, float y)
        {
            var slime = new SlimeEntity(world);
            slime.SetPos(x, y);
            return slime;
        }

        public float GetSpawnFrequency(AbstractWorld world)
        {
            return 2;
        }
    }
    public class SlimeRenderer : EntityRenderer<SlimeEntity>
    { 
        public override void Render(IntegrationRenderer renderer, SlimeEntity entity, float scale, Color colors)
        {
            var x = entity.GetRenderX();
            var y = entity.GetRenderY();
            var width = 16;
            var height = 16;
            renderer.Draw("Textures/Entities/Slime/greenslime_1", x, y,
                width / 2f, height, colors);
        }
    }
}
