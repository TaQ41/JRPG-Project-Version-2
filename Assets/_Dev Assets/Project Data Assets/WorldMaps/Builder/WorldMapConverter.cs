using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace WorldMapData.Builder
{

/// <summary>
/// Convert a map to mock tiles in the scene view or in reverse. This is a convenience object used to make building tile maps for the JRPG much nicer.
/// </summary>
public class WorldMapConverter : SerializedMonoBehaviour
{
    [SerializeField]
    private List<ListWrapper<Tile>> Map;

    public GameObject SceneViewMapParent;
    public GameObject SceneViewMapGeneratedParent;
    public GameObject TileMockObject;

    [SerializeField]
    private List<ListWrapper<Tile>> Map1;

    // Tile property setters
    private const string mapCoordsText = "MapCoords";
    private const string worldCoordsText = "WorldCoords";
    private const string isNavigableText = "IsNavigable";

    [Button]
    public void GenerateCSharpMap()
    {
        Map.Clear();
        if (SceneViewMapParent == null)
        {
            Debug.LogError("The scene view map parent must be set to find objects!");
            return;
        }

        int totalMockTilesCount = SceneViewMapParent.transform.childCount;
        for (int i = 0; i < totalMockTilesCount; i++)
        {
            GameObject mockTileObj = SceneViewMapParent.transform.GetChild(i).gameObject;
            if (mockTileObj.TryGetComponent(out MonobehaviourTile mTile) == false)
            {
                continue;
            }

            if (mTile.MapCoords.z == Map.Count)
            {
                Map.Add(new());
            }

            Map[mTile.MapCoords.z].Values.Add(CreateTile(mTile));
        }
    }

    private Tile CreateTile(MonobehaviourTile mTile)
    {
        return new Tile()
        {
            WorldCoords = mTile.gameObject.transform.position,
            MapCoords = mTile.MapCoords,
            IsNavigable = mTile.IsNavigable
        };
    }

    [Button]
    public void GenerateSceneViewMap()
    {
        if (!SceneViewMapGeneratedParent || !TileMockObject)
        {
            Debug.LogError("The scene view map parent AND the tile mock object both need to be set to generate a scene view map!");
            return;
        }

        if (Map == null)
        {
            Debug.LogError("Make sure the Map field is not null!");
            return;
        }

        foreach (ListWrapper<Tile> zList in Map)
        {
            foreach (Tile xTile in zList.Values)
            {
                CreateMockTile(xTile);
            }
        }
    }

    private void CreateMockTile(Tile tileContext)
    {
        GameObject mockTile = GameObject.Instantiate(TileMockObject, SceneViewMapGeneratedParent.transform);
        MonobehaviourTile mTile = mockTile.AddComponent<MonobehaviourTile>();
        mTile.MapCoords = DataConversionHelper.TruncateVector3ToInt(tileContext.MapCoords);
        mockTile.transform.position = tileContext.WorldCoords;
        mockTile.SetActive(true);
    }

    /// <summary>
    /// Maintain as the Tile class grows.
    /// Converts the CSharp list object in this object to raw text to be used in a world tilemap preset file.
    /// </summary>
    /// <remarks>
    /// PrettyPrint was once developed for this, to get pretty print, a text file at "C:\Users\ashto\OneDrive\Documents\Development\External C#scripts\PrettyPrint TileMapBuilder GenerateTextFromCSharpMap Backup.txt"
    /// will contain the text of the prettyPrint implementation.
    /// </remarks>
    [Button]
    public string GenerateTextFromCSharpMap(bool generateEntireClassSignature = false)
    {
        if (Map == null)
        {
            Debug.LogError("The map must be set to generate FROM!");
            return string.Empty;
        }

        string text = "";
        if (generateEntireClassSignature == true)
            text = "using System.Collections.Generic;using UnityEngine;namespace WorldMapData.Maps{public static class MyMapObject{public static readonly string MapName=\"\";public static readonly List<ListWrapper<Tile>> Tiles=";

        text += "new List<ListWrapper<Tile>>{";
        foreach (ListWrapper<Tile> objChildList in Map)
        {
            text += "new ListWrapper<Tile>{Values=new List<Tile>{";
            foreach (Tile objChild in objChildList.Values)
            {
                text += "new Tile{";
                text += mapCoordsText + "=" + Vector3IntToText(objChild.MapCoords) + ",";
                text += worldCoordsText + "=" + Vector3ToText(objChild.WorldCoords) + ",";
                text += isNavigableText + "=" + BoolToText(objChild.IsNavigable) + ",";
                text += "},";
            }

            text += "}},";
        }
        
        text += "};";

        if (generateEntireClassSignature == true)
            text += "}}";

        return text;
    }

    private string Vector3ToText(Vector3 vec3)
    {
        string vec3Text = "new Vector3(";
        vec3Text += vec3.x.ToString() + "f,";
        vec3Text += vec3.y.ToString() + "f,";
        vec3Text += vec3.z.ToString() + "f";
        return vec3Text + ")";
    }

    private string Vector3IntToText(Vector3Int vec3Int)
    {
        string vec3IntText = "new Vector3Int";
        vec3IntText += vec3Int.ToString();
        return vec3IntText;
    }

    private string BoolToText(bool boolean)
    {
        return boolean.ToString().ToLower();
    }

    /// <summary>
    /// Maintain the ExtractTileInfoFromText as the Tile object grows!
    /// This is error-prone, where you must input the text generated from the GenerateTextFromCSharpMap method
    /// without the full class signature or what is contained in the Tiles field. 
    /// Generates a map from text, this is saved to Map1 as to not overwrite the Map that may not be saved anywhere else.
    /// </summary>
    /// <param name="text">The text that was generated from a GenerateTextFromCSharpMap in the Tiles field.</param>
    [Button]
    public void GenerateCSharpMap1FromText(string text)
    {
        ParseNextMapSection(text[(text.IndexOf('{') + 1)..], new());
    }

    private void ParseNextMapSection(string text, List<ListWrapper<Tile>> tilesObj)
    {
        // The first open bracket from the beginning of the string index
        int openBracketIndex = text.IndexOf('{');

        if (openBracketIndex == -1)
        {
            Map1 = tilesObj;
            return;
        }

        if (text[..openBracketIndex].Contains("new ListWrapper<Tile>"))
        {
            tilesObj.Add(new());
            text = text[(openBracketIndex + 1)..];
            text = text[(text.IndexOf('{') + 1)..]; // Just skip the whole "values = new List<Tile>{" instead
            ParseNextMapSection(text, tilesObj);
            return;
        }

        if (text[..openBracketIndex].Contains("new Tile"))
        {
            tilesObj[^1].Values.Add(new());
            text = text[(openBracketIndex + 1)..];
            tilesObj[^1].Values[^1] = ExtractTileInfoFromText(text, out string remainingText);
            ParseNextMapSection(remainingText, tilesObj);
            return;
        }
    }

    private Tile ExtractTileInfoFromText(string text, out string remainingText)
    {
        Tile tile = new();

        text = text[(text.IndexOf(mapCoordsText) + mapCoordsText.Length + 2)..];
        tile.MapCoords = ExtractVector3IntFromText(text, out string remainingText1);
        text = remainingText1;

        text = text[(text.IndexOf(worldCoordsText) + worldCoordsText.Length + 2)..];
        tile.WorldCoords = ExtractVector3FromText(text, out string remainingText2);
        text = remainingText2;

        text = text[(text.IndexOf(isNavigableText) + isNavigableText.Length + 2)..];
        tile.IsNavigable = ExtractBoolFromText(text, out string remainingText3);
        text = remainingText3;

        remainingText = text;
        return tile;
    }

    private Vector3 ExtractVector3FromText(string text, out string remainingText)
    {
        Vector3 vec3 = new();
        text = text[(text.IndexOf('(') + 1)..];
        vec3.x = float.Parse(text[..text.IndexOf('f')]);
        text = text[(text.IndexOf(',') + 1)..];

        vec3.y = float.Parse(text[..text.IndexOf('f')]);
        text = text[(text.IndexOf(',') + 1)..];

        vec3.z = float.Parse(text[..text.IndexOf('f')]);
        text = text[(text.IndexOf(')') + 1)..];

        remainingText = text;
        return vec3;
    }

    private Vector3Int ExtractVector3IntFromText(string text, out string remainingText)
    {
        Vector3Int vec3Int = new();
        text = text[(text.IndexOf('(') + 1)..];
        vec3Int.x = int.Parse(text[..text.IndexOf(',')]);
        text = text[(text.IndexOf(',') + 1)..];

        vec3Int.y = int.Parse(text[..text.IndexOf(',')]);
        text = text[(text.IndexOf(',') + 1)..];

        vec3Int.z = int.Parse(text[..text.IndexOf(')')]);
        text = text[(text.IndexOf(')') + 1)..];
        
        remainingText = text;
        return vec3Int;
    }

    private bool ExtractBoolFromText(string text, out string remainingText)
    {
        if (text.Substring(0, 4).Equals("true"))
        {
            remainingText = text[4..];
            return true;
        }

        remainingText = text[5..];
        return false;
    }
}
}