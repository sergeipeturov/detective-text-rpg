using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CaseElementBase : Bordable
{
    /// <summary>
    /// Имя для обращения (уникальное в рамках дела)
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Отображаемый текст
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Известен
    /// </summary>
    public bool IsKnown { get; set; }

    /// <summary>
    /// Секретный
    /// </summary>
    public bool IsSecret { get; set; }

    /// <summary>
    /// Нуждается в доказательстве
    /// </summary>
    public bool IsNeedToBeProven { get; set; }

    //TODO: придумать систему доказательств

    public CaseElementBase()
    {
        Name = ""; Text = ""; IsKnown = false; IsSecret = false; IsNeedToBeProven = true;
    }

    public CaseElementBase(string name, string text, bool isSecret = false)
    {
        Name = name; Text = text; IsKnown = false; IsSecret = isSecret; IsNeedToBeProven = true;
    }
}
