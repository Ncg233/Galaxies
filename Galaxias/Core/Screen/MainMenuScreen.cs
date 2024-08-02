using Galasias.Core.Screen;
using Galaxias.Core.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Galaxias.Core.Screen;
public class MainMenuScreen : ScreenOverlay
{
    private static Song mainMusic;
    public override void Update()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.F))
        {
            galaxias.LoadWorld();
        }
    }
    public override void Render(IntegrationRenderer renderer)
    {
        renderer.Draw("Assets/Textures/Misc/a", new Rectangle(0, 0, width, height), Color.White);
    }
    protected override void OnInit()
    {
        if (mainMusic == null) {
            mainMusic = galaxias.Content.Load<Song>("Assets/Musics/galaxias");
        }
        galaxias.PlayMusic(mainMusic);
        base.OnInit();
    }
    public override void Hid()
    {
        galaxias.StopMusic(mainMusic);
    }

}
