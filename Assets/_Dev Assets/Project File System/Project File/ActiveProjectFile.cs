using ProjectFileSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveProjectFile", menuName = "Scriptable Objects/ActiveProjectFile")]
public class ActiveProjectFile : ScriptableObject
{
    public ProjectFileHeader Data;

    [Sirenix.OdinInspector.Button]
    public void ProjectFileSave()
    {
        ProjectFileJSON_Writer.SaveProjectFile(Data);
    }

    [Sirenix.OdinInspector.Button]
    public void ProjectFileEmpty()
    {
        Data = default;
    }

    [Sirenix.OdinInspector.Button]
    public void ProjectFileDelete(string projectGUID)
    {
        ProjectFileJSON_Writer.DeleteProjectFile(projectGUID);
    }

    [Sirenix.OdinInspector.Button]
    public void ProjectFileGet(string projectGUID)
    {
        Data = ProjectFileJSON_Reader.ProjectFileHeader_Get(projectGUID);
    }

    [Sirenix.OdinInspector.Button]
    public ProjectFile_BasicIndentityInfo[] ProjectFileGetAllIdentity()
    {
        return ProjectFileJSON_Reader.GetProjectFileIdentities();
    }
}