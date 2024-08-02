using Galaxias.Client.Render;
using Galaxias.Core.Main;
using Microsoft.Xna.Framework;

namespace Galaxias.Client.Screen;

public abstract class AbstractScreen
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

    public abstract void Render(IntegrationRenderer renderer);
    public virtual void Update()
    {

    }

    public virtual void Hid()
    {

    }
}
