using Galasias.Core.Screen;
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
