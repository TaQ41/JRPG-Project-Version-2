using System;
using UnityEngine;

namespace DialogueMessageSystem
{

public class DialogueChainProcessor : MonoBehaviour
{
    [Sirenix.OdinInspector.ShowInInspector]
    public bool IsProcessing { get; private set; } = false;

    [SerializeField]
    private Canvas dialogueObjectCanvas;

    [SerializeField]
    private TMPro.TextMeshProUGUI dialogueMessageText;

    public DialogueContainer[] DialogueChain {get; protected set;}
    public int DialogueChainIndex {get; private set;}

    /// <summary>
    /// Begin a dialogue message chain, this will then start from the first item in the given array of containers.
    /// Note, dialogueChainIndex enumerates initially from -1, so the side effect in 'LoadNextDialogue' will not cause
    /// any negative effects.
    /// </summary>
    public void LoadInitial(DialogueContainer[] inputDialogueChain)
    {
        if (!dialogueObjectCanvas || !dialogueMessageText)
        {
            Debug.LogError("The dialogueObjectCanvas and dialogueMessageText has not been set!");
            return;
        }
        
        dialogueObjectCanvas.enabled = true;
        DialogueChain = inputDialogueChain;
        DialogueChainIndex = -1;
        IsProcessing = true;

        LoadNextDialogue();
    }

    /// <summary>
    /// Loads the next dialogue in the chain, this will load the dialogue in place of the last dialogue message.
    /// This function has the side effect, that the dialogueChainIndex will increment by 1 on call.
    /// </summary>
    /// <param name="prevFormat">The format of the last dialogeContainer, used to possibly skip the FormatMessage request.</param>
    public void LoadNextDialogue()
    {
        DialogueChainIndex += 1;

        DialogueContainer dialogue;

        try
        {
            dialogue = DialogueChain[DialogueChainIndex];
        }
        catch (IndexOutOfRangeException)
        {
            Cleanup();
            return;
        }

        DialogueContainer.MessageFormats prevFormat = DialogueContainer.MessageFormats.None;

        if (DialogueChainIndex != 0)
            prevFormat = DialogueChain[DialogueChainIndex - 1].MessageFormat;

        if (prevFormat.Equals(dialogue.MessageFormat) == false)
            dialogueMessageText = FormatTMPro(dialogue.MessageFormat);

        dialogueMessageText.text = dialogue.Message;
        LoadOptions(dialogue);
    }

    private void Cleanup()
    {
        IsProcessing = false;
        dialogueObjectCanvas.enabled = false;
    }

    private TMPro.TextMeshProUGUI FormatTMPro(DialogueContainer.MessageFormats format)
    {
        if (format.Equals(DialogueContainer.MessageFormats.None))
            return dialogueMessageText;

        TMPro.TextMeshProUGUI tmpro = new();
        // Set font to whatever

        switch (format)
        {
            case DialogueContainer.MessageFormats.Dialogue:
                tmpro.fontSize = 40;
            break;

            case DialogueContainer.MessageFormats.Warning:
                tmpro.fontSize = 40;
                tmpro.alignment = TMPro.TextAlignmentOptions.Center;
            break;
        }

        return tmpro;
    }

    private void LoadOptions(DialogueContainer dialogue)
    {
        if (dialogue.Options == null)
            return;

        foreach (GameObject option in dialogue.Options)
        {
            // calculate spacing
            // load
            // script enable
            // display
        }
    }
}
}