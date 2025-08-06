using System.Collections.Generic;
using UnityEngine;

namespace WorldMapData.Maps
{

/// <summary>
/// 
/// </summary>
public static class TutorialMap
{
    public static readonly string MapName = "Tutorial";
    public static readonly List<ListWrapper<Tile>> Tiles = new List<ListWrapper<Tile>>{new ListWrapper<Tile>{Values=new List<Tile>{new Tile{MapCoords=new Vector3(0f,0f,0f),WorldCoords=new Vector3(0f,0f,0f),IsNavigable=false,},new Tile{MapCoords=new Vector3(1f,0f,0f),WorldCoords=new Vector3(1f,0f,0f),IsNavigable=false,},new Tile{MapCoords=new Vector3(2f,0f,0f),WorldCoords=new Vector3(2f,0f,0f),IsNavigable=false,},}},new ListWrapper<Tile>{Values=new List<Tile>{new Tile{MapCoords=new Vector3(0f,0f,1f),WorldCoords=new Vector3(0f,0f,1f),IsNavigable=false,},new Tile{MapCoords=new Vector3(1f,0f,1f),WorldCoords=new Vector3(1f,0f,1f),IsNavigable=false,},new Tile{MapCoords=new Vector3(2f,0f,1f),WorldCoords=new Vector3(2f,0f,1f),IsNavigable=false,},new Tile{MapCoords=new Vector3(3f,0f,1f),WorldCoords=new Vector3(3f,0f,1f),IsNavigable=false,},new Tile{MapCoords=new Vector3(-2f,0f,1f),WorldCoords=new Vector3(-2f,0f,1f),IsNavigable=false,},new Tile{MapCoords=new Vector3(-1f,0f,1f),WorldCoords=new Vector3(-1f,0f,1f),IsNavigable=false,},}},new ListWrapper<Tile>{Values=new List<Tile>{new Tile{MapCoords=new Vector3(0f,0f,2f),WorldCoords=new Vector3(0f,0f,2f),IsNavigable=false,},new Tile{MapCoords=new Vector3(1f,0f,2f),WorldCoords=new Vector3(1f,0f,2f),IsNavigable=false,},new Tile{MapCoords=new Vector3(2f,0f,2f),WorldCoords=new Vector3(2f,0f,2f),IsNavigable=false,},new Tile{MapCoords=new Vector3(-2f,0f,2f),WorldCoords=new Vector3(-2f,0f,2f),IsNavigable=false,},}},new ListWrapper<Tile>{Values=new List<Tile>{new Tile{MapCoords=new Vector3(1f,0f,3f),WorldCoords=new Vector3(1f,0f,3f),IsNavigable=false,},new Tile{MapCoords=new Vector3(-2f,0f,3f),WorldCoords=new Vector3(-2f,0f,3f),IsNavigable=false,},}},new ListWrapper<Tile>{Values=new List<Tile>{new Tile{MapCoords=new Vector3(1f,0f,4f),WorldCoords=new Vector3(1f,0f,4f),IsNavigable=false,},new Tile{MapCoords=new Vector3(0f,0f,4f),WorldCoords=new Vector3(0f,0f,4f),IsNavigable=false,},new Tile{MapCoords=new Vector3(-1f,0f,4f),WorldCoords=new Vector3(-1f,0f,4f),IsNavigable=false,},new Tile{MapCoords=new Vector3(-2f,0f,4f),WorldCoords=new Vector3(-2f,0f,4f),IsNavigable=false,},}},new ListWrapper<Tile>{Values=new List<Tile>{new Tile{MapCoords=new Vector3(1f,0f,5f),WorldCoords=new Vector3(1f,0f,5f),IsNavigable=false,},}},};
}
}