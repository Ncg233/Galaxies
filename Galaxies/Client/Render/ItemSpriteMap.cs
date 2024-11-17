
using Galaxies.Client.Resource;
using Galaxies.Core.World.Items;
using Galaxies.Util;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using System;

namespace Galaxies.Client.Render;
public class ItemSpriteMap
{
    public readonly Texture2D Texture;
    public readonly int RenderWidth;
    public readonly int Renderheight;
    public readonly float ColorMod;
    public ItemSpriteMap(Texture2D texture, int renderWidth, int renderheight, float colorMod)
    {
        Texture = texture;
        RenderWidth = renderWidth;
        Renderheight = renderheight;
        ColorMod = colorMod;
    }
    public static ItemSpriteMap Deserialize(JObject o)
    {
        //load texture
        var texture = TextureManager.LoadTexture2D(JsonUtils.GetValue<string>(o, "texture"));
        var size = JsonUtils.GetValue<int[]>(o, "renderSize");
        JsonUtils.TryGetValue(o, "color", out var colorMod, 1f);
        return new ItemSpriteMap(texture, size[0], size[1], colorMod);
    }
}