using Galaxies.Core.World.Items;
using Galaxies.Core.World.Tiles;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Galaxies.Client.Render;
internal class SpriteManager
{
    public static readonly Dictionary<Tile, TileSpriteMap> stateToSprite = [];
    public static readonly Dictionary<Item, ItemSpriteMap> itemToSprite = [];
    public static void LoadContent()
    {
        var assembly = typeof(Main).Assembly;

        foreach (var tile in AllTiles.tileRegister)
        {
            if (tile.Value.GetRenderType() != TileRenderType.Invisible)
            {
                string sourceName = "Galaxies.Content.Infos.Models.Tiles." + tile.Key + ".json";
                Stream stream = assembly.GetManifestResourceStream(sourceName);
                if (stream == null)
                {
                    Log.Info("Can't find tile json file at " + sourceName);
                    continue;
                }
                using StreamReader reader = new(stream);
                string json = reader.ReadToEnd();
                var jobject = JObject.Parse(json);
                stateToSprite.Add(tile.Value, TileSpriteMap.Deserialize(tile.Value, jobject));
            }
        }
        foreach(var pair in AllItems.itemRegister)
        {
            if (pair.Key == "air") continue;
            string sourceName = "Galaxies.Content.Infos.Models.Items." + pair.Key + ".json";
            Stream stream = assembly.GetManifestResourceStream(sourceName);
            if(stream == null)
            {
                Log.Info("Can't find item json file at " + sourceName);
                continue;
            }
            using StreamReader reader = new(stream);
            string json = reader.ReadToEnd();
            var jobject = JObject.Parse(json);
            itemToSprite.Add(pair.Value, ItemSpriteMap.Deserialize(jobject));

        }
    }
    public static TileSpriteMap GetSpriteMap(TileState tile)
    {
        return stateToSprite.GetValueOrDefault(tile.GetTile());
    }

    public static IStateInfo GetStateInfo(TileState tile)
    {

        return stateToSprite.GetValueOrDefault(tile.GetTile()).GetStateInfo(tile);
    }
    public static ItemSpriteMap GetSpriteMap(Item item)
    {
        return itemToSprite.GetValueOrDefault(item);
    }
}
