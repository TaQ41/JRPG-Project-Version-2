using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLoadingSystem
{

/// <summary>
/// Load the game of the current player, initialize it, and end it.
/// </summary>
public class GameLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraObj;

    [SerializeField] private UIUpdateSystem.UIDataLinker UIDataLinker;
    [SerializeField] private UIUpdateSystem.UIDisplayer UIDisplayer;

    [SerializeField]
    private EntityPlacement entityPlacement;

    [SerializeField]
    private MenuNavigationSystem.MenuPageManager gameviewPlayerActionsMenu;

    [SerializeField]
    private DialogueMessageSystem.DialogueChainProcessor dialogueChainProcessor;

    [SerializeField]
    private BattleSystem.BattleHandler battleHandler;

    [SerializeField]
    private ActiveProjectFile projectFile;
    private EntityData.Player CurrentPlayer = null;

    private string GameMapSceneContext = string.Empty;

    /// <summary>
    /// Resets menus, handles player turn index switching, and begins events that have been pending.
    /// Called before the player's turn begins and after the player's turn ends.
    /// </summary>
    public void LoadGameTurnStart(bool firstPass = false)
    {
        CurrentPlayer = projectFile.Data.PlayerData.GetCurrentPlayer();
        gameviewPlayerActionsMenu.Awake();

        // Disable all player input
        if (firstPass == false)
        {
            projectFile.Data.PlayerData.IncrementTurnIndex();
            if (projectFile.Data.PlayerData.PlayerTurn == 0)
            {
                projectFile.Data.TotalTurnCycleCount += 1;
            }
        }

        try
        {
            TryActivatePendingGameEvent();
        }
        catch (NotImplementedException)
        {
            Debug.Log("The game events have not been implemented.");
        }

        LoadGameWorld();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private bool TryActivatePendingGameEvent()
    {
        throw new NotImplementedException();
        /*if (projectFile.Data.NextGameEvent.ActivationCycle <= projectFile.Data.TotalTurnCycleCount)
        {
            // Call the game event of this game event's id
            return true;
        }

        return false;*/
    }

    public void LoadGameWorld()
    {
        // Map loading - I
        try
        {
            _ = LoadGameMap();
        }
        catch
        {
            return; // The transition error helper scene or the error helper scene has been loaded, stop here.
        }

        CameraPlacement.UnlinkCamera(cameraObj);
        entityPlacement.FlushEntitiesOnMap();

        entityPlacement.InitializeEntitiesOnMap(projectFile.Data);
        CameraPlacement.TryLinkCameraToEntity(CurrentPlayer, cameraObj);

        if (CurrentPlayer.BattleGuidName.Equals(string.Empty))
        {
            LoadPlayerWorld();
            return;
        }

        LoadBattleWorld();
    }

    /// <summary>
    /// 
    /// </summary>
    private void LoadPlayerWorld()
    {
        // Readying player UI - III
        UIDataLinker.LinkAll();
        UIDisplayer.Show(UIDisplayer.PlayerInfoCanvas);

        // Compile player messages for effects - IV
        DialogueMessageSystem.DialogueContainer[] compiledPlayerMessages = CompilePlayerMessageEffects();
        dialogueChainProcessor.LoadInitial(compiledPlayerMessages);

        // Enable Input - V
    }

    private void LoadBattleWorld()
    {
        Debug.Log("BattleWorld is loading");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    private async Task LoadGameMap()
    {
        if (!GameMapSceneContext.Equals(string.Empty))
            await SceneManager.UnloadSceneAsync(GameMapSceneContext);

        if (CurrentPlayer.BattleGuidName.Equals(string.Empty))
            GameMapSceneContext = CurrentPlayer.livingMapName;
        else
            GameMapSceneContext = battleHandler.GetBattleByGuidName(CurrentPlayer.BattleGuidName).BattleMap.MapName;

        GameMapSceneContext += " Scene";

        try
        {
            await SceneManager.LoadSceneAsync(GameMapSceneContext, LoadSceneMode.Additive);
        }
        catch
        {
            SceneTransitionSystem.TransitionManagerUser.LoadTransitionErrorHelper();
            throw new NullReferenceException("Scene name (" + GameMapSceneContext + ") was not found!");
        }
    }

    /// <summary>
    /// Get effects on the player that have an attached dialogueContainer's message which isn't empty.
    /// </summary>
    /// <returns></returns>
    private DialogueMessageSystem.DialogueContainer[] CompilePlayerMessageEffects()
    {
        //throw new NotImplementedException();
        return new DialogueMessageSystem.DialogueContainer[0];
    }

    /// <summary>
    /// Called when the player's turn ends by whatever means. Hides the screen and unloads all loaded assets before beginning the next turn.
    /// </summary>
    public IEnumerator EndTurn()
    {
        float wrapperTimeToHide = 1f; // Delete this when creating the actual wrapper object.

        float elapsedTime = 0f;
        while (elapsedTime < wrapperTimeToHide)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        LoadGameTurnStart();
    }
}
}