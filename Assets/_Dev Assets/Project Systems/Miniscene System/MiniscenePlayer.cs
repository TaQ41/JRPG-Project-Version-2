using System;
using System.Collections.Generic;
using UnityEngine;
using SceneTransitionSystem;
using UnityEngine.SceneManagement;
using Sirenix.Utilities;

namespace MinisceneSystem
{

/// <summary>
/// Play through a list of miniscenes in order and waiting for each miniscene process to finish.
/// </summary>
public class MiniscenePlayer : MonoBehaviour
{
    [SerializeField]
    private TransitionManagerUser transitionManagerUser;

    [SerializeField]
    private List<GameObject> Miniscenes;
    private int minisceneIndex = -1;
    public static Action MinisceneEnded;

    public void Awake()
    {
        MinisceneEnded += TryNextMiniscene;
        if (Miniscenes.IsNullOrEmpty())
        {
            LoadHomeMenu();
            return;
        } 

        TryNextMiniscene();
    }

    [Sirenix.OdinInspector.Button]
    private void TryNextMiniscene()
    {
        minisceneIndex += 1;
        if (minisceneIndex >= Miniscenes.Count)
        {
            LoadHomeMenu();
            return;
        }
        
        GameObject miniscene = Miniscenes[minisceneIndex];
        if (miniscene == null)
        {
            Debug.LogError("Miniscene value was null! Index: " + minisceneIndex);
            MinisceneEnded.Invoke();
            return;
        }

        miniscene.SetActive(true);
    }

    private void LoadHomeMenu()
    {
        transitionManagerUser.Transition(GenericTransitionManager.SceneNames.HomeMenu);
    }
}
}