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
    public override void Update()
    {
    }
    public override void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        renderer.Draw("Textures/Misc/a", new Rectangle(0, 0, width, height), Color.White);
        base.Render(renderer, mouseX, mouseY);
    }
    protected override void OnInit()
    {
        if (mainMusic == null)
        {
            mainMusic = galaxias.Content.Load<Song>("Assets/Musics/galaxias");
            galaxias.PlayMusic(mainMusic);
        }
        AddButton(new Widget.Button("Single Player", width / 2 - 100, height / 4 + 48, 200, 20, () =>
        {
            Log.Info("Single player");
            GalaxiasServer server;
            galaxias.SetupServer(false, out server);
            NetPlayManager.InitServer("127.0.0.1", 9050, server);
            galaxias.SetCurrentScreen(null);


        }));
        AddButton(new Widget.Button("Multiplayer", width / 2 - 100, height / 4 + 78, 200, 20, () =>
        {
            //galaxias.SetupServer();
            //galaxias.StartWorld();
            //Console.WriteLine("Join world");
            Log.Info("Multiplayer");
            NetPlayManager.InitClient("127.0.0.1", 9050);
            galaxias.SetCurrentScreen(null);

        }));
        AddButton(new Widget.Button("Quit", width / 2 - 100, height / 4 + 108, 200, 20, galaxias.QuitGame));
        base.OnInit();
    }
    public override void Hid()
    {
        galaxias.StopMusic(mainMusic);
    }

}
