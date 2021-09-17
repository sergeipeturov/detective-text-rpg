using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseTestimony : CaseElementBase
{
    /// <summary>
    /// Имя персонажа, который дал показание
    /// </summary>
    public string CharacterName { get; set; }
    
    /// <summary>
    /// Текст в формате Имя: Показание
    /// </summary>
    public string TextWithCharacterName { get { return CharacterName + ": " + Text; } }

    public CaseTestimony() : base()
    {
        CharacterName = "";
    }

    public CaseTestimony(string name, string text, string chatacterName, bool isSecret = false) : base(name, text, isSecret)
    {
        CharacterName = chatacterName;
    }
}
