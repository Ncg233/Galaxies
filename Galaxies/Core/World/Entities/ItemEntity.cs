using Galaxies.Client.Render;
using Galaxies.Core.World.Items;
using Microsoft.Xna.Framework;

namespace Galaxies.Core.World.Entities;
public class ItemEntity : Entity
{
    private static ItemEntityRenderer EntityRenderer = new ItemEntityRenderer();
    public ItemPile pile;
    public ItemEntity(AbstractWorld world, ItemPile pile, float x, float y) : base(world)
    {
        this.pile = pile;
        SetPos(x, y);
    }
    public override void Render(IntegrationRenderer renderer, Color light)
    {
        EntityRenderer.Render(renderer,this, 1, light);
    }
    public override void OnCollideWithEntity(Entity otherEntity)
    {  
        if (!world.IsClient)
        {
            if (otherEntity is AbstractPlayerEntity)
            {
                SetDead();
            }
        }   
    }
    public class ItemEntityRenderer : EntityRenderer<ItemEntity>
    {
        public override void Render(IntegrationRenderer renderer, ItemEntity entity, float scale, Color colors)
        {
            ItemRenderer.RenderInWorld(renderer, entity.pile, entity.GetRenderX(), entity.GetRenderY(), scale, colors);
        }
    }
}
