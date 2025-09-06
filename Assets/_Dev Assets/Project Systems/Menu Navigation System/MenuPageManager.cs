using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace MenuNavigationSystem
{

/// <summary>
/// Load and close applicable menu pages of a menu locally. On restarting the menu, this will once again, be in a default state.
/// </summary>
public class MenuPageManager : MonoBehaviour
{
    [ShowInInspector]
    private List<MenuPageDetails> menuPages, insetMenuPages;

    [SerializeField]
    private MenuPageDetails entryMenuPage;

    /// <summary>
    /// Used to reset the Menu.
    /// </summary>
    public void Awake()
    {
        menuPages = new();
        insetMenuPages = new();
        TryOpenMenuPage(entryMenuPage);
    }

    /// <summary>
    /// Attempt to open a given menu page.
    /// If the page is inset, then the page behind it will not close.
    /// If the page is not inset, all inset pages will close and it will be the only open menu page.
    /// </summary>
    /// <returns>True if the menuPage opening process worked as expected, false otherwise.</returns>
    [Button]
    public bool TryOpenMenuPage(MenuPageDetails menuPage)
    {
        if (menuPage.MenuPageParent == null)
        {
            Debug.LogError("The menuPage object is null!");
            return false;
        }

        TryUpdateCanvasEnabledState(menuPage, true);
        if (menuPage.IsInset == false)
        {
            if (menuPages.Count != 0)
            {
                TryUpdateCanvasEnabledState(menuPages[^1], false);
            }

            DisableInsetMenuPages();
            menuPages.Add(menuPage);
        }
        else
        {
            insetMenuPages.Add(menuPage);
        }

        return true;
    }

    /// <summary>
    /// Attempts to close the most recently opened menu page (the top menu page).
    /// </summary>
    /// <returns>True if the menu page closing process worked as expected, false otherwise.</returns>
    [Button]
    public bool TryCloseTopMenuPage()
    {
        if (insetMenuPages.IsNullOrEmpty() == true)
        {
            if (menuPages.Count < 2)
            {
                return false;
            }

            TryUpdateCanvasEnabledState(menuPages[^2], true);
            RemoveAndDeactivateLastMenuPage(menuPages);
            return true;
        }

        RemoveAndDeactivateLastMenuPage(insetMenuPages);
        return true;
    }

    /// <summary>
    /// Used to update the enabled state of all menu pages that could be currently enabled to the user.
    /// </summary>
    /// <param name="enabledState"></param>
    public void ChangeMenuEnabledState(bool enabledState)
    {
        foreach (MenuPageDetails insetMenuPage in insetMenuPages)
            TryUpdateCanvasEnabledState(insetMenuPage, enabledState);

        TryUpdateCanvasEnabledState(menuPages[^1], enabledState);
    }

    /// <summary>
    /// NOTE: Called from the close page method, so, there being at least two items has already been confirmed.
    /// </summary>
    private void RemoveAndDeactivateLastMenuPage(List<MenuPageDetails> menuPagesList)
    {
        TryUpdateCanvasEnabledState(menuPagesList[^1], false);
        menuPagesList.RemoveAt(menuPagesList.Count - 1);
    }

    /// <summary>
    /// Deactivates each open inset menu page, then removes all of them from the list.
    /// </summary>
    private void DisableInsetMenuPages()
    {
        foreach (MenuPageDetails menuPage in insetMenuPages)
        {
            TryUpdateCanvasEnabledState(menuPage, false);
        }

        insetMenuPages.Clear();
    }

    /// <summary>
    /// Rather than activating and deactivating GameObjects, it would be better to enable/disable the canvas component on the menuPage.
    /// </summary>
    /// <returns>True on the canvas being found and updated, false otherwise.</returns>
    private bool TryUpdateCanvasEnabledState(MenuPageDetails menuPage, bool enabledState)
    {
        if (menuPage.MenuPageParent.TryGetComponent(out Canvas canvas) == false)
        {
            Debug.LogError("There was an issue discovering the canvas component on this menuPage.");
            return false;
        }

        canvas.enabled = enabledState;
        return true;
    }
}

[System.Serializable]
public struct MenuPageDetails
{
    /// <summary>
    /// The parent GameObject found in the menu that this struct should point to.
    /// </summary>
    public GameObject MenuPageParent;

    /// <summary>
    /// Should this page be partially layered on top of the previous page?
    /// </summary>
    public bool IsInset;
}
}