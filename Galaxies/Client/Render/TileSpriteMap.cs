using Galaxies.Client.Resource;
using Galaxies.Core.World.Tiles;
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
    private Dictionary<TileState, IStateInfo> infos;
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
        var size = jobject.GetValue("size").ToObject<int[]>();
        int row = size[0];
        int col = size[1];

        //state part
        Rectangle[,] sourceRect = new Rectangle[row, col];
        int width = source.Width / col;
        int height = source.Height / row;
        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < col; x++)
            {
                sourceRect[y, x] = new Rectangle(x * width, y * height, width, height);
            }
        }

        Dictionary<TileState, IStateInfo> infos = [];
        var state = jobject.GetValue("state").ToObject<JObject>();
        foreach (var s in tile.GetAllState())
        {
            var prop = state.GetValue(s.GetState()).ToObject<JObject>();
            bool animation = false;
            bool smoothTile = false;
            if (prop.TryGetValue("animation", out var value))
            {
                animation = value.ToObject<bool>();
            }
            if (prop.TryGetValue("smoothTile", out var v))
            {
                smoothTile = v.ToObject<bool>();
            }

            if (animation)
            {
                var rect = prop.GetValue("renderRect").ToObject<int[]>();
                var rectList = new List<Rectangle>();
                for (int y = rect[0]; y <= rect[2]; y++)
                {
                    for (int x = rect[1]; x <= rect[3]; x++)
                    {
                        rectList.Add(sourceRect[y, x]);
                    }
                    //Console.WriteLine(x);
                }
                var info = new AnimationStateInfo(rectList);
                infos.Add(s, info);

            }
            else if (smoothTile)
            {
                var rectList = new List<Rectangle>();
                var rect = prop.GetValue("full").ToObject<int[]>();
                rectList.Add(sourceRect[rect[0], rect[1]]);

                rect = prop.GetValue("sideIII").ToObject<int[]>();
                rectList.Add(sourceRect[rect[0], rect[1]]);

                rect = prop.GetValue("sideII").ToObject<int[]>();
                rectList.Add(sourceRect[rect[0], rect[1]]);

                rect = prop.GetValue("sideI").ToObject<int[]>();
                rectList.Add(sourceRect[rect[0], rect[1]]);

                rect = prop.GetValue("corner").ToObject<int[]>();
                rectList.Add(sourceRect[rect[0], rect[1]]);

                rect = prop.GetValue("single").ToObject<int[]>();
                rectList.Add(sourceRect[rect[0], rect[1]]);

                var info = new SmoothStateInfo(rectList);
                infos.Add(s, info);
            }
            else//default state info
            {
                var rect = prop.GetValue("renderRect").ToObject<int[]>();
                var info = new StateInfo(sourceRect[rect[0], rect[1]]);
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
