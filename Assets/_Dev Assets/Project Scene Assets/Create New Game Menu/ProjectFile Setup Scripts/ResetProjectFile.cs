using UnityEngine;

namespace ProjectFileSetup
{

/// <summary>
/// Reset the project file in the activeProjectFile on awake.
/// </summary>
public class ResetProjectFile : MonoBehaviour
{
    [SerializeField]
    private ActiveProjectFile activeProjectFile;

    void Awake()
    {
        if (!ValidateActiveProjectFile.Invoke(activeProjectFile)) return;
        activeProjectFile.Data = new();
    }
}
}