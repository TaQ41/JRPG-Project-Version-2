namespace ProjectFileSetup
{

/// <summary>
/// Pass an activeProjectFile asset and check to see if it is null or not.
/// </summary>
public static class ValidateActiveProjectFile
{
    public static bool Invoke(ActiveProjectFile activeProjectFile)
    {
        if (activeProjectFile == null)
        {
            UnityEngine.Debug.LogError("Set the active project file on this component!");
            return false;
        }

        return true;
    }
}
}

//
// Paste this snippet into components of the CreateNewGameComponents namespace:
// -
// if (!ValidateActiveProjectFile.Invoke(activeProjectFile)) return;
// -