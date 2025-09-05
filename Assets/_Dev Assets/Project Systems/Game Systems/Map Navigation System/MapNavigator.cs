using System;
using System.Collections.Generic;
using UnityEngine;
using WorldMapData;

namespace MapNavigationSystem
{

/// <summary>
/// Refactored version to navigate through tiles.
/// </summary>
public class MapNavigator : MonoBehaviour
{
    [SerializeField]
    private MoveSession sessionUser;

    public WorldMap WorldMap { get; set; }

    /// <summary>
    /// Records the tile coords that the entity was on before it moved.
    /// Starts with the original position. This is so the CurrTile can be guaranteed to be found.
    /// </summary>
    public List<Vector3Int> MoveHistory { get; set; }
    private Tile CurrTile;

    public void MoveEntity(Vector2Int dir)
    {
        // Initializations
        Vector3Int currPos = MoveHistory[^1];
        Vector3Int destPos = currPos + new Vector3Int(dir.x, 0, dir.y);
        CurrTile = WorldMap[currPos.z, currPos.x];
        
        if (MoveHistory.Count > 1 && MoveHistory[^2] == destPos)
        {
            MoveBackward();
            return;
        }

        // Not enough moves to move on this tile.
        int moveDecrementCount = CurrTile.MoveDecrementAmount;
        if (sessionUser.MoveCount - moveDecrementCount < 0)
        {
            Debug.Log("Not enough moves detected!");
            return;
        }

        // The next tile doesn't exist.
        if (WorldMap.TryGetTile(destPos.z, destPos.x, out Tile destTile) == false)
        {
            Debug.Log($"Tile not located! x: {destPos.x}, z: {destPos.z}");
            return;
        }

        // Placeholder until player effects and tile properties can affect these.
        // The tile isn't within the required y-range to be moved to.
        if (IsInYRange(destTile, 1) == false)
        {
            Debug.Log("Too much y diff.");
            return;
        }

        MoveHistory.Add(destPos);
        sessionUser.MoveCount -= moveDecrementCount;
        sessionUser.PassTile(destTile);
    }

    private bool IsInYRange(Tile destTile, int maxYDiff)
    {
        int yDiff = Math.Abs(destTile.MapCoords.y - CurrTile.MapCoords.y);
        return maxYDiff >= yDiff;
    }

    public void MoveBackward()
    {
        if (MoveHistory.Count == 1)
            return;

        Vector3Int destPos = MoveHistory[^2];
        MoveHistory.RemoveAt(MoveHistory.Count - 1);
        
        Tile destTile = WorldMap[destPos.z, destPos.x];

        sessionUser.MoveCount += destTile.MoveDecrementAmount;
        sessionUser.PassTile(destTile);
    }
}
}