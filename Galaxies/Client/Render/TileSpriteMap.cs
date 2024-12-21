using Galaxies.Client.Render.TileStateInfo;
using Galaxies.Client.Resource;
using Galaxies.Core.World.Tiles;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Galaxies.Client.Render;
public class TileSpriteMap
{
    public Texture2D SourceTexture { get; private set; }
    public int Width;
    public int Height;
    private readonly Dictionary<TileState, IStateInfo> infos;
    public TileSpriteMap(Texture2D source, int width, int height, Dictionary<TileState, IStateInfo> stateInfos)
    {
        SourceTexture = source;
        Width = width;
        Height = height;
        infos = stateInfos;
    }
    public static TileSpriteMap Deserialize(Tile tile, JObject jobject)
    {
        //load texture
        var source = TextureManager.LoadTexture2D(jobject.GetValue("texture").ToString());
        var size = JsonUtils.GetValue<int[]>(jobject, "size");
        int row = size[0];
        int col = size[1];
        int interval = size.Length > 2 ? size[2] : 0;

        //state part
        Rectangle[,] sourceRect = new Rectangle[row, col];
        int width = (source.Width - interval) / col - interval;
        int height = (source.Height - interval) / row - interval;
        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < col; x++)
            {
                sourceRect[y, x] = new Rectangle(x * (width + interval) + interval, y * (height + interval) + interval, width, height);
            }
        }

        Dictionary<TileState, IStateInfo> infos = [];
        JObject state = JsonUtils.GetValue<JObject>(jobject,"state");
        foreach (var s in tile.GetAllState())
        {
            var prop = JsonUtils.GetValue<JObject>(state, s.GetState());
            JsonUtils.TryGetValue(prop, "processor", out string processorName, "default");
            var processor = SpriteManager.ProcessorsMap[processorName].Invoke();
            processor.Deserialize(prop, sourceRect);
            infos.Add(s, processor);

        }
        return new(source, width, height, infos);
    }

    public IStateInfo GetStateInfo(TileState state)
    {
        return infos.GetValueOrDefault(state);
    }
}
