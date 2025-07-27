using UnityEngine;

namespace WorldMapData.Builder
{

public class MonobehaviourTile : MonoBehaviour
{
    public bool IsNavigable = true;

    [Tooltip("The world coords of the tile are instead found where this tile is placed in the actual project.")]
    public Vector3 MapCoords;
}
}