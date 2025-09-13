using System;
using System.Collections.Generic;
using EntityData;

namespace ProjectFileSystem
{

/// <summary>
/// A data structure that is used to hold information about players and index through them.
/// </summary>
[Serializable]
public struct ProjectFilePlayerData
{
    public int PlayerTurn;

    [UnityEngine.SerializeField]
    private List<Player> Players;

    /// <summary>
    /// Macro to getting the currentPlayer using the PlayerTurn in this class.
    /// </summary>
    /// <returns>Null on failure.</returns>
    public readonly Player GetCurrentPlayer()
    {
        return GetPlayer(PlayerTurn);
    }

    /// <summary>
    /// Get a player in the Players list with a given index.
    /// </summary>
    /// <returns>Null on an invalid index.</returns>
    public readonly Player GetPlayer(int index)
    {
        try
        {
            return Players[index];
        }
        catch
        {
            return null;
        }
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

    public readonly WorldMapData.WorldMap GetPlayerMap(Player player, ProjectFileWorldMapData projectFileWorldMapData)
    {
        string playerMapName = player.livingMapName;
        return projectFileWorldMapData.SearchForMap(playerMapName);
    }
}
}