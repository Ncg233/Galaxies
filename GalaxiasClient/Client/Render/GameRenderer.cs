using ClientGalaxias.Client.Gui;
using ClientGalaxias.Client.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientGalaxias.Client.Render;
public class GameRenderer
{
    private IntegrationRenderer renderer;
    private WorldRenderer _worldRenderer;
    private InGameHud hud;
    //private SpriteBatch _spriteBatch;
    private GalaxiasClient _galaxias;

    public Camera camera;

    public GameRenderer(GalaxiasClient galaxias, IntegrationRenderer renderer, WorldRenderer worldRenderer, Camera camera)
    {
        this.renderer = renderer;
        this.camera = camera;
        _galaxias = galaxias;
        hud = new InGameHud(_galaxias);
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
        if (_galaxias.GetWorld() != null)
        {
            //render world
            camera.Update(_galaxias.GetPlayer(), dTime);
            renderer.Begin(sortMode: SpriteSortMode.Immediate,
                samplerState: SamplerState.PointClamp,
                depthStencilState: DepthStencilState.Default,
                transformMatrix: camera.TransfromMatrix);

            _worldRenderer.Render(null, renderer);
            renderer.End();
            //render hud
            renderer.Begin(sortMode: SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                samplerState: SamplerState.PointClamp,
                depthStencilState: DepthStencilState.Default,
                transformMatrix: camera.GuiMatrix);
            hud.Render(renderer, camera.guiWidth, camera.guiHeight, dTime);
            renderer.End();

        }
        if (_galaxias.GetCurrentScreen() != null)
        {
            renderer.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GuiMatrix);
            _galaxias.GetCurrentScreen().Render(renderer);
            renderer.End();
        }
    }

    internal void onResize(int width, int height)
    {
        camera.OnResize(width, height);
        _worldRenderer.OnResize(width, height);
    }
}
