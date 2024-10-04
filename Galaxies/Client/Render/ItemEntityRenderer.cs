using Galaxies.Core.World.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Render;
public class ItemEntityRenderer : EntityRenderer<ItemEntity>
{
    public override void LoadContent()
    {
        
    }

    public override void Render(IntegrationRenderer renderer, ItemEntity entity, int scale, Color colors)
    {
        ItemRenderer.RenderInWorld(renderer, entity.pile, entity.GetRenderX(), entity.GetRenderY(), colors);
    }
}

