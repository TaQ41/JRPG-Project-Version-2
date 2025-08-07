using System;
using UnityEngine;

namespace GameEventData
{

/// <summary>
/// Used to store an event that waits for its activation cycle to be picked up and then, begins the event that has the same id link as this.
/// Note that this should only be used for random game events.
/// </summary>
[Serializable]
public struct GameEventCallingContext
{
    /// <summary>
    /// What event should be called when this is activated? Use the event id for this.
    /// </summary>
    public string EventId;

    /// <summary>
    /// On what cycle should this event occur?
    /// </summary>
    public int ActivationCycle;
}
}