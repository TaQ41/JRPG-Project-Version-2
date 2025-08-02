using System;
using System.Collections.Generic;
using EntityData;

namespace ProjectFileSystem
{

/// <summary>
/// 
/// </summary>
[Serializable]
public struct ProjectFilePlayerData
{
    public int PlayerTurn;

    [UnityEngine.SerializeReference]
    private List<Player> Players;

    /// <remarks>
    /// Uses the ListIndexExtensions.GetItem<> method and directly returns its result using the PlayerTurn as the index and the Players list as the values.
    /// </remarks>
    public readonly Player GetCurrentPlayer()
    {
        return ListIndexExtensions.GetItem(PlayerTurn, Players);
    }

    public int IncrementTurnIndex()
    {
        PlayerTurn = ListIndexExtensions.IncrementIndex(PlayerTurn, Players);
        return PlayerTurn;
    }
}
}