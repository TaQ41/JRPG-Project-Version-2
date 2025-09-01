using System.Collections.Generic;
using UnityEngine;

namespace WorldMapData.Builder
{

public class ZTileNegativityResolver : MonoBehaviour
{
    [SerializeField]
    private List<ListWrapper<Tile>> builderWorldMap;

    [Sirenix.OdinInspector.Button]
    private void ResolveZTileNegativityWithBuilderMap()
    {
        Debug.Log(Time.realtimeSinceStartupAsDouble);
        int myInt = ResolveZTileNegativity(builderWorldMap);
        Debug.Log(Time.realtimeSinceStartupAsDouble);
        Debug.Log("negativity counter: " + myInt);
    }

    /// <summary>
    /// Fix negative values in tile's zIndex's. If they have negative values, accessing methods in the worldMap will fail.
    /// Relatively expensive in proportion to the size of the worldMap; When dynamically adding tiles, check if any of
    /// the newly added tiles have negative zIndex's, if so, then call this.
    /// </summary>
    /// <remarks>
    /// _____ side effect _____, in which, the value passed to this method, will be directly affected by reference.
    /// So, this will affect the value of the variable that was passed into this method.
    /// </remarks>
    /// <param name="worldMap">What worldMap should this try to resolve?</param>
    /// <returns>
    /// Return the positive value that this increased every z index of every tile by.
    /// A return of 0 means that this didn't do anything.
    /// </returns>
    public static int ResolveZTileNegativity(List<ListWrapper<Tile>> map)
    {
        int negativityValue = 0;
        foreach (ListWrapper<Tile> zList in map)
        {
            // This assumes that each list will always contain at least one tile, otherwise.. why is it even there?
            int zIndex = zList[0].MapCoords.z;
            if (zIndex < negativityValue)
                negativityValue = zIndex;
        }

        // negativityValue will be a negative integer.
        if (negativityValue == 0)
            return 0;
        
        foreach (ListWrapper<Tile> zList in map)
        {
            foreach (Tile tile in zList.Values)
            {
                tile.MapCoords += new Vector3Int(0, 0, -negativityValue);
            }
        }

        return -negativityValue;
    }
}
}