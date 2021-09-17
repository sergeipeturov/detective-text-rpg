using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseQuestion : CaseElementBase
{
    /// <summary>
    /// Является ли главным вопросом дела
    /// </summary>
    public bool IsMain { get; set; }

    public CaseQuestion() : base()
    {
        IsMain = false;
    }

    public CaseQuestion(string name, string text, bool isMain = false) : base(name, text, false)
    {
        IsMain = isMain;
    }
}
