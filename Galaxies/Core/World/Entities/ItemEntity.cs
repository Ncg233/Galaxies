using Galaxies.Client.Render;
using Galaxies.Core.World.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Entities;
public class ItemEntity : Entity
{
    private static ItemEntityRenderer EntityRenderer = new ItemEntityRenderer();
    public ItemPile pile;
    public ItemEntity(EntityType entity, AbstractWorld world, ItemPile pile, float x, float y) : base(entity, world)
    {
        this.pile = pile;
        SetPos(x, y);
    }
    public override void Render(IntegrationRenderer renderer, Color light)
    {
        EntityRenderer.Render(renderer,this, 1, light);
    }

}
