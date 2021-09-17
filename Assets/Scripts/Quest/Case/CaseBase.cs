using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CaseBase
{
    /// <summary>
    /// Имя для обращения (уникальное)
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Название для отображения
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// Изображение, иллюстрация по-умолчанию
    /// </summary>
    public Sprite Picture;

    /// <summary>
    /// Словарь целей
    /// </summary>
    public Dictionary<string, string> Goals { get; set; }

    /// <summary>
    /// Имя текущей цели
    /// </summary>
    public string CurrentGoalKey { get; set; }

    /// <summary>
    /// Текущая цель
    /// </summary>
    public string CurrentGoal { get { return Goals[CurrentGoalKey] != null ? Goals[CurrentGoalKey] : "-"; } }

    /// <summary>
    /// Состояние дела
    /// </summary>
    public CaseState State { get; set; } = CaseState.NotAssigned;

    /// <summary>
    /// Состояние дела в строковим виде (для отображения)
    /// </summary>
    public string StateStr
    {
        get
        {
            switch (State)
            {
                case CaseState.NotAssigned: return "Не начато";
                case CaseState.Opened: return "Открыто";
                case CaseState.Denied: return "Отклонено";
                case CaseState.Failed: return "Провалено";
                case CaseState.Closed: return "Закрыто";
                default: return "Не начато";
            }
        }
    }

    /// <summary>
    /// Этапы
    /// </summary>
    public List<CaseStage> Stages { get; set; } = new List<CaseStage>();

    /// <summary>
    /// Текущий этап
    /// </summary>
    public CaseStage CurrentStage { get; set; }

    /// <summary>
    /// Полный ход дела до текущего этапа
    /// </summary>
    public List<CaseStage> PassedStages { get; set; }

    /// <summary>
    /// Полный текст до текущего этапа
    /// </summary>
    public List<string> FullText { get; set; }

    /// <summary>
    /// Полный текст до текущего этапа одной строкой
    /// </summary>
    public string FullTextAsString { get { return string.Join($"{Environment.NewLine}{Environment.NewLine}", FullText); } }

    //закомментировано, потому что пока решено сделать приоритет в StageBase для каждого этапа
    //public bool Priority { get; set; }

    /// <summary>
    /// Факты
    /// </summary>
    public List<CaseFact> Facts { get; set; } = new List<CaseFact>();

    /// <summary>
    /// Предположения
    /// </summary>
    public List<CaseGues> Gueses { get; set; } = new List<CaseGues>();

    /// <summary>
    /// Показания
    /// </summary>
    public List<CaseTestimony> Testimonies { get; set; } = new List<CaseTestimony>();

    /// <summary>
    /// Пользовательские заметки
    /// </summary>
    public List<CaseUserNote> UserNotes { get; set; } = new List<CaseUserNote>();

    /// <summary>
    /// Вопросы
    /// </summary>
    public List<CaseQuestion> Questions { get; set; } = new List<CaseQuestion>();

    /// <summary>
    /// Главный вопрос
    /// </summary>
    public CaseQuestion MainQuestion { get { return Questions.FirstOrDefault(x => x.IsMain); } }

    /// <summary>
    /// Дата-время открытия дела
    /// </summary>
    public Calendar OpeningDate { get; set; }

    /// <summary>
    /// Дата-время закрыия дела
    /// </summary>
    public Calendar ClosingDate { get; set; }

    /// <summary>
    /// Дополнительные флаги (например, для определения доступных действий и т.п.)
    /// </summary>
    public Dictionary<string, string> Flags { get; set; } = new Dictionary<string, string>();

    public CaseBase()
    {
        Name = "";
        Label = "";
        //Priority = 1;
        Picture = null;
        State = CaseState.NotAssigned;
        Stages = new List<CaseStage>();
        Goals = new Dictionary<string, string>();
        Facts = new List<CaseFact>();
        Gueses = new List<CaseGues>();
        Testimonies = new List<CaseTestimony>();
        UserNotes = new List<CaseUserNote>();
        PassedStages = new List<CaseStage>();
        FullText = new List<string>();
        CurrentStage = null;

        SetStages();
        SetGoals();
        SetFacts();
        SetGueses();
        SetTestimonies();
        SetQuestions();
    }

    /// <summary>
    /// Переход на этап по Id этапа
    /// </summary>
    public virtual void GoStage(int stageId)
    {
        //TODO: какая-то обработка ошибки, если отсутствует этап с таким Id?
        //TODO: выполнение AdditionalScript. здесь или в QuestManager?
        CurrentStage = Stages.FirstOrDefault(x => x.Id == stageId);
        PassedStages.Add(CurrentStage);
        //FullText.Add(CurrentStage.Text);
    }

    /// <summary>
    /// Установка родительского этапа для действий
    /// </summary>
    public virtual void SetActionsParent(StageBase caseStage)
    {
        for (int i = 0; i < caseStage.Actions.Count; i++)
        {
            caseStage.Actions[i].ParentType = StageType.caseStage;
            caseStage.Actions[i].ParentName = Name;
        }
    }

    /// <summary>
    /// Первоначальная установка всех этапов
    /// </summary>
    public abstract void SetStages();

    /// <summary>
    /// Первоначальная установка всех задач
    /// </summary>
    public abstract void SetGoals();

    /// <summary>
    /// Первоначальная установка всех фактов
    /// </summary>
    public abstract void SetFacts();

    /// <summary>
    /// Первоначальная установка всех предположений
    /// </summary>
    public abstract void SetGueses();

    /// <summary>
    /// Первоначальная установка всех показаний
    /// </summary>
    public abstract void SetTestimonies();

    /// <summary>
    /// Первоначальная установка всех вопросов
    /// </summary>
    public abstract void SetQuestions();
}
