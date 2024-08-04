﻿using ClientGalaxias.Client.Main;
using ClientGalaxias.Client.Resource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ClientGalaxias.Client.Render;
public class IntegrationRenderer
{
    private SpriteBatch spriteBatch;
    private TextureManager textureManager;
    private SpriteFont spriteFont;
    public IntegrationRenderer(TextureManager textureManager)
    {
        this.textureManager = textureManager;
    }
    public void LoadContents()
    {
        spriteBatch = new SpriteBatch(GalaxiasClient.GetInstance().GraphicsDevice);
        spriteFont = GalaxiasClient.GetInstance().Content.Load<SpriteFont>("Assets/Fonts/defaultFont");

    }
    public void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null, SamplerState samplerState = null, DepthStencilState depthStencilState = null, RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = null)
    {
        spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
    }

    public void Draw(string textureName, Rectangle rect, Color color, Rectangle? source = null, SpriteEffects effects = SpriteEffects.None)
    {
        spriteBatch.Draw(textureManager.LoadTexture2D(textureName), rect, source, color, 0, Vector2.Zero, effects, 0);
    }
    public void Draw(string textureName, float x, float y, Color color, float width = 1, float height = 1, Rectangle? source = null, SpriteEffects effects = SpriteEffects.None)
    {
        spriteBatch.Draw(textureManager.LoadTexture2D(textureName), new Vector2(x, y), source, color, 0, Vector2.Zero, new Vector2(width, height), effects, 0);
    }
    public void Draw(Texture2D texture, float x, float y, Color color, float width = 1, float height = 1, Rectangle? source = null, SpriteEffects effects = SpriteEffects.None)
    {
        spriteBatch.Draw(texture, new Vector2(x, y), source, color, 0, Vector2.Zero, new Vector2(width, height), effects, 0);
    }
    public void End()
    {
        spriteBatch.End();
    }
    public void DrawString(string s, float x, float y, Color color1, Color color2, float scale = 1)
    {
        spriteBatch.DrawString(spriteFont, s, new Vector2(x + scale, y), color2, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        spriteBatch.DrawString(spriteFont, s, new Vector2(x, y), color1, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
    }
}