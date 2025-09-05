using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLoadingSystem
{

/// <summary>
/// Place entities onto the current player's map. This includes initializing their objects on it, and a method to allow other
/// navigational classes to move around the entity objects also.
/// </summary>
public class EntityPlacement : MonoBehaviour
{
    public const string EntitiesInSceneParentTag = "Entities In Scene Parent";
    private static ProjectFileSystem.ProjectFileHeader projectFileHeader;
    private static WorldMapData.WorldMap worldMap;

    public void FlushEntitiesOnMap()
    {
        Transform entitiesInSceneParent = GameObject.FindWithTag(EntitiesInSceneParentTag).transform;

        for (int i = 0; i < entitiesInSceneParent.childCount; i++) {
            Destroy(entitiesInSceneParent.GetChild(i).gameObject);
        }
    }

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

        Transform entitiesInSceneParent = GameObject.FindWithTag(EntitiesInSceneParentTag).transform;

        if (worldMap.IsMapTypeBattle == true)
        {
            foreach (EntityData.Entity entity in FindEntitiesInBattle())
            {
                entity.ActiveGameObject = GameObject.Instantiate(entity.EntityObjectAsset, entitiesInSceneParent);
                PlaceEntity(entity, worldCoords: worldMap.Tiles[entity.BattleTileCoords.z][entity.BattleTileCoords.x].WorldCoords);
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
            PlaceEntity(player, worldCoords: worldMap[player.WorldTileCoords.z, player.WorldTileCoords.x].WorldCoords);
        }
    }

    /// <summary>
    /// Place an entity in the world given coords.
    /// These coords map directly to the sceneView world space (combined with the entity's float height too!).
    /// Acts on the entity's ActiveGameObject. Make sure this is set.
    /// </summary>
    public static void PlaceEntity(EntityData.Entity entity, Vector3 worldCoords)
    {
        Vector3 tileFloatHeight = new(0f, entity.TileFloatHeight, 0f);
        entity.ActiveGameObject.transform.position = worldCoords + tileFloatHeight;
    }

    private static List<EntityData.Entity> FindEntitiesInBattle()
    {
        // Needs their to be a structure to store concurrent battle information (also serializable).

        return default;
    }
}
}