using Galaxias.Core.Main;
using Galaxias.Core.Render;
using Microsoft.Xna.Framework;

namespace Galasias.Core.Screen;

public class ScreenOverlay
{
    protected int width;
    protected int height;
    protected GalaxiasClient galaxias;
    public void Init(GalaxiasClient galaxias, int width, int height)
    {
        this.galaxias = galaxias;
        this.width = width;
        this.height = height;
        OnInit();
    }

    protected virtual void OnInit()
    {
        
    }

    public virtual void Render(IntegrationRenderer renderer)
    {
        renderer.Draw("Assets/Textures/Misc/a", 0, 0, Color.White);
    }
    public virtual void Update()
    {

    }

    public virtual void Hid()
    {
        
    }
}
