using UnityEngine;

namespace GameLoadingSystem
{

/// <summary>
/// Initialize many of the other systems for the administrator scene and call to load the current player.
/// This takes place on the Awake call.
/// </summary>
public class FirstPass : MonoBehaviour
{
    [SerializeField]
    private GameLoader gameLoader;

    [SerializeField]
    private ActiveProjectFile activeProjectFile;

    void Awake()
    {
        if (gameLoader == null)
        {
            Debug.LogError("The gameWorldLoader is not set!");
            return;
        }

        gameLoader.LoadGameTurnStart(firstPass: true);
    }
}
}