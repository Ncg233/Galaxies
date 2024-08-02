using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Galaxias.Client.Screen;
using Galaxias.Core.World.Entities;
using Microsoft.Xna.Framework.Media;
using Galaxias.Client.Render;
using Galaxias.Client.Resource;
using Galaxias.Client.Key;
using Galaxias.Client;

namespace Galaxias.Core.Main;
public class GalaxiasClient : Game
{
    public static readonly string Version = "0.0.1";
    private static GalaxiasClient instance;
    private GraphicsDeviceManager _graphics;
    private AbstractScreen _currentScreen;
    private readonly IntegrationRenderer renderer;
    private readonly GameRenderer _gameRenderer;
    private readonly WorldRenderer worldRenderer;
    private readonly TileRenderer tileRenderer;
    private readonly TextureManager textureManager;
    private Player player;
    private ClientWorld world;
    private InteractionManager interactionManager;
    private Camera camera = new();
    private int width, height;
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
        worldRenderer = new WorldRenderer(this, camera, tileRenderer);
        _gameRenderer = new GameRenderer(this, renderer, worldRenderer, camera);
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
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)// TODO: Add your update logic here
    {
        base.Update(gameTime);
        if (KeyBind.FullScreen.IsKeyDown())
        {
            _graphics.ToggleFullScreen();
            OnResize();
        }
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        if (width != GetWindowWidth() || height != GetWindowHeight())
        {
            OnResize();
        }
        world?.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        KeyBind.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        _currentScreen?.Update();
        interactionManager?.Update(this, _gameRenderer.camera);

    }

    protected override void Draw(GameTime gameTime)
    {

        // TODO: Add your drawing code here
        _gameRenderer.Render((float)gameTime.ElapsedGameTime.TotalSeconds);

        base.Draw(gameTime);
    }
    public void SetCurrentScreen(AbstractScreen newScreen)
    {
        _currentScreen?.Hid();

        _currentScreen = newScreen;

        _currentScreen?.Init(this, camera.guiWidth, camera.guiHeight);

    }
    public void LoadWorld()
    {
        world = new ClientWorld();
        player = new Player(world);
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
        _currentScreen?.OnResize(camera.guiWidth, camera.guiHeight);
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
    public ClientWorld GetWorld()
    {
        return world;
    }

    public AbstractScreen GetCurrentScreen()
    {
        return _currentScreen;
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
}
