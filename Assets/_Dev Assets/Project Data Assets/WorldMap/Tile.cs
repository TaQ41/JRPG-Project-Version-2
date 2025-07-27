using UnityEngine;

namespace WorldMapData
{

/// <summary>
/// A single object that is used to hold a position on a world map, this includes various properties that a tile interpreter can use to
/// control effects on the game.
/// </summary>
[System.Serializable]
public class Tile
{
    /// <summary>
    /// Used as a final setting to block players from landing on this tile.
    /// </summary>
    public bool IsNavigable = true;

    /// <summary>
    /// The coords of this tile on its map. (Need to be a single 1 point diff for navigation)
    /// </summary>
    public Vector3 MapCoords;

    /// <summary>
    /// The coords of the tile in the actual world. Used to map the player asset onto the tile.
    /// (The player placement will place the player slightly above these coords.)

    /// </summary>
    public Vector3 WorldCoords;
}
}