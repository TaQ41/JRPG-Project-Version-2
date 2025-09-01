using UnityEngine;

namespace WorldMapData.Builder
{

/// <summary>
/// This object is used to emulate a Tile class, but allow it to be placed on mockTiles for the GameWorldBuilders.
/// </summary>
public class MonobehaviourTile : MonoBehaviour
{
    public bool IsNavigable = true;

    [Tooltip("The world coords of the tile are instead found where this tile is placed in the actual project.")]
    public Vector3Int MapCoords;
}
}