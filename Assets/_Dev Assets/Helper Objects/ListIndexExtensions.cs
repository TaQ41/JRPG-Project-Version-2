using System;
using System.Collections.Generic;
using Sirenix.Utilities;

public static class ListIndexExtensions
{
    private const string EmptyOrNullValuesListMessage = "The values list that the index is a part of is null or empty!";

    /// <summary>
    /// Increment the index int in any given list, unless it spills over the values max, then return 0.
    /// </summary>
    /// <returns>The new index value.</returns>
    /// <exception cref="NullReferenceException"></exception>
    public static int IncrementIndex<T>(int index, List<T> values)
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

    /// <summary>
    /// Get the current item using an index of a given list.
    /// </summary>
    /// <returns>The item of the list's index.</returns>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public static T GetItem<T>(int index, List<T> values)
    {
        try
        {
            return values[index];
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException(EmptyOrNullValuesListMessage);
        }
    }
}