using System;
using System.Collections.Generic;
using UnityEngine;

namespace WorldMapData
{

/// <summary>
/// A world map is a 2d list of tiles that centers around letting objects access those tiles.
/// These should be built with the builder section in the WorldMap folder or the files in this child namespace of 'Builder'
/// </summary>
[Serializable]
public class WorldMap
{
    [SerializeReference]
    public List<List<Tile>> Tiles = new();

    public Tile this[int xCoord, int zCoord]
    {
        get
        {
            return Tiles[zCoord][xCoord];
        }

        set
        {
            Tiles[zCoord][xCoord] = value;
        }
    }

    public bool TryGetTile(int xCoord, int zCoord, out Tile tile)
    {
        if (xCoord < 0 || zCoord < 0)
        {
            tile = default(Tile);
            return false;
        }

        if (zCoord >= Tiles.Count || xCoord >= Tiles[zCoord].Count)
        {
            tile = default(Tile);
            return false;
        }

        tile = Tiles[zCoord][xCoord];
        return true;
    }
}
}