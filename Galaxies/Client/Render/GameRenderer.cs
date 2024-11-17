using Galaxies.Client.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;

namespace Galaxies.Client.Render;
public class GameRenderer
{
    private IntegrationRenderer renderer;
    private WorldRenderer _worldRenderer;
    private InGameHud hud;
    //private SpriteBatch _spriteBatch;
    private Main _galaxias;

    public Camera camera;
    private bool renderHud = true;

    public GameRenderer(Main galaxias, IntegrationRenderer renderer, WorldRenderer worldRenderer, Camera camera, InGameHud inGameHud)
    {
        this.renderer = renderer;
        this.camera = camera;
        _galaxias = galaxias;
        hud = inGameHud;

        _worldRenderer = worldRenderer;
    }
    public void LoadContents()
    {
        //_spriteBatch = new SpriteBatch(_galaxias.GraphicsDevice);
        _worldRenderer.LoadContents();
    }
    public void Render(float dTime)
    {
        _galaxias.GraphicsDevice.Clear(Color.Black);

        Point p = Mouse.GetState().Position;
        double mouseX = p.X * camera.guiWidth / _galaxias.GetWindowWidth();
        double mouseY = p.Y * camera.guiHeight / _galaxias.GetWindowHeight();

        if (_galaxias.GetWorld() != null)
        {
            //render world
            camera.Update(_galaxias.GetPlayer(), dTime);
            renderer.Begin(sortMode: SpriteSortMode.Deferred,
                blendState: BlendState.AlphaBlend,
                samplerState: SamplerState.PointWrap,
                depthStencilState: DepthStencilState.Default,
                effect: null,
                transformMatrix: camera.TransfromMatrix);

            _worldRenderer.Render(renderer);

            renderer.End();
            //render hud
            if (renderHud)
            {
                renderer.Begin(sortMode: SpriteSortMode.Immediate,
                blendState:BlendState.AlphaBlend,
                samplerState: SamplerState.PointClamp,
                depthStencilState: DepthStencilState.Default,
                effect: null,
                transformMatrix: camera.GuiMatrix);

                hud.Render(renderer, mouseX, mouseY, dTime);

                renderer.End();
            }
        }
        //render gui
        renderer.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GuiMatrix, blendState: BlendState.AlphaBlend);

        _galaxias.ScreenManager.Render(renderer, mouseX, mouseY);
        renderer.End();

    }

    internal void onResize(int width, int height)
    {
        camera.OnResize(width, height);
        _worldRenderer.OnResize(width, height);
    }
}
