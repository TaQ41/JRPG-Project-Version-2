using System;
using System.Collections.Generic;
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
    public ProjectFileWorldMapData WorldMapData;

    public List<BattleSystem.BattleContainer> CurrentBattles = new();
}
}