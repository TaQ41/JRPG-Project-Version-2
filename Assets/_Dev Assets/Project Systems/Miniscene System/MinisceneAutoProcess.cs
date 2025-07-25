using UnityEngine;

namespace MinisceneSystem
{

/// <summary>
/// Automatically activate and deactivate the main component on the miniscene object being selected (Activated from the player)
/// This class should be placed on the miniscene object that wraps up all other objects (oldest parent)
/// </summary>
public class MinisceneAutoProcess : MonoBehaviour
{
    [SerializeField]
    private GameObject mainComponentBlock;

    void OnEnable()
    {
        mainComponentBlock.SetActive(true);
    }

    public void TriggerEndAction()
    {
        mainComponentBlock.SetActive(false);
        gameObject.SetActive(false);
        MiniscenePlayer.MinisceneEnded.Invoke();
    }
}
}