using System;
using UnityEngine;

namespace ProjectFileSystem
{

[Serializable]
/// <summary>
/// 
/// </summary>
public class ProjectFileHeader
{
    [SerializeField]
    private string m_projectGUID = Guid.NewGuid().ToString();
    public string ProjectGUID { get {return m_projectGUID;} }

    public string ProjectName;
}
}