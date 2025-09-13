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
    private const string GoblinAddressableKey = "Assets/_Dev Assets/Project Data Assets/Entities/Goblin/Goblin Entity Object.prefab";
    
    public Goblin()
    {
        TileFloatHeight = 1f;   
        _ = LoadEntityObjectAsset(GoblinAddressableKey);
    }
}
}