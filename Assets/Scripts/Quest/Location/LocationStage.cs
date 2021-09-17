using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationStage : StageBase
{
    /// <summary>
    /// Позволять ли добавлять к списку действий список действий текущего этапа локации
    /// </summary>
    public bool AllowCaseActions { get; set; }

    public LocationStage() : base()
    {
        AllowCaseActions = false;
    }

    public LocationStage(int id, string text) : base(id, text)
    {
        AllowCaseActions = false;
    }
}
