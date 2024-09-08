using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Galaxias.Core.World.Entities;
using Microsoft.Xna.Framework.Media;
using Galaxias.Client.Resource;
using Galaxias.Client.Render;
using Galaxias.Client.Key;
using Galaxias.Core.World;
using Galaxias.Core.Networking;
using Galaxias.Client.Gui.Screen;
using Galaxias.Server;
using Galaxias.Core.Networking.Packet.C2S;
using System.Threading;
using Galaxias.Core.World.Tiles;
using Galaxias.Client.Gui;

namespace Galaxias.Client.Main;
public class GalaxiasClient : Game
{
    public static readonly string Version = "0.2.9";
    public static float DeltaTime;
    private static GalaxiasClient instance;
    private GalaxiasServer server;
    private GraphicsDeviceManager _graphics;
    public readonly ScreenManager ScreenManager;
    public readonly IntegrationRenderer Renderer;
    public readonly GameRenderer GameRenderer;
    public readonly WorldRenderer WorldRenderer;
    public readonly TileRenderer TileRenderer;
    public readonly ItemRenderer ItemRenderer;
    public readonly TextureManager TextureManager;
    public readonly Camera camera = new();
    public readonly InGameHud inGameHud;
    private NetPlayManager netPlayManager;
    private ClientPlayer player;
    private ClientWorld world;
    private InteractionManagerClient interactionManager;
    
    private int width, height;
    private bool isHost;
    private Song song;
    public GalaxiasClient()
    {
        Thread.CurrentThread.Name = "Client";
        instance = this;
        AllTiles.Init();
        TextureManager = new TextureManager(Content);
        Renderer = new IntegrationRenderer(TextureManager);
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;

        TileRenderer = new TileRenderer();
        ItemRenderer = new ItemRenderer();
        WorldRenderer = new WorldRenderer(this, camera, TileRenderer);
        inGameHud = new InGameHud(this);
        GameRenderer = new GameRenderer(this, Renderer, WorldRenderer, camera, inGameHud);
        ScreenManager = new ScreenManager(this);
        
        //_graphics.IsFullScreen = true;
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;

        camera.OnResize(GetWindowWidth(), GetWindowHeight());
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
        Renderer.LoadContents();
        GameRenderer.LoadContents();
        TileRenderer.LoadContent(TextureManager);
        ItemRenderer.LoadContent(TextureManager);
        song = Content.Load<Song>("Assets/Musics/earth_forest");

        SetCurrentScreen(new MainMenuScreen());
        ScreenManager.FadeIn(1f);
        //entityRenderer.LoadContent(textureManager);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)// TODO: Add your update logic here
    {
        base.Update(gameTime);
        DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        NetPlayManager.UpdateClient();
        if (KeyBind.FullScreen.IsKeyPressed())
        {
            _graphics.ToggleFullScreen();
            OnResize();
        }
        //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        //{
        //    QuitGame();
        //}//please quit game with esc key, or the thread will not stop 
        if (width != GetWindowWidth() || height != GetWindowHeight())
        {
            OnResize();
        }
        ScreenManager.Update(DeltaTime);
        world?.Update(DeltaTime);
        KeyBind.Update(DeltaTime);
        
        interactionManager?.Update(this, GameRenderer.camera, (float)gameTime.ElapsedGameTime.TotalSeconds);
        HandleKey();

    }
    private void HandleKey()
    {
        if (!IsActive) return;

        if (KeyBind.InventoryKey.IsKeyPressed())
        {
            inGameHud.ToggleInventory();
        }
        if (KeyBind.Esc.IsKeyPressed())
        {
            if (ScreenManager.CurrentScreen != null && ScreenManager.CurrentScreen.CanCloseWithEsc)
            {
                SetCurrentScreen(null);
            }
            else if (world != null)
            {
                SetCurrentScreen(new MenuScreen());
            }
        }

    }
    protected override void Draw(GameTime gameTime)
    {

        // TODO: Add your drawing code here
        GameRenderer.Render((float)gameTime.ElapsedGameTime.TotalSeconds);

        base.Draw(gameTime);
    }
    public void QuitGame()
    {
        NetPlayManager.StopClient();
        server?.ServerShutdown();
        Exit();
    }
    public void QuitWorld()
    {
        NetPlayManager.StopClient();
        server?.ServerShutdown();
        LoadWorld(null);
        StopMusic();
        SetCurrentScreen(new MainMenuScreen());

        
    }
    public void SetCurrentScreen(AbstractScreen newScreen)
    {
        ScreenManager.SetCurrentScreen(newScreen, camera.guiWidth, camera.guiHeight);

    }
    public void SetupServer(bool host, out GalaxiasServer gServer)
    {
        isHost = host;
        server = new GalaxiasServer();
        server.StartServerThread();
        gServer = server;
    }
    //this method is used for client join
    public void LoadWorld(ClientWorld world)
    {
        this.world = world;
        if (world != null)
        {
            player = new ClientPlayer(world);
            world.AddEntity(player);
            SetCurrentScreen(null);
            WorldRenderer.SetRenderWorld(world);
            interactionManager = new InteractionManagerClient(world, player);
            PlayMusic(song, 0.5f, true);
        }else
        {
            player = null;
            interactionManager = null;
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
        return _graphics.PreferredBackBufferWidth;
    }
    public int GetWindowHeight()
    {
        return _graphics.PreferredBackBufferHeight;
    }
    public Player GetPlayer()
    {
        return player;
    }
    public AbstractWorld GetWorld()
    {
        return world;
    }
    public TextureManager GetTextureManager()
    {
        return TextureManager;
    }
    public static GalaxiasClient GetInstance()
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

    internal ItemRenderer GetItemRenderer()
    {
        return ItemRenderer;
    }
}
