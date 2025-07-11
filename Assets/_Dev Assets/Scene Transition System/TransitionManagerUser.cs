using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SceneTransitionSystem
{

/// <summary>
/// The user method to call into the GenericTransitionManager class.
/// Interprets the result of the TransitionManager through an action and may provide more direct scene access on failure.
/// </summary>
public class TransitionManagerUser : MonoBehaviour
{
    [Sirenix.OdinInspector.Button]
    public async Task Transition(string sceneName)
    {
        GenericTransitionManager.OnEnd += InterpretResult;
        await GenericTransitionManager.TransitionToScene(sceneName, gameObject.scene);
    }

    private void InterpretResult(bool result)
    {
        Debug.Log(result);
        GenericTransitionManager.OnEnd -= InterpretResult;
    }
}
}