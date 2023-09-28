using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Object for storing stats info to be displayed in the debug menu
/// </summary>
public abstract class StatTracker : MonoBehaviour
{
    /// <summary>
    /// Returns the name of the stat shown in the debug menu
    /// </summary>
    /// <returns></returns>
    public abstract string GetStatName();

    /// <summary>
    /// Returns the visual element associated with the stat
    /// </summary>
    /// <returns></returns>
    public abstract TemplateContainer GetStatVisual();

    /// <summary>
    /// Updates the stat visual for this tracker
    /// </summary>
    public abstract void UpdateStat();
}
