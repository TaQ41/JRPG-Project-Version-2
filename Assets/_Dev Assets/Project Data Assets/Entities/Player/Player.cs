using System;
using UnityEngine;

namespace EntityData
{

/// <summary>
/// The player is used by many systems as a means of deciding how to interact with the game.
/// </summary>
[Serializable]
public class Player : Entity
{
    private const string PrefabKey = "PlayerBase";
    public override string GetPrefabKey() => PrefabKey;

    [Sirenix.OdinInspector.Title ("Map Location", horizontalLine: false)]

    /// <summary>
    /// What world map is the player currently living in. This should not be changed to account for battle maps.
    /// </summary>
    public string livingMapName;

    /// <summary>
    /// The coords of the player on the primary map, this should not be used for battle maps.
    /// </summary>
    public Vector3Int WorldTileCoords;

    public string BattleGuidName = string.Empty;

    [Sirenix.OdinInspector.Title ("Player Identity")]

    public string DisplayName;
    public int TurnIndex;
}
}