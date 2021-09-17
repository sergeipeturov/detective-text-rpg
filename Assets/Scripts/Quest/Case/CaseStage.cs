using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseStage : StageBase
{
    /// <summary>
    /// Позволять ли добавлять к списку действий список действий текущего этапа локации
    /// </summary>
    public bool AllowLocationActions { get; set; }

    public CaseStage() : base()
    {
        AllowLocationActions = false;
    }

    public CaseStage(int id, string text) : base(id, text)
    {
        AllowLocationActions = false;
    }
}
