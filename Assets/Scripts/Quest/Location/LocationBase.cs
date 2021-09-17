using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LocationBase
{
    /// <summary>
    /// Имя для обращения (уникальное)
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Имя для отображения
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public Address Address { get; set; }

    /// <summary>
    /// Адрес с названием локации
    /// </summary>
    public string FullAddress { get { return $"{Label}, {Address.FullAddress}"; } }

    /// <summary>
    /// Этапы локации
    /// </summary>
    public List<LocationStage> Stages { get; set; } = new List<LocationStage>();

    /// <summary>
    /// Текущий этап
    /// </summary>
    public LocationStage CurrentStage { get; set; }

    public LocationBase()
    {
        Name = ""; Label = ""; Address = new Address(); Stages = new List<LocationStage>(); CurrentStage = null;
        SetStages();
    }

    /// <summary>
    /// Переход на этап по Id этапа
    /// </summary>
    public virtual void GoStage(int stageId)
    {
        //TODO: какая-то обработка ошибки, если отсутствует этап с таким Id?
        CurrentStage = Stages.FirstOrDefault(x => x.Id == stageId);
    }

    /// <summary>
    /// Установка родительского этапа для действий
    /// </summary>
    public virtual void SetActionsParent(StageBase locationStage)
    {
        for (int i = 0; i < locationStage.Actions.Count; i++)
        {
            locationStage.Actions[i].ParentType = StageType.locationStage;
            locationStage.Actions[i].ParentName = Name;
        }
    }

    /// <summary>
    /// Первоначальная установка всех этапов
    /// </summary>
    public abstract void SetStages();
}
