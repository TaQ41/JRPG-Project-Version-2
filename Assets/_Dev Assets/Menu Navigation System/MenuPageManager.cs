using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace MenuNavigationSystem
{

/// <summary>
/// Load and close applicable menu pages of a menu locally. On restarting the menu, this will
/// once again, be in a default state.
/// </summary>
public class MenuPageManager : MonoBehaviour
{
    [Sirenix.OdinInspector.ShowInInspector]
    private List<MenuPageDetails> menuPages, insetMenuPages;

    [SerializeField]
    private MenuPageDetails entryMenuPage;

    void Awake()
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

        menuPage.MenuPageParent.SetActive(true);
        if (menuPage.IsInset == false)
        {
            if (menuPages.Count != 0)
            {
                menuPages[^1].MenuPageParent.SetActive(false);
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

            menuPages[^2].MenuPageParent.SetActive(true);
            RemoveAndDeactivateLastMenuPage(menuPages);
            return true;
        }

        RemoveAndDeactivateLastMenuPage(insetMenuPages);
        return true;
    }

    /// <summary>
    /// NOTE: Called from the 'TryCloseTopMenuPage', so, there being at least one item has already been confirmed.
    /// </summary>
    private void RemoveAndDeactivateLastMenuPage(List<MenuPageDetails> menuPagesList)
    {
        menuPagesList[^1].MenuPageParent.SetActive(false);
        menuPagesList.RemoveAt(menuPagesList.Count - 1);
    }

    /// <summary>
    /// Deactivates each open inset menu page, then removes all of them from the list.
    /// </summary>
    private void DisableInsetMenuPages()
    {
        foreach (MenuPageDetails menuPage in insetMenuPages)
        {
            menuPage.MenuPageParent.SetActive(false);
        }

        insetMenuPages.Clear();
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