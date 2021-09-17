using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StageBase
{
    public delegate void AdditionalScript();

    /// <summary>
    /// ID этапа
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Иллюстрация
    /// </summary>
    public Sprite Picture;

    /// <summary>
    /// Текст уже показывался
    /// </summary>
    public bool IsTextAlreadyShown { get; set; }

    /// <summary>
    /// Текст повторяемый
    /// </summary>
    public bool IsTextRepeatable { get; set; }

    /// <summary>
    /// Приоритет (0 - дело становится текущим, когда выполняется условие перехода на этот этап. 1 - дело не перехватывает на себя управление, пока этого не сделает игрок)
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Позволяет ли сменить текущее дело
    /// </summary>
    public bool AllowChangeCase { get; set; }

    /// <summary>
    /// Позволяет ли открыть карту и сменить текущую локацию
    /// </summary>
    public bool AllowChangeLocation { get; set; }

    /// <summary>
    /// Текст текущего этапа.
    /// Для этапов дела доступен код [LocText], который заменится на текст локации
    /// </summary>
    public string Text { get { return GetText(); } set { text = value; } }
    private string text;

    /// <summary>
    /// Действия
    /// </summary>
    public List<QuestAction> Actions { get; set; } = new List<QuestAction>();

    /// <summary>
    /// Доп. скрипт, который происходит на этапе (минус к здоровью, запуск мини-игры и т.п.)
    /// </summary>
    public AdditionalScript Script { get; set; }

    public StageBase()
    {
        Id = -1;
        Picture = null;
        Text = "";
        IsTextRepeatable = false;
        IsTextAlreadyShown = false;
        Actions = new List<QuestAction>();
        Priority = 1;
        AllowChangeCase = true;
        AllowChangeLocation = true;
    }

    public StageBase(int id, string text)
    {
        Id = id;
        Picture = null;
        Text = text;
        IsTextRepeatable = false;
        IsTextAlreadyShown = false;
        Actions = new List<QuestAction>();
        Priority = 1;
        AllowChangeCase = true;
        AllowChangeLocation = true;
    }

    private string GetText()
    {
        if (IsTextAlreadyShown)
        {
            if (IsTextRepeatable)
                return text;
            else
                return "";
        }
        else
            return text;
    }
}

public enum StageType
{
    caseStage = 0,
    locationStage
}