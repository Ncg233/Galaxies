using System.Collections.Generic;
using ClientGalaxias.Client.Resource;
using Galaxias.Core.World.Entities;
using Galaxias.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ClientGalaxias.Client.Render;
public class EntityRenderer
{
    //private readonly Dictionary<Entity, Texture2D> entityToTexture = new Dictionary<Entity, Texture2D>();
    //public void LoadContent(TextureManager manager)
    //{
        //foreach (var entityId in AllEntities.entityRegister)
        //{
            //entityToTexture.Add(new Player(null), manager.LoadTexture2D("Textures/Entities/" + "player"));
        //}
    //}
    
    public void Render(IntegrationRenderer renderer, float renderX, float renderY, float width, float height,Entity entity, int scale, Color colors)
    {
        //Texture2D entityTexture = entityToTexture.GetValueOrDefault(entity);
        //if(entityTexture != null)
        renderer.Draw("Textures/Entities/" + "player", renderX - width * scale / 2, renderY - height * scale, colors,
            effects: entity.direction == Direction.Left ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
                
    }
}
