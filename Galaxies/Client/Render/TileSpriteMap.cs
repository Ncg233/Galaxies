using Galaxies.Client.Resource;
using Galaxies.Core.World.Tiles;
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
        int width = source.Width / col - interval;
        int height = source.Height / row - interval;
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
            JsonUtils.TryGetValue(prop, "animation", out bool animation);
            JsonUtils.TryGetValue(prop, "smoothTile", out bool smoothTile);

            if (animation)
            {
                var rect = JsonUtils.GetValue<int[]>(prop, "renderRect");
                var rectList = new List<Rectangle>();
                for (int y = rect[0] - 1; y <= rect[2] - 1; y++)
                {
                    for (int x = rect[1] - 1; x <= rect[3] - 1; x++)
                    {
                        rectList.Add(sourceRect[y, x]);
                    }
                }
                var info = new AnimationStateInfo(rectList);
                infos.Add(s, info);

            }
            else if (smoothTile)
            {
                var rectList = new List<Rectangle>();
                var rect = JsonUtils.GetValue<int[]>(prop, "full");
                rectList.Add(sourceRect[rect[0] - 1, rect[1] - 1]);

                rect = JsonUtils.GetValue<int[]>(prop, "sideIII");
                rectList.Add(sourceRect[rect[0] - 1, rect[1] - 1]);

                rect = JsonUtils.GetValue<int[]>(prop, "sideII");
                rectList.Add(sourceRect[rect[0] - 1, rect[1] - 1]);

                rect = JsonUtils.GetValue<int[]>(prop, "sideI");
                rectList.Add(sourceRect[rect[0] - 1, rect[1] - 1]);

                rect = JsonUtils.GetValue<int[]>(prop, "corner");
                rectList.Add(sourceRect[rect[0] - 1, rect[1] - 1]);

                rect = JsonUtils.GetValue<int[]>(prop, "single");
                rectList.Add(sourceRect[rect[0] - 1, rect[1] - 1]);

                var info = new SmoothStateInfo(rectList);
                infos.Add(s, info);
            }
            else//default state info
            {
                var rect = JsonUtils.GetValue<int[]>(prop, "renderRect");
                var info = new StateInfo(sourceRect[rect[0] - 1, rect[1] - 1]);
                infos.Add(s, info);
            }
            //var info = new StateInfo(animation, rectList);
            //infos.Add(s, info);

        }
        TileSpriteMap map = new(source, width, height, infos);

        return map;
    }

    public IStateInfo GetStateInfo(TileState state)
    {
        return infos.GetValueOrDefault(state);
    }
}
