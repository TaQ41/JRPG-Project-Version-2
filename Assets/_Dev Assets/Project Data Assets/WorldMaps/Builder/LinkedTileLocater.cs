using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace WorldMapData.Builder
{

/// <summary>
/// Find nearby tiles in the built map by using their MapCoords. This is used because not all MapCoords line up with WorldCoords, and it
/// is convenient to be able to know what is currently next to this object.
/// </summary>
public class LinkedTileLocater : MonoBehaviour
{
    public GameObject SceneViewMapParent;

    [Button]
    public List<GameObject> LocateNearbyTiles(MonobehaviourTile mTileOrigin)
    {
        List<GameObject> nearbyMockTiles = new();
        if (SceneViewMapParent == null)
        {
            Debug.LogError("The scene view map parent must be set to locate nearby tiles!");
            return default;
        }

        int totalMockTilesCount = SceneViewMapParent.transform.childCount;
        for (int i = 0; i < totalMockTilesCount; i++)
        {
            GameObject mockTile = SceneViewMapParent.transform.GetChild(i).gameObject;
            if (mockTile.TryGetComponent(out MonobehaviourTile mTile) == false)
            {
                continue;
            }

            if (Math.Abs(mTileOrigin.MapCoords.x - mTile.MapCoords.x) == 1)
            {
                if (CompareYZmTiles(mTileOrigin, mTile))
                {
                    nearbyMockTiles.Add(mTile.gameObject);
                    continue;
                }
            }

            if (Math.Abs(mTileOrigin.MapCoords.z - mTile.MapCoords.z) == 1)
            {
                if (CompareYXmTiles(mTileOrigin, mTile))
                {
                    nearbyMockTiles.Add(mTile.gameObject);
                    continue;
                }
            }
        }

        return nearbyMockTiles;
    }

    [Button]
    public List<GameObject> LocateNearbyTiles(GameObject mockTileObj)
    {
        if (mockTileObj.TryGetComponent(out MonobehaviourTile mTile) == false)
        {
            Debug.Log("This gameObject doesn't have a MonobehaviourTile component.");
            return default;
        }

        return LocateNearbyTiles(mTile);
    }

    private bool CompareYZmTiles(MonobehaviourTile mTileOrigin, MonobehaviourTile mTileComparer)
    {
        return
            mTileOrigin.MapCoords.y == mTileComparer.MapCoords.y &&
            mTileOrigin.MapCoords.z == mTileComparer.MapCoords.z;
    }

    private bool CompareYXmTiles(MonobehaviourTile mTileOrigin, MonobehaviourTile mTileComparer)
    {
        return
            mTileOrigin.MapCoords.y == mTileComparer.MapCoords.y &&
            mTileOrigin.MapCoords.x == mTileComparer.MapCoords.x;
    }
}
}