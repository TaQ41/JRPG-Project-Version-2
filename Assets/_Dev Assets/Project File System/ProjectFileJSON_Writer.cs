using UnityEngine;
using System;
using System.IO;

namespace ProjectFileSystem
{

[Serializable]
/// <summary>
/// Contains methdos for modifying the saved project files. May save to file or delete from file.
/// </summary>
public static class ProjectFileJSON_Writer
{
    /// <summary>
    /// Used for the delete function, delete the meta file then the json file. This is the extension name for meta files in Unity.
    /// </summary>
    const string ExtensionMeta = ".meta";

    /// <summary>
    /// Save the given project file to the saved project files folder. Use the PFHeader from the 'ActiveProjectFile' from 'Data'
    /// </summary>
    /// <returns>A bool indicating if the action was successful or not.</returns>
    public static bool SaveProjectFile(ProjectFileHeader projectFileHeader)
    {
        if (projectFileHeader == null)
        {
            Debug.LogError("The current projectFileHeader is null!");
            return false;
        }

        string fileContent = JsonUtility.ToJson(projectFileHeader, prettyPrint: false);
        string filePath = ProjectFileJSON_Reader.ProjectFilePath_Construct(projectFileHeader.ProjectGUID);
        File.WriteAllText(filePath, fileContent);
        return true;
    }

    /// <summary>
    /// Delete a project file saved in the 'saved project files folder' by a given projectGUID.
    /// </summary>
    /// <returns>A bool indicating if the action was successful or not.</returns>
    public static bool DeleteProjectFile(string projectGUID)
    {
        string filePath = ProjectFileJSON_Reader.ProjectFilePath_Construct(projectGUID);
        string metaFilePath = ProjectFileJSON_Reader.ProjectFilePath_Construct(projectGUID) + ExtensionMeta;

        if (File.Exists(filePath) == false || File.Exists(metaFilePath) == false)
        {
            Debug.Log("Attempted to delete a non-existent file, make sure the name of the projectGUID is correct. : " + projectGUID);
            return false;
        }

        File.Delete(metaFilePath);
        File.Delete(filePath);
        return true;
    }
}
}