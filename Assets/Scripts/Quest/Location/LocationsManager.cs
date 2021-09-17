using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationsManager
{
    /// <summary>
    /// Все локации
    /// </summary>
    public List<LocationBase> AllLocationsList { get; private set; } = new List<LocationBase>()
    {
        new LocationMoralesOffice()
    };

    /// <summary>
    /// Локации, известные игроку
    /// </summary>
    public List<LocationBase> LocationsList { get; set; } = new List<LocationBase>();

    /// <summary>
    /// Текущая локация
    /// </summary>
    public LocationBase CurrentLocation { get; private set; } = null;

    /// <summary>
    /// Установить локацию как текущую
    /// </summary>
    /// <param name="locName">Имя локации</param>
    public void SetCurrentLocation(string locName)
    {
        if (AllLocationsList.Any(x => x.Name == locName))
        {
            CurrentLocation = AllLocationsList.FirstOrDefault(x => x.Name == locName);
            GameLog.Log($"Смена локации. Текущая: {CurrentLocation.Label}");
        }
        //нужна ли обработка случая, когда нет локации с таким именем?
    }

    /// <summary>
    /// Первое упоминание локации перед игроком (открытие новой локации, игрок узнает о новой локации)
    /// </summary>
    /// <param name="locName">Имя локации</param>
    public void OpenLocation(string locName)
    {
        var loc = AllLocationsList.FirstOrDefault(x => x.Name == locName);
        if (loc != null)
        {
            LocationsList.Add(loc);
            GameLog.Log($"Открыта новая локация: {loc.Label}", true, $"Открыта новая локация:{Environment.NewLine}{loc.Label}");
        }
    }

    /// <summary>
    /// Открыть локацию и установить текущей
    /// </summary>
    /// <param name="caseName">Имя дела</param>
    public void OpenLocationAndSetCurrent(string locName)
    {
        OpenLocation(locName);
        SetCurrentLocation(locName);
    }
}

public static class LocationsNames
{
    /// <summary>
    /// Мораллес детективс, офис
    /// </summary>
    public const string morales_office = "morales_office";
}
