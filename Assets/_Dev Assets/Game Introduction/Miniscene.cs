using System;
using UnityEngine;

namespace MinisceneSystem
{

[Serializable]
/// <summary>
/// An object for holding data to load and control actions on the miniscene.
/// </summary>
public struct Miniscene
{
    [SerializeField]
    private bool m_isSkippable;
    public readonly bool IsSkippable { get { return m_isSkippable;} }

    [SerializeField]
    private GameObject m_minisceneObj;
    public readonly GameObject MinisceneObj { get { return m_minisceneObj;} }
}
}