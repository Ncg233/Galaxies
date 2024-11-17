using Galaxies.Client.Gui;
using Galaxies.Client.Gui.Screen;
using Galaxies.Client.Render;
using Galaxies.Core.Audio;
using Microsoft.Xna.Framework;
using System;

namespace Galaxies.Client.Gui.Widget;
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
        renderer.Draw("Textures/Gui/button", new Rectangle(x, y, width, height), Color.White);
        if (IsMouseOver(mouseX, mouseY))
        {
            renderer.DrawString(_text, x + width / 2 - _text.Length * 4, y, Color.Yellow, Color.Black);
        }
        else
        {
            renderer.DrawString(_text, x + width / 2 - _text.Length * 4, y, Color.White, Color.Black);
        }
        //renderer.DrawString(GalaxiasClient.GetInstance().Font, _text, x, y, Color.White, Color.Black);
    }

    public bool MouseClicked(double mouseX, double mouseY, MouseType type)
    {
        if (type == MouseType.Left && IsMouseOver(mouseX, mouseY))
        {
            OnClick();
            AllSounds.ButtonClick.PlayEffect(1f, 1f, 0.5f);
            return true;
        }
        return false;
    }
    public bool IsMouseOver(double mouseX, double mouseY)
    {
        return x <= mouseX && mouseX <= x + width && y <= mouseY && mouseY <= y + height;
    }
    private void OnClick()
    {
        onClickAction.Invoke();
    }

}
