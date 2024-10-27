using Galaxies.Client;
using Galaxies.Core.World.Tiles;
using Galaxies.Core.World.Tiles.State;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Render;
internal class TileSpriteManager
{
    public static readonly Dictionary<Tile, TileSpriteMap> stateToSprite = [];
    public static void LoadContent()
    {
        var assembly = typeof(Main).Assembly;

        foreach (var tile in AllTiles.tileRegister)
        {
            if (tile.Key != "air")
            {
                string sourceName = "Galaxies.Content.Infos.Models.Tiles." + tile.Key + ".json";
                Stream stream = assembly.GetManifestResourceStream(sourceName);
                if (stream == null)
                {
                    Console.WriteLine("Can't find json file at " + sourceName);
                    continue;
                }
                using StreamReader reader = new(stream);
                string json = reader.ReadToEnd();
                var jobject = JObject.Parse(json);
                stateToSprite.Add(tile.Value, TileSpriteMap.Deserialize(tile.Value, jobject));
            }
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
}
