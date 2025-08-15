using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLoadingSystem
{

/// <summary>
/// Place entities onto the current player's map. This includes initializing their objects on it, and a method to allow other
/// navigational classes to move around the entity objects also.
/// </summary>
public static class EntityPlacement
{
    private const string EntitiesInSceneParentTag = "Entities In Scene Parent";
    private static ProjectFileSystem.ProjectFileHeader projectFileHeader;
    private static WorldMapData.WorldMap worldMap;

    /// <summary>
    /// Attempt to get the player's map, find all the entities within that map at the moment, and finally, move them to those positions.
    /// </summary>
    /// <param name="data"></param>
    public static void InitializeEntitiesOnMap(ProjectFileSystem.ProjectFileHeader data)
    {
        projectFileHeader = data;
        EntityData.Player currPlayer = projectFileHeader.PlayerData.GetCurrentPlayer();
        string currPlayerMapName = currPlayer.livingMapName;

        try
        {
            worldMap = projectFileHeader.WorldMapData.SearchForMap(currPlayerMapName);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            // Let the player load onto a map with some helper scene.
            return;
        }

        Transform entitiesInSceneParent;
        try
        {
            entitiesInSceneParent = GameObject.FindWithTag(EntitiesInSceneParentTag).transform;
        }
        catch (UnityException ue)
        {
            Debug.LogError("The entitiesInSceneParent gameObject was not found! : " + ue.Message);
            GameObject tempReplacement = GameObject.Instantiate(new GameObject());
            tempReplacement.tag = EntitiesInSceneParentTag;
            entitiesInSceneParent = tempReplacement.transform;
        }

        if (worldMap.IsMapTypeBattle == true)
        {
            foreach (EntityData.Entity entity in FindEntitiesInBattle())
            {
                entity.ActiveGameObject = GameObject.Instantiate(entity.EntityObjectAsset, entitiesInSceneParent);
                PlaceEntity(entity, mapCoords: worldMap.Tiles[entity.BattleTileCoords.z][entity.BattleTileCoords.x].MapCoords);
            }

            return;
        }

        foreach (EntityData.Player player in projectFileHeader.PlayerData.GetPlayers())
        {
            if (player.livingMapName.Equals(currPlayerMapName) == false)
            {
                continue;
            }

            player.ActiveGameObject = GameObject.Instantiate(player.EntityObjectAsset, entitiesInSceneParent);
            PlaceEntity(player, mapCoords: worldMap.Tiles[player.WorldTileCoords.z][player.WorldTileCoords.x].MapCoords);
        }
    }

    /// <summary>
    /// Place an entity in the world given the entity and coords and that its scene GameObject hasn't been destroyed.
    /// </summary>
    /// <exception cref="NullReferenceException"></exception>
    public static void PlaceEntity(EntityData.Entity entity, Vector3 mapCoords)
    {
        if (!entity.ActiveGameObject)
        {
            throw new NullReferenceException("The entity you are trying to move has had its scene gameObject destroyed.");
        }

        Vector3 tileFloatHeight = new(0f, entity.TileFloatHeight, 0f);
        entity.ActiveGameObject.transform.position = mapCoords + tileFloatHeight;
    }

    private static List<EntityData.Entity> FindEntitiesInBattle()
    {
        // Needs their to be a structure to store concurrent battle information (also serializable).

        return default;
    }
}
}