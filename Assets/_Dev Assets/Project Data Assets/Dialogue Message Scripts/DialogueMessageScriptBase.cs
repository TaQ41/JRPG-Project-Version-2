namespace DialogueMessageScriptData
{

/// <summary>
/// The base for scripts of conversations that can have options in the game.
/// </summary>
public abstract class DialogueMessageScriptBase
{
    public abstract DialogueMessageSystem.DialogueContainer[] DialogueContainers { get; }
}
}