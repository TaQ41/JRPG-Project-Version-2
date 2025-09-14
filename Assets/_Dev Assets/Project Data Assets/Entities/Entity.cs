using System;
using UnityEngine;

namespace EntityData
{

/// <summary>
/// The base class for all entities used in this project, this includes players.
/// </summary>
[Serializable]
public class Entity
{
    /// <summary>
    /// What coords does the entity currently live on? This is used with the secondary maps.
    /// </summary>
    public Vector3Int BattleTileCoords;

    /// <summary>
    /// What GameObject is this entity tied to?
    /// </summary>
    public GameObject EntityObjectAsset;

    /// <summary>
    /// The key of the entity's base GameObject found in the EntityBrowser
    /// </summary>
    public virtual string GetPrefabKey() => string.Empty;

    /// <summary>
    /// What GameObject in the current map can this entity be accessed at?
    /// </summary>
    public GameObject ActiveGameObject;

    /// <summary>
    /// How high should the entity's center be above the tile.
    /// </summary>
    public float TileFloatHeight;

    /// <summary>
    /// The turn that this entity is placed at in a battle. Used to delete the correct entity when having them leave the battle.
    /// </summary>
    public int BattleTurn;
}
}