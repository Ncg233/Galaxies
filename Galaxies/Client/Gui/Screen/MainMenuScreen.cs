using Galaxies.Client.Render;
using Galaxies.Core.Audio;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.IO;

namespace Galaxies.Client.Gui.Screen;
public class MainMenuScreen : AbstractScreen
{
    private static Song mainMusic;
    private static DirectoryInfo rootPath = new DirectoryInfo("C:\\Users\\MC\\Documents\\.galaxias\\");
    public MainMenuScreen()
    {
        CanCloseWithEsc = false;
        var a = Utils.Random.Next(0, 2);
        if(a == 0)
        {
            AllSounds.Galaxias.PlayMusic(1f);
        }else
        {
            AllSounds.Galaxias2.PlayMusic(1f);
        }
        
    }
    public override void Update()
    {
    }
    public override void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        renderer.Draw("Textures/Misc/a", new Rectangle(0, 0, Width, Height), Color.White);
        base.Render(renderer, mouseX, mouseY);
    }
    protected override void OnInit()
    {
        AddWidget(new Widget.Button("Single Player", Width / 2 - 100, Height / 4 + 48, 200, 20, () =>
        {
            galaxias.SetCurrentScreen(new WorldSelectionScreen(true));
        }));
        AddWidget(new Widget.Button("Multiplayer", Width / 2 - 100, Height / 4 + 78, 200, 20, () =>
        {
            //galaxias.SetupServer();
            //galaxias.StartWorld();
            //Console.WriteLine("Join world");
            //Log.Info("Multiplayer");
            //NetPlayManager.InitClient("127.0.0.1", 9050);
            //galaxias.SetCurrentScreen(null);
            galaxias.SetCurrentScreen(new MultiplayerScreen());

        }));
        AddWidget(new Widget.Button("Quit", Width / 2 - 100, Height / 4 + 108, 200, 20, galaxias.QuitGame));
    }
    public override void Hid()
    {
        galaxias.StopMusic();
    }

}
