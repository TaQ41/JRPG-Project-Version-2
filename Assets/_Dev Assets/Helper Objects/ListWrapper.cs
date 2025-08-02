using System;
using System.Collections.Generic;
using WorldMapData;

/// <summary>
/// Allows double nested lists to be serialized natively by wrapping the secondary list in a class declaration.
/// </summary>
/// <typeparam name="T">Any serializable type</typeparam>
[Serializable]
public class ListWrapper<T>
{
    public List<T> values;
    public T this[int index]
    {
        get
        {
            return values[index];
        }
        set
        {
            values[index] = value;
        }
    }
}

public class TestClass
{
    public List<ListWrapper<Tile>> tiles =
    new List<ListWrapper<Tile>>
    {
        new ListWrapper<Tile>
        {
            values = new List<Tile>
            {
                new Tile {MapCoords=new(), WorldCoords=new(), IsNavigable=true},
            },
        }
    };
}