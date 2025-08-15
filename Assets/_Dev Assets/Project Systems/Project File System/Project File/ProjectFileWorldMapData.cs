using System;
using System.Collections.Generic;
using UnityEngine;
using WorldMapData;

namespace ProjectFileSystem
{

/// <summary>
/// 
/// </summary>
[Serializable]
public struct ProjectFileWorldMapData
{
    [SerializeField]
    private List<WorldMap> WorldMaps;
    public readonly void AddMap(WorldMap worldMap)
    {
        try
        {
            SearchForMap(worldMap.MapName);
        }
        catch
        {
            WorldMaps.Add(worldMap);
            return;
        }
    
        Debug.LogError("This map has already been added to the worldMaps!");
    }

    /// <summary>
    /// Searchs for a map in WorldMaps that matchs the given mapName.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public readonly WorldMap SearchForMap(string mapName)
    {
        foreach (WorldMap worldMap in WorldMaps)
        {
            if (worldMap.MapName.Equals(mapName))
            {
                return worldMap;
            }
        }

        throw new KeyNotFoundException("No worldMap with the name \"" + mapName + "\" was found in the WorldMaps list of the projectFileHeader!");
    }
}
}