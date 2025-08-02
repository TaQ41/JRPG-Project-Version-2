using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using WorldMapData.Builder;

public class CoordsLinker : MonoBehaviour
{
    public GameObject SceneViewParent;

    public List<GameObject> MockTiles = new();
    public List<MonobehaviourTile> mTiles = new();

    [Button]
    public void FillListFromSceneView(bool useMockTileList = false)
    {
        if (SceneViewParent == null)
        {
            Debug.LogError("The scene view parent field must be set!");
            return;
        }

        mTiles = new List<MonobehaviourTile>();

        if (useMockTileList == true)
        {
            foreach (GameObject mockTile in MockTiles)
            {
                if (mockTile.TryGetComponent(out MonobehaviourTile mTile) == true)
                {
                    mTiles.Add(mTile);
                }
            }
            return;
        }

        int sceneViewParentChildCount = SceneViewParent.transform.childCount;
        for (int i = 0; i < sceneViewParentChildCount; i++)
        {
            if (SceneViewParent.transform.GetChild(i).TryGetComponent(out MonobehaviourTile mTile) == true)
            {
                mTiles.Add(mTile);
            }
        }
    }

    [Button]
    [Tooltip ("Map Coords should be set to the world coords on the tiles. (Map coords are set to the tile position)")]
    public void MapWorldCoordsOntoMapCoords()
    {
        foreach (MonobehaviourTile mTile in mTiles)
        {
            Vector3 position = mTile.transform.position;
            mTile.MapCoords = new Vector3Int((int)position.x, (int)position.y, (int)position.z);
        }
    }

    [Button]
    [Tooltip ("World Coords should be set to the map coords on the tiles. (The tile position is set to the map coords)")]
    public void MapMapCoordsOntoWorldCoords()
    {
        foreach (MonobehaviourTile mTile in mTiles)
        {
            mTile.transform.position = mTile.MapCoords;
        }
    }
}