using System;
using UnityEngine;

namespace BattleSystem
{

/// <summary>
/// 
/// </summary>
public class BattleHandler : MonoBehaviour
{
    [SerializeField]
    private ActiveProjectFile activeProjectFile;

    [SerializeField]
    private EntityData.EntityBrowser entityBrowser;

    /// <summary>
    /// Generates a new battle and appends it to the currentBattles list in the activepProjectFile.
    /// Use the JoinBattle method from EntityData.Entity to append entities into the battle.
    /// </summary>
    /// <returns>
    /// The index that this is located at in the currentBattles list.
    /// NOTE: Only use this immediately after the battle has been generated, this value cannot be garuanteed to be accurate when battles are added or removed.
    /// </returns>
    public int GenerateBattle(WorldMapData.WorldMap battleMap)
    {
        BattleContainer _battle = new()
        {
            GuidName = Guid.NewGuid().ToString(),
            BattleTurnOrder = 0,
            Entities = new(),
            BattleMap = battleMap
        };

        activeProjectFile.Data.CurrentBattles.Add(_battle);
        return activeProjectFile.Data.CurrentBattles.Count - 1;
    }

    public BattleContainer GetBattleByGuidName(string guidName)
    {
        foreach (BattleContainer battle in activeProjectFile.Data.CurrentBattles)
        {
            if (battle.GuidName.Equals(guidName))
            {
                return battle;
            }
        }

        return default;
    }

    public void JoinBattle(EntityData.Entity entity, BattleContainer battle)
    {
        if (entity is EntityData.Player player)
        {
            player.BattleGuidName = battle.GuidName;
        }
        else
        {
            GameObject prefab = entityBrowser.BrowseForEntity(entity.GetPrefabKey());
            if (prefab != null)
                entity.EntityObjectAsset = prefab;
        }
        
        entity.BattleTurn = battle.Entities.Count;
        battle.Entities.Add(entity);
    }

    public void LeaveBattle(EntityData.Entity entity, BattleContainer battle)
    {
        battle.Entities.RemoveAt(entity.BattleTurn);

        // Fix the turns of entities that came after the one leaving.
        for (int i = entity.BattleTurn; i < battle.Entities.Count; i++)
            battle.Entities[i].BattleTurn--;

        if (battle.BattleTurnOrder >= entity.BattleTurn)
            battle.BattleTurnOrder -= 1;

        if (entity is EntityData.Player player)
            player.BattleGuidName = string.Empty;
    }
}
}