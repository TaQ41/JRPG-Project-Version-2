using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneTransitionSystem
{
    
/// <summary>
/// The user method to call into the GenericTransitionManager class.
/// Interprets the result of the TransitionManager through an action and may provide more direct scene access on failure.
/// </summary>
public class TransitionManagerUser : MonoBehaviour
{
    private const string TransitionErrorHelperSceneName = "Transition Error Helper Scene";

    [Sirenix.OdinInspector.Button]
    public void Transition(string _sceneName)
    {
        GenericTransitionManager.OnEnd += InterpretResult;
        _ = GenericTransitionManager.TransitionToScene(_sceneName, gameObject.scene);
    }

    private void InterpretResult(bool result)
    {
        GenericTransitionManager.OnEnd -= InterpretResult;

        if (result == false)
        {
            LoadTransitionErrorHelper();
            return;
        }
    }

    public static void LoadTransitionErrorHelper()
    {
        SceneManager.LoadScene(TransitionErrorHelperSceneName, LoadSceneMode.Additive);
    }
}
}