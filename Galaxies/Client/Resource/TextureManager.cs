using Galaxies.Client;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Galaxies.Client.Resource;
public class TextureManager
{
    private static readonly Dictionary<string, Texture2D> textureDic = [];
    public static Texture2D BlankTexture { get; private set; }

    public static void LoadContent()
    {
        BlankTexture = LoadTexture2D("Textures/Misc/blank");
    }
    public static Texture2D LoadTexture2D(string path)
    {
        var texture = textureDic.GetValueOrDefault(path, null);
        texture ??= Main.GetInstance().Content.Load<Texture2D>("Assets/" + path);
        return texture;
    }
}