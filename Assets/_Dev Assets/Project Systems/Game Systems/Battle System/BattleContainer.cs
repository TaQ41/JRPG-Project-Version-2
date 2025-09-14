using System;
using System.Collections.Generic;
using WorldMapData;

namespace BattleSystem
{

/// <summary>
/// -
/// </summary>
[Serializable]
public struct BattleContainer
{
    public string GuidName;
    public int BattleTurnOrder;
    public List<EntityData.Entity> Entities;
    public WorldMap BattleMap;
}
}