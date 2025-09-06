using UnityEngine;
using UnityEngine.EventSystems;

namespace GameviewPlayerActions
{

/// <summary>
/// Begins the move session when the gameObject this is attached to is clicked.
/// </summary>
public class ConfirmMoveAction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private MenuNavigationSystem.MenuPageManager menuPageManager;
    [SerializeField] private MapNavigationSystem.MoveSession moveSession;

    public void OnPointerClick(PointerEventData eventData)
    {
        // May call a function here instead that calls back to do the following when it is finished.
        menuPageManager.ChangeMenuEnabledState(false);
        moveSession.BeginMoveSession();
    }
}
}