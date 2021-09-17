using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseUserNote : Bordable
{
    /// <summary>
    /// Автоматически назначаемый уникальный в рамках дела Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Текст
    /// </summary>
    public string Text { get; set; }

    public CaseUserNote()
    {
        Id = 0; Text = "";
    }

    public CaseUserNote(int id, string text)
    {
        Id = id; Text = text;
    }
}
