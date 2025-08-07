using UnityEngine;

namespace DialogueMessageSystem
{

/// <summary>
/// The structure that provides a concise area for messages to relay necessary details.
/// </summary>
public struct DialogueContainer
{
    public enum MessageFormats
    {
        None = 0,
        Dialogue = 1,
        Warning = 2,
    }

    public string Message;
    public MessageFormats MessageFormat;
    public GameObject[] Options;
    public bool IsSkippable;
}
}