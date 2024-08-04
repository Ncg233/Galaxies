using Client.Code.Gui;
using Client.Code.Render;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Code.Gui.Widget;
public class Button : IRenderable
{
    private string _text;
    private int x;
    private int y;
    private int width;
    private int height;
    private Action onClickAction;
    public Button(string text, int x, int y, int width, int height, Action onClickAction)
    {
        _text = text;
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.onClickAction = onClickAction;
    }
    public void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        renderer.Draw("Assets/Textures/Gui/button", new Rectangle(x, y, width, height), Color.White);
        renderer.DrawString(_text, x + width / 2 - _text.Length * 4, y, Color.White, Color.Black);
        //renderer.DrawString(GalaxiasClient.GetInstance().Font, _text, x, y, Color.White, Color.Black);
    }

    public void MouseClicked(double mouseX, double mouseY)
    {
        if (x <= mouseX && mouseX <= x + width && y <= mouseY && mouseY <= y + height)
        {
            OnClick();
        }
    }
    private void OnClick()
    {
        onClickAction.Invoke();
    }

}
