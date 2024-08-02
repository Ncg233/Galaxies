using Galaxias.Client.Render;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Client.Gui.Widget;
public class Button : IRenderable
{
    private int x;
    private int y;
    private int width;
    private int height;
    public Button(int x, int y, int width, int height) {
        this.x = x;
        this.y = y; 
        this.width = width;
        this.height = height;
    }
    public void Render(IntegrationRenderer renderer, int mouseX, int mouseY)
    {
        renderer.Draw("Assets/Textures/Misc/blank", new Rectangle(x, y, width, height), Color.White);
    }
}
