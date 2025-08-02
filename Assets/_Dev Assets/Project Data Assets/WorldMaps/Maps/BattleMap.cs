using System.Collections.Generic;
using UnityEngine;

namespace WorldMapData.Maps
{

/// <summary>
/// 
/// </summary>
public static class BattleMap
{
    public static readonly string MapName = "Battle";
    public static readonly List<ListWrapper<Tile>> Tiles = new List<ListWrapper<Tile>>{new ListWrapper<Tile>{values=new List<Tile>{new Tile(){MapCoords=new Vector3(0f,0f,0f),WorldCoords=new Vector3(0f,0f,0f),IsNavigable=false,},new Tile(){MapCoords=new Vector3(1f,0f,0f),WorldCoords=new Vector3(1f,0f,0f),IsNavigable=false,},new Tile(){MapCoords=new Vector3(2f,0f,0f),WorldCoords=new Vector3(2f,0f,0f),IsNavigable=false,},}},new ListWrapper<Tile>{values=new List<Tile>{new Tile(){MapCoords=new Vector3(0f,0f,1f),WorldCoords=new Vector3(0f,0f,1f),IsNavigable=false,},new Tile(){MapCoords=new Vector3(1f,0f,1f),WorldCoords=new Vector3(1f,0f,1f),IsNavigable=false,},new Tile(){MapCoords=new Vector3(2f,0f,1f),WorldCoords=new Vector3(2f,0f,1f),IsNavigable=false,},}},new ListWrapper<Tile>{values=new List<Tile>{new Tile(){MapCoords=new Vector3(0f,0f,2f),WorldCoords=new Vector3(0f,0f,2f),IsNavigable=false,},new Tile(){MapCoords=new Vector3(1f,0f,2f),WorldCoords=new Vector3(1f,0f,2f),IsNavigable=false,},new Tile(){MapCoords=new Vector3(2f,0f,2f),WorldCoords=new Vector3(2f,0f,2f),IsNavigable=false,},}},};
}
}