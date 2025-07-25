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
        public static readonly string HomeMenu = "Home Menu Scene";
        public static readonly string GameIntroduction = "Game Introduction Scene";
        public static readonly string GameLoaderAdministrator = "Game Loader Administrator Scene";
    }

    /// <summary>
    /// Invoked when the process comes to an end, will return false or true depending on the result of loading the scenes.
    /// </summary>
    public static Action<bool> OnEnd;

    public static readonly string TransitionSceneName = "Generic Transition Scene";

    /// <summary>
    /// Transition to a scene by provided the TO scene and the calling scene.
    /// </summary>
    /// <returns>False on a failure of loading any scenes, this can include the transition scene too. True on all sceneloading working.</returns>
    public static async Task TransitionToScene(string sceneName, Scene prevScene)
    {
        try
        {
            await SceneManager.LoadSceneAsync(TransitionSceneName, LoadSceneMode.Additive);
        }
        catch
        {
            OnEnd.Invoke(false);
            return;
        }

        try
        {
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        catch
        {
            OnEnd.Invoke(false);
            return;
        }
        finally
        {
            await SceneManager.UnloadSceneAsync(TransitionSceneName);
        }

        OnEnd.Invoke(true);

        await SceneManager.UnloadSceneAsync(prevScene);
    }
}
}