using UnityEngine;

/// <summary>
/// This is an example script that should be replaced when options and more format settings are introduced to the DialogueChainProcessor.
/// For now, this works to be used by a Unity Button in the Dialogue / Message box canvas to "press" next in the processor.
/// </summary>
public class NextButtonExample : MonoBehaviour
{
    [SerializeField]
    DialogueMessageSystem.DialogueChainProcessor dialogueChainProcessor;

    // Example with Monobehaviour
    public void OnNext()
    {
        if (dialogueChainProcessor == null)
        {
            return;
        }

        dialogueChainProcessor.LoadNextDialogue();
    }
}