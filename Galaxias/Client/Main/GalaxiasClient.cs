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

namespace Galaxias.Client.Main;
public class GalaxiasClient : Game
{
    public static readonly string Version = "0.2.0";
    private static GalaxiasClient instance;
    private GalaxiasServer server;
    private GraphicsDeviceManager _graphics;
    public AbstractScreen CurrentScreen { get; private set; }
    private readonly IntegrationRenderer renderer;
    private readonly GameRenderer _gameRenderer;
    private readonly WorldRenderer worldRenderer;
    private readonly TileRenderer tileRenderer;
    private readonly ItemRenderer itemRenderer;
    private readonly TextureManager textureManager;
    private NetPlayManager netPlayManager;
    private ClientPlayer player;
    private World world;
    private InteractionManager interactionManager;
    private Camera camera = new();
    private int width, height;
    private bool isHost;
    public GalaxiasClient()
    {
        instance = this;
        textureManager = new TextureManager(Content);
        renderer = new IntegrationRenderer(textureManager);
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;

        tileRenderer = new TileRenderer();
        itemRenderer = new ItemRenderer();
        worldRenderer = new WorldRenderer(this, camera, tileRenderer);
        _gameRenderer = new GameRenderer(this, renderer, worldRenderer, camera);
        //_graphics.IsFullScreen = true;
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;

        camera.OnResize(GetWindowWidth(), GetWindowHeight());
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();
        SetCurrentScreen(new MainMenuScreen());
    }

    protected override void LoadContent()
    {
        renderer.LoadContents();
        _gameRenderer.LoadContents();
        tileRenderer.LoadContent(textureManager);
        itemRenderer.LoadContent(textureManager);
        //entityRenderer.LoadContent(textureManager);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)// TODO: Add your update logic here
    {
        base.Update(gameTime);
        NetPlayManager.Instance.UpdateClient();
        if (KeyBind.FullScreen.IsKeyPressed())
        {
            _graphics.ToggleFullScreen();
            OnResize();
        }
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
        {
            QuitGame();
        }//please quit game with esc key, or the thread will not stop 
        if (width != GetWindowWidth() || height != GetWindowHeight())
        {
            OnResize();
        }
        if (IsActive && Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && CurrentScreen != null)
        {
            Point p = Mouse.GetState().Position;
            double mouseX = p.X * camera.guiWidth / GetWindowWidth();
            double mouseY = p.Y * camera.guiHeight / GetWindowHeight();
            CurrentScreen.MouseClicked(mouseX, mouseY);

        }
        world?.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        KeyBind.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        CurrentScreen?.Update();
        interactionManager?.Update(this, _gameRenderer.camera, (float)gameTime.ElapsedGameTime.TotalSeconds);

    }
    protected override void Draw(GameTime gameTime)
    {

        // TODO: Add your drawing code here
        _gameRenderer.Render((float)gameTime.ElapsedGameTime.TotalSeconds);

        base.Draw(gameTime);
    }
    public void QuitGame()
    {
        NetPlayManager.Instance.StopClient();
        server?.StopServer();
        Exit();
    }
    public void SetCurrentScreen(AbstractScreen newScreen)
    {
        CurrentScreen?.Hid();

        CurrentScreen = newScreen;

        CurrentScreen?.Init(this, camera.guiWidth, camera.guiHeight);

    }
    public void SetupServer(bool host)
    {
        isHost = host;
        server = new GalaxiasServer();
        server.StartServerThread();
    }
    //this method is only used for server start
    public void StartWorld()
    {
        
    }
    //this method is used for client join
    public void JoinWorld()
    {
        world = new ClientWorld();
        player = new ClientPlayer(world);
        world.AddEntity(player);
        SetCurrentScreen(null);
        worldRenderer.SetRenderWorld(world);
        interactionManager = new InteractionManager(world);
    }
    private void OnResize()
    {
        width = GetWindowWidth();
        height = GetWindowHeight();
        _gameRenderer.onResize(width, height);
        CurrentScreen?.OnResize(camera.guiWidth, camera.guiHeight);

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
    public World GetWorld()
    {
        return world;
    }

    public AbstractScreen GetCurrentScreen()
    {
        return CurrentScreen;
    }
    public TextureManager GetTextureManager()
    {
        return textureManager;
    }
    public static GalaxiasClient GetInstance()
    {
        return instance;
    }

    internal GameRenderer GetGameRenderer()
    {
        return _gameRenderer;
    }
    internal void PlayMusic(Song song)
    {
        MediaPlayer.Play(song);
    }

    internal void StopMusic(Song mainMusic)
    {
        MediaPlayer.Stop();
    }

    internal ItemRenderer GetItemRenderer()
    {
        return itemRenderer;
    }
}
