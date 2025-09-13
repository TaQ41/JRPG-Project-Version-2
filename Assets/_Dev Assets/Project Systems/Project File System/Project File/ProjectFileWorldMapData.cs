using System;
using System.Collections.Generic;
using UnityEngine;
using WorldMapData;

namespace ProjectFileSystem
{

/// <summary>
/// A data structure used to hold information about worldMaps. Holds the methods to interact with the list of worldMaps.
/// </summary>
[Serializable]
public struct ProjectFileWorldMapData
{
    [SerializeField]
    private List<WorldMap> WorldMaps;

    public readonly WorldMap this[int index]
    {
        get
        {
            return WorldMaps[index];
        }
    }

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
    /// Returns reference for player worldMaps, returns a new copy for battle maps.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public readonly WorldMap SearchForMap(string mapName)
    {
        foreach (WorldMap worldMap in WorldMaps)
        {
            if (worldMap.MapName.Equals(mapName))
            {
                if (worldMap.IsMapTypeBattle == true)
                    return worldMap.DeepCopy();

                return worldMap;
            }
        }

        throw new KeyNotFoundException("No worldMap with the name \"" + mapName + "\" was found in the WorldMaps list of the projectFileHeader!");
    }
}
}