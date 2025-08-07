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
    public Vector3Int LivingTileCoords;

    /// <summary>
    /// What GameObject is this entity tied to?
    /// </summary>
    public GameObject EntityObjectAsset;

    /// <summary>
    /// What GameObject in the current map can this entity be accessed at?
    /// </summary>
    public GameObject ActiveGameObject;

    /// <summary>
    /// How high should the entity's center be above the tile.
    /// </summary>
    public float TileFloatHeight;
}
}