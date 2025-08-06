using System;
using System.Collections.Generic;

/// <summary>
/// Allows double nested lists to be serialized natively by wrapping the secondary list in a class declaration.
/// </summary>
/// <typeparam name="T">Any serializable type</typeparam>
[Serializable]
public class ListWrapper<T>
{
    public List<T> Values = new();
    public T this[int index]
    {
        get
        {
            return Values[index];
        }
        set
        {
            Values[index] = value;
        }
    }
}