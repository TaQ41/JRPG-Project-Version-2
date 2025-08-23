using System;
using System.Collections.Generic;

namespace WorldMapData
{

/// <summary>
/// A world map is a 2d list of tiles that centers around letting objects access those tiles.
/// These should be built with the builder section in the WorldMap folder or the files in this child namespace of 'Builder'
/// </summary>
[Serializable]
public class WorldMap
{
    public List<ListWrapper<Tile>> Tiles = new();

    [UnityEngine.SerializeField]
    private bool m_isMapTypeBattle;
    public bool IsMapTypeBattle { get {return m_isMapTypeBattle;} }

    [UnityEngine.SerializeField]
    private string m_mapName;
    public string MapName { get {return m_mapName;} }

    public Tile this[int zCoord, int xCoord]
    {
        get
        {
            return GetTileInList(zCoord, xCoord);
        }

        set
        {
            Tiles[zCoord][xCoord] = value;
        }
    }

    public bool TryGetTile(int zCoord, int xCoord, out Tile tile)
    {
        tile = GetTileInList(zCoord, xCoord);

        if (tile == null)
        {
            return false;
        }

        return true;
    }

    private Tile GetTileInList(int zCoord, int xCoord)
    {
        ListWrapper<Tile> zList;
        try
        {
            zList = Tiles[zCoord];
        }
        catch
        {
            return null;
        }

        foreach (Tile xTile in zList.Values)
        {
            if ((int)xTile.MapCoords.x == xCoord)
            {
                return xTile;
            }
        }

        return null;
    }
}
}