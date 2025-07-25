using UnityEngine;
using UnityEngine.EventSystems;

namespace MenuNavigationSystem
{

/// <summary>
/// Change the current scene by using the linked transitionManagerUser instance.
/// After that, the transition system will handle everything else.
/// </summary>
public class MenuButtonChangeScene : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private SceneTransitionSystem.TransitionManagerUser transitionManagerUser;

    [SerializeField]
    private string sceneNameToLoad;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (transitionManagerUser == null)
        {
            Debug.LogError("The TransitionManagerUser field is null!");
            return;
        }

        transitionManagerUser.Transition(sceneNameToLoad);
    }
}
}