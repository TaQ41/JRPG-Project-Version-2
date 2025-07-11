using UnityEngine;
using System.IO;

namespace ProjectFileSystem
{

/// <summary>
/// Read project files inside the saved project files folder. Return the project file headers or a collection of their identifying info.
/// </summary>
public static class ProjectFileJSON_Reader
{
    const string ExtensionJson = ".json";
    public static string SavedProjectFilesFolderPath
    {
        get
        {
            string path = @"Assets\_Dev Assets\Project File System\Saved Project Files";
            if (Directory.Exists(path) == false)
            {
                Debug.Log($"path ({path}) does not exist.. Now creating folder 'Saved Project Files' at {path}.");
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }

    /// <summary>
    /// Gets the path of a JSON doc by the project file's name, its ProjectGUID.
    /// </summary>
    public static string ProjectFilePath_Construct(string projectGUID)
    {
        return $@"{SavedProjectFilesFolderPath}\{projectGUID}{ExtensionJson}";
    }

    /// <summary>
    /// Get the ProjectFileHeader by a project file's path.
    /// </summary>
    private static ProjectFileHeader ProjectFileHeaderFromPath(string path)
    {
        return JsonUtility.FromJson<ProjectFileHeader>(File.ReadAllText(path));
    }

    /// <summary>
    /// Get the projectFileHeader class by providing its 'ProjectGUID' field.
    /// </summary>
    /// <returns>Null on failure.</returns>
    public static ProjectFileHeader ProjectFileHeader_Get(string projectGUID)
    {
        string path = ProjectFilePath_Construct(projectGUID);

        if (File.Exists(path) == false)
        {
            Debug.LogError($"projectFile ({path}) does not exist. Please make sure the file was saved correctly and/or that the projectGUID is verbatim.");
            return null;
        }

        return ProjectFileHeaderFromPath(path);
    }

    /// <summary>
    /// Get the basic identity information of every saved project file. Meta files are skipped.
    /// </summary>
    /// <returns>Empty signifies no file has been saved yet.</returns>
    public static ProjectFile_BasicIndentityInfo[] GetProjectFileIdentities()
    {
        System.Collections.Generic.List<ProjectFile_BasicIndentityInfo> projectFileIdentityInfos = new();

        foreach (string path in Directory.GetFiles(SavedProjectFilesFolderPath))
        {
            if (Path.GetExtension(path) != ExtensionJson)
            {
                continue;
            }

            ProjectFile_BasicIndentityInfo identityInfo = new();
            ProjectFileHeader header = ProjectFileHeaderFromPath(path);

            identityInfo.ProjectGUID = header.ProjectGUID;
            projectFileIdentityInfos.Add(identityInfo);
        }

        return projectFileIdentityInfos.ToArray();
    }
}
}