using System;
using TMPro;
using UnityEngine;

namespace UIUpdateSystem
{

/// <summary>
/// Connect UI messages to their corresponding data section in the ProjectFileHeader.
/// </summary>
public class UIDataLinker : MonoBehaviour
{
    [SerializeField]
    private ActiveProjectFile activeProjectFile;

    [SerializeField]
    private MapNavigationSystem.MoveSession moveSession;

    [SerializeField]
    private TextMeshProUGUI playerNameInfoText, playerTurnIndexInfoText, playerMoveCountText;

    public void LinkAll()
    {
        playerNameInfoText.text = "Player Name: " + activeProjectFile.Data.PlayerData.GetCurrentPlayer().DisplayName;
        playerTurnIndexInfoText.text = "Player Turn Index: " + activeProjectFile.Data.PlayerData.PlayerTurn.ToString();
    }

    void Update()
    {
        playerMoveCountText.text = "Move Count: " + moveSession.MoveCount.ToString();
    }
}
}