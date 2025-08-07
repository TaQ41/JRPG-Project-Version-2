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

    public void LinkAll()
    {
        LinkPlayerInfo();
    }

    public void LinkPlayerInfo()
    {
        LinkPlayerNameInfo();
        LinkPlayerTurnIndexInfo();
    }

    [SerializeField]
    private TextMeshProUGUI playerNameInfoText;

    private void LinkPlayerNameInfo()
    {
        try
        {
            playerNameInfoText.text = activeProjectFile.Data.PlayerData.GetCurrentPlayer().DisplayName;
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e.Message);
        }
    }


    [SerializeField]
    private TextMeshProUGUI playerTurnIndexInfoText;

    private void LinkPlayerTurnIndexInfo()
    {
        try
        {
            playerTurnIndexInfoText.text = activeProjectFile.Data.PlayerData.PlayerTurn.ToString();
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e.Message);
        }
    }
}
}