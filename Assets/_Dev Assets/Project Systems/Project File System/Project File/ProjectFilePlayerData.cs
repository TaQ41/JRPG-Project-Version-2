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

    [UnityEngine.SerializeField]
    private List<Player> Players;

    /// <remarks>
    /// Uses the ListIndexExtensions.GetItem<> method and directly returns its result using the PlayerTurn as the index and the Players list as the values.
    /// </remarks>
    public readonly Player GetCurrentPlayer()
    {
        return GetPlayer(PlayerTurn);
    }

    public readonly Player GetPlayer(int index)
    {
        return Players.GetItem(index);
    }

    public readonly Player[] GetPlayers()
    {
        return Players.ToArray();
    }

    public int IncrementTurnIndex()
    {
        PlayerTurn = Players.IncrementIndex(PlayerTurn);
        return PlayerTurn;
    }
}
}