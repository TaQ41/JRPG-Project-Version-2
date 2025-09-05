using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLoadingSystem
{

/// <summary>
/// 
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
    private DialogueMessageSystem.DialogueChainProcessor dialogueChainProcessor;

    [SerializeField]
    private ActiveProjectFile projectFile;
    private EntityData.Player CurrentPlayer { get {return projectFile.Data.PlayerData.GetCurrentPlayer();} }

    private Scene GameMapSceneContext;

    /// <summary>
    /// 
    /// </summary>
    public void LoadGameTurnStart(bool firstPass = false)
    {
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

        LoadGameTurnWorld();
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

    /// <summary>
    /// 
    /// </summary>
    public void LoadGameTurnWorld()
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

        // Placing entities on map load - II
        CameraPlacement.UnlinkCamera(cameraObj);
        entityPlacement.FlushEntitiesOnMap();

        EntityPlacement.InitializeEntitiesOnMap(projectFile.Data);
        CameraPlacement.TryLinkCameraToEntity(projectFile.Data.PlayerData.GetCurrentPlayer(), cameraObj);

        // Readying player UI - III
        UIDisplayer.Show(UIDisplayer.PlayerInfoCanvas);
        UIDataLinker.LinkPlayerInfo();

        // Compile player messages for effects - IV
        DialogueMessageSystem.DialogueContainer[] compiledPlayerMessages = CompilePlayerMessageEffects();
        dialogueChainProcessor.LoadInitial(compiledPlayerMessages);

        // Enable Input - V
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    private async Task LoadGameMap()
    {
        EntityData.Player player = CurrentPlayer;
        string loadedMapName = player.livingMapName;

        try
        {
            await SceneManager.LoadSceneAsync(loadedMapName + " Scene", LoadSceneMode.Additive);
            GameMapSceneContext = SceneManager.GetSceneByName(loadedMapName + " Scene");
        }
        catch
        {
            SceneTransitionSystem.TransitionManagerUser.LoadTransitionErrorHelper();
            throw new NullReferenceException("Scene name (" + loadedMapName + ") was not found!");
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

        SceneManager.UnloadSceneAsync(GameMapSceneContext);
        LoadGameTurnStart();
    }
}
}