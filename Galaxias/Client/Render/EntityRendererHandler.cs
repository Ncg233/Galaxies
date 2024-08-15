using Galaxias.Core.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Client.Render;
public class EntityRendererHandler
{
    private static readonly Dictionary<EntityType, EntityRenderer> typeToRenderer = [];
    static EntityRendererHandler() {
        typeToRenderer.Add(AllEntityTypes.PlayerEntity, new PlayerRenderer());
    }
    public static EntityRenderer GetRenderer(EntityType entityType) { 
        return typeToRenderer[entityType];
    }
}
