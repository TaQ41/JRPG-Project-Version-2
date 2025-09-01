using UnityEngine;
using UnityEngine.EventSystems;

namespace MenuNavigationSystem
{

/// <summary>
/// Change the current page in a linked MenuPageManager instance.
/// This may try to close the current page or try to open a new page.
/// Provide a MenuPageDetails struct if opening a new page, while closing a page will only need to execute the method.
/// </summary>
public class MenuButtonChangePage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] 
    private MenuNavigationSystem.MenuPageManager menuPageManager;

    [SerializeField, Tooltip("This should open a specified page. If this is false, the most recent page opened should close, if possible.")]
    private bool ShouldOpenMenuPage;

    [SerializeField, Sirenix.OdinInspector.ShowIf("ShouldOpenMenuPage")]
    private MenuNavigationSystem.MenuPageDetails menuPageDetails;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (menuPageManager == null)
        {
            Debug.LogError("Set the menuPageManager instance!");
            return;
        }

        if (ShouldOpenMenuPage == true)
        {
            menuPageManager.TryOpenMenuPage(menuPageDetails);
            return;
        }

        menuPageManager.TryCloseTopMenuPage();
    }
}
}