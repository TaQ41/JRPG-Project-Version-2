using System;
using UnityEngine;

namespace EntityData
{

/// <summary>
/// 
/// </summary>
[Serializable]
public class Goblin : Entity
{
    private const string PrefabKey = "Goblin";
    public override string GetPrefabKey() => PrefabKey;
    
    public Goblin()
    {
        TileFloatHeight = 1f;
    }
}
}