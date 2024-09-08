using Galaxias.Client.Main;
using Galaxias.Client.Render;
using Galaxias.Core.Networking;
using Galaxias.Server;
using Galaxias.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Galaxias.Client.Gui.Screen;
public class MainMenuScreen : AbstractScreen
{
    private static Song mainMusic;
    public MainMenuScreen ()
    {
        CanCloseWithEsc = false;
        if (mainMusic == null)
        {
            mainMusic = GalaxiasClient.GetInstance().Content.Load<Song>("Assets/Musics/galaxias");
        }
        GalaxiasClient.GetInstance().PlayMusic(mainMusic, 0.7f, true);
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
        
        AddButton(new Widget.Button("Single Player", Width / 2 - 100, Height / 4 + 48, 200, 20, () =>
        {
            Log.Info("Single player");
            
            galaxias.ScreenManager.FadeOut(1f, () => {
                galaxias.SetupServer(false, out GalaxiasServer server);
                NetPlayManager.InitLocalServer(server);
                galaxias.SetCurrentScreen(null);
            });
        }));
        AddButton(new Widget.Button("Multiplayer", Width / 2 - 100, Height / 4 + 78, 200, 20, () =>
        {
            //galaxias.SetupServer();
            //galaxias.StartWorld();
            //Console.WriteLine("Join world");
            Log.Info("Multiplayer");
            NetPlayManager.InitClient("127.0.0.1", 9050);
            galaxias.SetCurrentScreen(null);

        }));
        AddButton(new Widget.Button("Quit", Width / 2 - 100, Height / 4 + 108, 200, 20, galaxias.QuitGame));
        base.OnInit();
    }
    public override void Hid()
    {
        galaxias.StopMusic();
    }

}
