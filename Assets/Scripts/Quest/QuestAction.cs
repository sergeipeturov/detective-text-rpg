using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAction
{
    public delegate bool Condition();

    /// <summary>
    /// ID действия
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Текст
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// ID этапа, на который ведет
    /// </summary>
    public int StageId { get; set; }

    /// <summary>
    /// Кто был породителем действия
    /// </summary>
    public StageType ParentType { get; set; }

    /// <summary>
    /// Имя породителя действия
    /// </summary>
    public string ParentName { get; set; }

    /// <summary>
    /// Действие уже было выбрано игроком
    /// </summary>
    public bool IsAlreadyPerformed { get; set; }

    /// <summary>
    /// Действие может быть повторено
    /// </summary>
    public bool IsRepeatable { get; set; }

    /// <summary>
    /// Сколько времени занимает выполнение события
    /// </summary>
    public Clock TimeToAdd { get; set; }

    /// <summary>
    /// Условие появления этого действия
    /// </summary>
    public Condition AdditionalCondition { get; set; }

    public QuestAction()
    {
        Id = -1;
        Text = "";
        StageId = -1;
        IsAlreadyPerformed = false;
        IsRepeatable = false;
        TimeToAdd = new Clock(0, 0, 0);
        AdditionalCondition = DefaultCondition;
    }

    public QuestAction(int id, string text, int stageId)
    {
        Id = id;
        Text = text;
        StageId = stageId;
        IsAlreadyPerformed = false;
        IsRepeatable = false;
        TimeToAdd = new Clock(0, 0, 0);
        AdditionalCondition = DefaultCondition;
    }

    public QuestAction(int id, string text, int stageId, Clock clock)
    {
        Id = id;
        Text = text;
        StageId = stageId;
        IsAlreadyPerformed = false;
        IsRepeatable = false;
        TimeToAdd = clock;
        AdditionalCondition = DefaultCondition;
    }

    public QuestAction(int id, string text, int stageId, bool isRepeatable)
    {
        Id = id;
        Text = text;
        StageId = stageId;
        IsAlreadyPerformed = false;
        IsRepeatable = isRepeatable;
        TimeToAdd = new Clock(0, 0, 0);
        AdditionalCondition = DefaultCondition;
    }

    public QuestAction(int id, string text, int stageId, bool isRepeatable, Clock clock)
    {
        Id = id;
        Text = text;
        StageId = stageId;
        IsAlreadyPerformed = false;
        IsRepeatable = isRepeatable;
        TimeToAdd = clock;
        AdditionalCondition = DefaultCondition;
    }

    private bool DefaultCondition()
    {
        return true;
    }
}
