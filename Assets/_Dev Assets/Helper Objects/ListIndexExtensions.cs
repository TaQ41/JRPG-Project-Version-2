using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

public static class ListIndexExtensions
{
    private const string EmptyOrNullValuesListMessage = "The values list that the index is a part of is null or empty!";

    /// <summary>
    /// Increment the index int in any given list, unless it spills over the values max, then return 0.
    /// </summary>
    /// <returns>The new index value.</returns>
    /// <exception cref="NullReferenceException"></exception>
    public static int IncrementIndex<T>(this List<T> values, int index)
    {
        try
        {
            return ++index >= values.Count ? 0 : index;
        }
        catch
        {
            throw new NullReferenceException(EmptyOrNullValuesListMessage);
        }
    }
}