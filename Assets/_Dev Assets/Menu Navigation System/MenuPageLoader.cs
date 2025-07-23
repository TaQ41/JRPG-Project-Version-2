using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MenuNavigationSystem
{

/// <summary>
/// Allows simple navigation across pages in a menu.
/// </summary>
public class MenuPageLoader : MonoBehaviour
{
    [ShowInInspector]
    private List<MenuPageDetails> menuPages = new();

    /// <summary>
    /// The page should be opened immediately on the menu loading.
    /// </summary>
    [SerializeField]
    private MenuPageDetails entryMenuPage;

    void Awake()
    {
        OpenMenuPage(entryMenuPage);
    }

    /// <summary>
    /// Validates the .MenuPageParent. Then, closes the last if not inset and possible. Finally, opens the new page and adds it to the list.
    /// </summary>
    /// <param name="menuPage">What menuPage will be added to the list and displayed?</param>
    [Button]
    public void OpenMenuPage(MenuPageDetails menuPage)
    {
        if (menuPage.MenuPageParent == null)
        {
            Debug.LogError("This menuPageDetails struct doesn't point to any menuPage!");
            return;
        }

        if (menuPage.IsInset == false && menuPages.Count != 0)
        {
            menuPages[^1].MenuPageParent.SetActive(false);
        }

        menuPage.MenuPageParent.SetActive(true);
        menuPages.Add(menuPage);
    }

    /// <summary>
    /// 
    /// </summary>
    [Button]
    public void CloseMenuPage()
    {
        if (menuPages.Count == 1)
        {
            return;
        }

        menuPages[^2].MenuPageParent.SetActive(true);
        menuPages[^1].MenuPageParent.SetActive(false);
        menuPages.RemoveAt(menuPages.Count - 1);
    }
}

[Serializable]
public struct MenuPageDetails
{
    /// <summary>
    /// The parent GameObject found in the menu that this struct should point to.
    /// </summary>
    public GameObject MenuPageParent;

    /// <summary>
    /// Should this page be layered on top of the previous page?
    /// </summary>
    public bool IsInset;
}
}