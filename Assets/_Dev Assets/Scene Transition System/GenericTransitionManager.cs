using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneTransitionSystem
{

/// <summary>
/// Load scenes with a wrapped transition scene by the time it takes for them to load. This is used typically for menus outside of the GameWorld.
/// </summary>
public static class GenericTransitionManager
{
    public readonly struct SceneNames
    {
        public static readonly string HomeMenuSceneName = "Home Menu Scene";
        public static readonly string GameIntroductionSceneName = "Game Introduction Scene";
    }

    /// <summary>
    /// Invoked when the process comes to an end, will return false or true depending on the result of loading the scenes.
    /// </summary>
    public static Action<bool> OnEnd;

    static readonly string TransitionSceneName = "Generic Transition Scene";

    /// <summary>
    /// Transition to a scene by provided the TO scene and the calling scene.
    /// If the optional build index of the calling scene is omitted or clearly invalid, it will be inferred from the active scene.
    /// UI focus will be set to none during this process and is only reset to the old scene on failure or to the new scene on success.
    /// </summary>
    /// <returns>False on a failure this can include the transition scene too. True on all sceneloading working.</returns>
    public static async Task TransitionToScene(string sceneName, Scene prevScene)
    {
        // Change UI focus to none.

        try
        {
            await SceneManager.LoadSceneAsync(TransitionSceneName, LoadSceneMode.Additive);
        }
        catch
        {
            // Change UI focus to previous
            OnEnd.Invoke(false);
            return;
        }

        try
        {
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        catch
        {
            // Change UI focus to previous
            OnEnd.Invoke(false);
            return;
        }
        finally
        {
            await SceneManager.UnloadSceneAsync(TransitionSceneName);
        }

        // Change UI focus to the new scene
        OnEnd.Invoke(true);

        await SceneManager.UnloadSceneAsync(prevScene);
    }
}
}