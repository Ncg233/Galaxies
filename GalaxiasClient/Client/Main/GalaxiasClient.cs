using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Galaxias.Core.World.Entities;
using Microsoft.Xna.Framework.Media;
using ClientGalaxias.Client;
using ClientGalaxias.Client.Gui;
using ClientGalaxias.Client.Key;
using ClientGalaxias.Client.Render;
using ClientGalaxias.Client.Resource;
using System;
using Galaxias.Server;

namespace ClientGalaxias.Client.Main;
public class GalaxiasClient : Game
{
    public static readonly string Version = "0.1.0";
    private static GalaxiasClient instance;
    private GraphicsDeviceManager _graphics;
    public AbstractScreen CurrentScreen { get; private set; }
    private readonly IntegrationRenderer renderer;
    private readonly GameRenderer _gameRenderer;
    private readonly WorldRenderer worldRenderer;
    private readonly TileRenderer tileRenderer;
    private readonly ItemRenderer itemRenderer;
    private readonly EntityRenderer entityRenderer;
    private readonly TextureManager textureManager;
    private readonly Networking.Client client = new();
    private GalaxiasServer gServer;
    private ClientPlayer player;
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
        itemRenderer = new ItemRenderer();
        entityRenderer = new EntityRenderer();
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
        itemRenderer.LoadContent(textureManager);
        //entityRenderer.LoadContent(textureManager);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)// TODO: Add your update logic here
    {
        base.Update(gameTime);
        client.Update();
        if (KeyBind.FullScreen.IsKeyPressed())
        {
            _graphics.ToggleFullScreen();
            OnResize();
        }
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            client.Stop();
            gServer?.StopServer();
            Exit();
        }//please quit game with esc key, or the thread will not stop 
            
        if (width != GetWindowWidth() || height != GetWindowHeight())
        {
            OnResize();
        }
        if (Mouse.GetState().LeftButton == ButtonState.Pressed && CurrentScreen != null)
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
    public void SetCurrentScreen(AbstractScreen newScreen)
    {
        CurrentScreen?.Hid();

        CurrentScreen = newScreen;

        CurrentScreen?.Init(this, camera.guiWidth, camera.guiHeight);

    }
    public void SetupServer()
    {
        if(gServer == null)
        {
            gServer = new GalaxiasServer();
            gServer.StartServerThread();
        }
        client.Connect("localhost", 9050);
    }
    public void LoadWorld()
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
    public ClientWorld GetWorld()
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
