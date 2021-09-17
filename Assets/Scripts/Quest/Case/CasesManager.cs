using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasesManager
{
    /// <summary>
    /// Для отслеживания дат
    /// </summary>
    public Calendar CurrentDate { get; set; }

    /// <summary>
    /// Все дела
    /// </summary>
    public List<CaseBase> AllCasesList { get; private set; } = new List<CaseBase>()
    {
        new CaseGardeners()
    };

    /// <summary>
    /// Дела, встреченные игроком
    /// </summary>
    public List<CaseBase> CasesList { get; set; } = new List<CaseBase>();

    /// <summary>
    /// Текущее дело
    /// </summary>
    public CaseBase CurrentCase { get; private set; } = null;

    public CasesManager(Calendar currentDate)
    {
        CurrentDate = currentDate;
    }

    /// <summary>
    /// Установить текущее дело
    /// </summary>
    /// <param name="caseName">Имя дела</param>
    public void SetCurrentCase(string caseName)
    {
        if (CasesList.Any(x => x.Name == caseName && x.State == CaseState.Opened))
        {
            CurrentCase = CasesList.FirstOrDefault(x => x.Name == caseName && x.State == CaseState.Opened);
            GameLog.Log($"Смена текущего дела: {CurrentCase.Label}", true, $"Смена текущего дела:{Environment.NewLine}{CurrentCase.Label}");
        }
        //нужна ли обработка случая, когда нет открытого дела с таким именем?
    }

    /// <summary>
    /// Пометить дело как открытое
    /// </summary>
    /// <param name="caseName">Имя дела</param>
    public void OpenCase(string caseName)
    {
        if (AllCasesList.Any(x => x.Name == caseName && x.State == CaseState.NotAssigned))
        {
            var openingCase = AllCasesList.FirstOrDefault(x => x.Name == caseName && x.State == CaseState.NotAssigned);
            openingCase.State = CaseState.Opened;
            openingCase.OpeningDate = new Calendar(CurrentDate);
            CasesList.Add(openingCase);
            GameLog.Log($"Открыто новое дело: {openingCase.Label}", true, $"Открыто новое дело:{Environment.NewLine}{openingCase.Label}");
        }
    }

    /// <summary>
    /// Пометить дело как открытое и установить текущим
    /// </summary>
    /// <param name="caseName">Имя дела</param>
    public void OpenCaseAndSetCurrent(string caseName)
    {
        OpenCase(caseName);
        SetCurrentCase(caseName);
    }   
}

public static class CasesNames
{
    /// <summary>
    /// Садовники умирают молодыми
    /// </summary>
    public const string trying = "trying";
}
