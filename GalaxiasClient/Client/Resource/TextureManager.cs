using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ClientGalaxias.Client.Resource;
public class TextureManager
{
    private ContentManager contentManager;
    private Dictionary<string, Texture2D> textureDic = new Dictionary<string, Texture2D>();
    public TextureManager(ContentManager contentManager)
    {
        this.contentManager = contentManager;
    }
    public Texture2D LoadTexture2D(string path)
    {
        var texture = textureDic.GetValueOrDefault(path, null);
        if (texture == null)
        {
            texture = contentManager.Load<Texture2D>(path);
        }
        return texture;
    }
}