using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectFileSystem
{

/// <summary>
/// The entry point for accessing all data that is necessary to be saved.
/// </summary>
[Serializable]
public class ProjectFileHeader
{
    [SerializeField]
    private string m_projectGUID = Guid.NewGuid().ToString();
    public string ProjectGUID { get {return m_projectGUID;} }
    public string ProjectName;

    public int TotalTurnCycleCount;
    public GameEventData.GameEventCallingContext NextGameEvent;

    public ProjectFilePlayerData PlayerData;

    public List<WorldMapData.WorldMap> WorldMaps;
    public WorldMapData.WorldMap SearchForMap(string mapName)
    {
        foreach (WorldMapData.WorldMap worldMap in WorldMaps)
        {
            if (worldMap.MapName == mapName)
            {
                return worldMap;
            }
        }

        throw new Exception("No worldMap with the name \"" + mapName + "\" was found in the WorldMaps list of the projectFileHeader!");
    }
}
}