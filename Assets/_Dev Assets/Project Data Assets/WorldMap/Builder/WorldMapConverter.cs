using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace WorldMapData.Builder
{

/// <summary>
/// Convert a map to mock tiles in the scene view or in reverse. This is a convenience object used to make building tile maps for the JRPG much nicer.
/// </summary>
public class WorldMapConverter : SerializedMonoBehaviour
{
    [SerializeField]
    private List<List<Tile>> Map;

    public GameObject SceneViewMapParent;
    public GameObject SceneViewMapGeneratedParent;
    public GameObject TileMockObject;

    [Button]
    public void GenerateCSharpMap()
    {
        Map.Clear();
        if (SceneViewMapParent == null)
        {
            Debug.LogError("The scene view map parent must be set to find objects!");
            return;
        }

        int totalMockTilesCount = SceneViewMapParent.transform.childCount;
        for (int i = 0; i < totalMockTilesCount; i++)
        {
            GameObject mockTileObj = SceneViewMapParent.transform.GetChild(i).gameObject;
            if (mockTileObj.TryGetComponent(out MonobehaviourTile mTile) == false)
            {
                continue;
            }

            if (mTile.MapCoords.z == Map.Count)
            {
                Map.Add(new());
            }

            Map[(int)mTile.MapCoords.z].Add(CreateTile(mTile));
        }
    }

    private Tile CreateTile(MonobehaviourTile mTile)
    {
        return new Tile()
        {
            WorldCoords = mTile.gameObject.transform.position,
            MapCoords = mTile.MapCoords,
            IsNavigable = mTile.IsNavigable
        };
    }

    [Button]
    public void GenerateSceneViewMap()
    {
        if (!SceneViewMapGeneratedParent || !TileMockObject)
        {
            Debug.LogError("The scene view map parent AND the tile mock object both need to be set to generate a scene view map!");
            return;
        }

        if (Map == null)
        {
            Debug.LogError("Make sure the Map field is not null!");
            return;
        }

        foreach (List<Tile> zList in Map)
        {
            foreach (Tile xTile in zList)
            {
                CreateMockTile(xTile);
            }
        }
    }

    private void CreateMockTile(Tile tileContext)
    {
        GameObject mockTile = GameObject.Instantiate(TileMockObject, SceneViewMapGeneratedParent.transform);
        MonobehaviourTile mTile = mockTile.AddComponent<MonobehaviourTile>();
        mTile.MapCoords = tileContext.MapCoords;
        mockTile.transform.position = tileContext.WorldCoords;
        mockTile.SetActive(true);
    }
}
}