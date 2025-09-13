using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;

namespace EntityData
{

/// <summary>
/// The base class for all entities used in this project, this includes players.
/// </summary>
[Serializable]
public class Entity
{
    /// <summary>
    /// What coords does the entity currently live on? This is used with the secondary maps.
    /// </summary>
    public Vector3Int BattleTileCoords;

    /// <summary>
    /// What GameObject is this entity tied to?
    /// </summary>
    public GameObject EntityObjectAsset;

    protected IEnumerator LoadEntityObjectAsset(string key)
    {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(key);
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            EntityObjectAsset = handle.Result;
        }
        else
        {
            Debug.LogError("Failed to load the addressable at: " + key);
        }

        Addressables.Release(handle);
    }

    /// <summary>
    /// What GameObject in the current map can this entity be accessed at?
    /// </summary>
    public GameObject ActiveGameObject;

    /// <summary>
    /// How high should the entity's center be above the tile.
    /// </summary>
    public float TileFloatHeight;

    /// <summary>
    /// The turn that this entity is placed at in a battle. Used to delete the correct entity when having them leave the battle.
    /// </summary>
    public int BattleTurn;

    public void JoinBattle(BattleSystem.BattleContainer battle)
    {
        if (this is Player player)
            player.BattleGuidName = battle.GuidName;
        
        BattleTurn = battle.Entities.Count;
        battle.Entities.Add(this);
    }

    public void LeaveBattle(BattleSystem.BattleContainer battle)
    {
        battle.Entities.RemoveAt(BattleTurn);

        // Fix the turns of entities that came after the one leaving.
        for (int i = BattleTurn; i < battle.Entities.Count; i++)
            battle.Entities[i].BattleTurn--;

        if (battle.BattleTurnOrder >= BattleTurn)
            battle.BattleTurnOrder -= 1;

        if (this is Player player)
            player.BattleGuidName = string.Empty;
    }
}
}