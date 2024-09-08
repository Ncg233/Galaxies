using Galaxias.Client.Main;
using Galaxias.Client.Resource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace Galaxias.Client.Render;
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
        spriteBatch = new SpriteBatch(Main.GalaxiasClient.GetInstance().GraphicsDevice);
        spriteFont = Main.GalaxiasClient.GetInstance().Content.Load<SpriteFont>("Assets/Fonts/defaultFont");

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
        var texture = textureManager.LoadTexture2D(textureName);
        spriteBatch.Draw(texture, new Vector2(x, y), source, color, 0, Vector2.Zero, new Vector2(width, height), effects, 0);
    }
    public void Draw(Texture2D texture, float x, float y, float originX, float originY, float width, float height, Color color, Rectangle? source = null, SpriteEffects effects = SpriteEffects.None)
    {
        spriteBatch.Draw(texture, new Vector2(x, y), source, color, 0, new Vector2(originX, originY), new Vector2(width, height), effects, 0);
    }
    public void Draw(Texture2D texture, float x, float y, Color color, float width = 1, float height = 1, Rectangle? source = null, SpriteEffects effects = SpriteEffects.None)
    {
        spriteBatch.Draw(texture, new Vector2(x, y), source, color, 0, Vector2.Zero, new Vector2(width, height), effects, 0);
    }
    public void Draw(Texture2D texture, Rectangle rect, Color color, Rectangle? source = null, SpriteEffects effects = SpriteEffects.None)
    {
        spriteBatch.Draw(texture, rect, source, color, 0, Vector2.Zero, effects, 0);
    }
    public void DrawSpriteMap(SpriteMap map, float x, float y, float originX, float originY, int pos, Color color)
    {

        spriteBatch.Draw(map.SourceTexture, new Vector2(x, y), map.GetSourceRect(pos),color, 0, new Vector2(originX, originY), 1, SpriteEffects.None, 0);
    }
    public void End()
    {
        spriteBatch.End();
    }
    public void DrawString(string s, float x, float y, float scale = 1)
    {
        DrawString(s, x, y, Color.White, Color.Black, scale);
    }
    public void DrawString(string s, float x, float y, Color color1, Color color2, float scale = 1)
    {
        spriteBatch.DrawString(spriteFont, s, new Vector2(x + scale, y), color2, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        spriteBatch.DrawString(spriteFont, s, new Vector2(x, y), color1, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
    }
}
