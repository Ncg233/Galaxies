using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using System;
using System.IO;
using Galaxies.Client.Gui;
using Galaxies.Client.Gui.Screen;
using Galaxies.Core.World;
using Galaxies.Client.Render;
using Galaxies.Core.Networking;
using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Particles;
using Galaxies.Core.World.Tiles;
using Galaxies.Client.Resource;
using Galaxies.Client.Key;
using Galaxies.Util;
using Galaxies.Core.Audio;
using Galaxies.Core.Networking.Server;
using Galaxies.Core.Networking.Client;

namespace Galaxies.Client;
public class Main : Game
{
    private static readonly Guid DefaultId = Guid.Empty;
    public static readonly string Version = "0.3.0";
    public static float DeltaTime;
    private static Main instance;
    //private GalaxiasServer server;
    private GraphicsDeviceManager _graphics;
    public readonly ScreenManager ScreenManager;
    public readonly IntegrationRenderer Renderer;
    public readonly GameRenderer GameRenderer;
    public readonly WorldRenderer WorldRenderer;
    public static readonly Camera camera = new();
    public readonly InGameHud inGameHud;
    private NetPlayManager netPlayManager;
    private AbstractPlayerEntity player;
    private AbstractWorld world;
    private ParticleManager particleManager;
    private int width, height;
    public Main()
    {
        Thread.CurrentThread.Name = "Client";
        instance = this;
        AllTiles.Init();
        Renderer = new IntegrationRenderer();
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;

        particleManager = new ParticleManager();
        WorldRenderer = new WorldRenderer(this, camera, particleManager);
        inGameHud = new InGameHud(this);
        GameRenderer = new GameRenderer(this, Renderer, WorldRenderer, camera, inGameHud);
        ScreenManager = new ScreenManager(this);

        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        OnResize();
        Window.ClientSizeChanged += Window_ClientSizeChanged;

        camera.OnResize(GetWindowWidth(), GetWindowHeight());
    }
    void Window_ClientSizeChanged(object sender, EventArgs e)
    {
        _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
        _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
        OnResize();
    }
    public static float GetDeltaTime()
    {
        return DeltaTime;
    }
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();

    }

    protected override void LoadContent()
    {
        IntegrationRenderer.LoadContents();
        GameRenderer.LoadContents();
        TileRenderer.LoadContent();
        ItemRenderer.LoadContent();
        AllSounds.LoadContent();
        TextureManager.LoadContent();

        SetCurrentScreen(new MainMenuScreen());
        ScreenManager.FadeIn(1f);
        //entityRenderer.LoadContent(textureManager);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)// TODO: Add your update logic here
    {
        base.Update(gameTime);
        DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        NetPlayManager.Update();
        if (KeyBind.FullScreen.IsKeyPressed())
        {
            _graphics.ToggleFullScreen();
            OnResize();
        }
        //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        //{
        //    QuitGame();
        //}//please quit game with esc key, or the thread will not stop 
        if (IsActive || NetPlayManager.IsInit())
        {
            if (MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Resume();
            }
            ScreenManager.Update(DeltaTime);
            world?.Update(DeltaTime);
            particleManager.update(DeltaTime);

            //interactionManager?.Update(this, GameRenderer.camera, DeltaTime);
            HandleKey();
        }
        else
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
            }
        }


    }
    private void HandleKey()
    {

        if (KeyBind.InventoryKey.IsKeyPressed())
        {
            inGameHud.ToggleInventory();
        }
        if (KeyBind.Esc.IsKeyPressed())
        {
            var screen = ScreenManager.CurrentScreen;
            if (screen == null)
            {
                SetCurrentScreen(new MenuScreen());
            }
            else if (screen.CanCloseWithEsc)
            {
                ScreenManager.SetPreviousScreen();
            }
        }

    }
    protected override void Draw(GameTime gameTime)
    {

        GameRenderer.Render((float)gameTime.ElapsedGameTime.TotalSeconds);

        base.Draw(gameTime);
    }
    public void QuitGame()
    {
        NetPlayManager.Stop();
        Exit();
    }
    public void QuitWorld()
    {
        NetPlayManager.Stop();
        if (world != null)
        {
            particleManager.Clear();
            world.SaveData();
            world = null;
            player = null;
        }
        SetCurrentScreen(new MainMenuScreen());
    }
    public void SetCurrentScreen(AbstractScreen newScreen)
    {
        ScreenManager.SetCurrentScreen(newScreen, camera.guiWidth, camera.guiHeight);

    }
    //this method is used for server start
    internal void StartWorld(DirectoryInfo info)
    {
        world = new ServerWorld(info, WorldRenderer);
        player = world.CreatePlayer(null, DefaultId);// PlayerEntity
        world.AddEntity(player);
        WorldRenderer.SetRenderWorld(world);
        SetCurrentScreen(null);
        AllSounds.EarthForest.PlayMusic(0.5f);
    }
    //this method is used for client join
    internal void JoinWorld(ClientWorld world)
    {
        this.world = world;
        if (world != null)
        {
            player = world.CreatePlayer(null, DefaultId);//ClientPlayer
            world.AddEntity(player);
            SetCurrentScreen(null);
            WorldRenderer.SetRenderWorld(world);
            //interactionManager = new InteractionManager(world, player);
            //AllSounds.EarthForest.PlayMusic(0.5f);
        }
        else
        {
            player = null;
        }
    }
    private void OnResize()
    {
        width = GetWindowWidth();
        height = GetWindowHeight();
        GameRenderer.onResize(width, height);
        ScreenManager.OnResize(camera.guiWidth, camera.guiHeight);
        inGameHud.OnResize(camera.guiWidth, camera.guiWidth);

    }
    public int GetWindowWidth()
    {
        return _graphics.PreferredBackBufferWidth == 0 ? 1 : _graphics.PreferredBackBufferWidth;
    }
    public int GetWindowHeight()
    {
        return _graphics.PreferredBackBufferHeight == 0 ? 1 : _graphics.PreferredBackBufferHeight;
    }
    public AbstractPlayerEntity GetPlayer()
    {
        return player;
    }
    public AbstractWorld GetWorld()
    {
        return world;
    }
    public static Main GetInstance()
    {
        return instance;
    }

    internal GameRenderer GetGameRenderer()
    {
        return GameRenderer;
    }
    internal void PlayMusic(Song song, float volume, bool isRepeating)
    {
        MediaPlayer.IsRepeating = isRepeating;
        MediaPlayer.Volume = volume;
        MediaPlayer.Play(song);
    }

    internal void StopMusic()
    {
        MediaPlayer.Stop();
    }
    public ParticleManager GetParticleManager()
    {
        return particleManager;
    }
    public static void GetMosueTilePos(out int x, out int y)
    {
        Vector2 p = camera.ScreenToWorldSpace(Mouse.GetState().Position);
        x = Utils.Floor(p.X / 8);
        y = Utils.Floor(-p.Y / 8);
    }
}
