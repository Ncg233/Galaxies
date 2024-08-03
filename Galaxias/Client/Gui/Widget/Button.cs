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
    private Action onClickAction;
    public Button(int x, int y, int width, int height, Action onClickAction) {
        this.x = x;
        this.y = y; 
        this.width = width;
        this.height = height;
        this.onClickAction = onClickAction;
    }
    public void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        renderer.Draw("Assets/Textures/Misc/blank", new Rectangle(x, y, width, height), Color.White);
    }
    public void MouseClicked(double mouseX, double mouseY) { 
        if(x <= mouseX && mouseX <= x + width && y <= mouseY && mouseY <= y + height)
        {
            OnClick();
        }
    }
    private void OnClick()
    {
        onClickAction.Invoke();
    }

}
