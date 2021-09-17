using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    /// Полное имя
    /// </summary>
    public string FullName { get { return $"{Name} {Surname}"; } }

    /// <summary>
    /// Мужчина или женщина
    /// </summary>
    public bool IsMale { get; set; }

    /// <summary>
    /// Показатели состояния
    /// </summary>
    public PlayerStats Stats { get; set; }

    /// <summary>
    /// Характер
    /// </summary>
    public PlayerPersonality Personality { get; set; }

    /// <summary>
    /// Репутация
    /// </summary>
    public PlayerReputation Reputation { get; set; }

    /// <summary>
    /// Черты
    /// </summary>
    public List<PlayerFeature> Features { get; set; }

    /// <summary>
    /// Инвентарь
    /// </summary>
    public InventoryCollection Inventory { get; set; }

    /// <summary>
    /// Экипированный предмет
    /// </summary>
    public InventoryItem Equipment { get; set; } = null;

    /// <summary>
    /// Хранилище
    /// </summary>
    public InventoryCollection Storage { get; set; }

    /// <summary>
    /// Деньги
    /// </summary>
    public PlayerMoney Money { get; set; }

    public Player()
    {
        Name = ""; Surname = ""; IsMale = true;
        Stats = new PlayerStats();
        Personality = new PlayerPersonality();
        Reputation = new PlayerReputation();
        Features = new List<PlayerFeature>();
        Inventory = new InventoryCollection();
        Inventory.MaxCount = 25;
        Storage = new InventoryCollection();
        Money = new PlayerMoney();
    }

    public Player(string name, string surname, bool isMale = true)
    {
        Name = name; Surname = surname; IsMale = isMale;
        Stats = new PlayerStats();
        Personality = new PlayerPersonality();
        Reputation = new PlayerReputation();
        Features = new List<PlayerFeature>();
        Inventory = new InventoryCollection();
        Inventory.MaxCount = 25;
        Storage = new InventoryCollection();
        Money = new PlayerMoney();
    }

    /// <summary>
    /// Экипировать предмет
    /// </summary>
    public void EquipItem(InventoryItem item)
    {
        Equipment = item;
        GameLog.Log($"Экипирован предмет: {item.Label}");
    }

    /// <summary>
    /// Снять экипированный предмет
    /// </summary>
    public void TakeOffEquipment()
    {
        if (Equipment != null) GameLog.Log($"Снят предмет: {Equipment.Label}");
        Equipment = null;
    }
    
    /// <summary>
    /// Добавить черту
    /// </summary>
    public void AddFeature(PlayerFeature playerFeature)
    {
        if (!Features.Any(x => x.Name == playerFeature.Name))
        {
            Features.Add(playerFeature);
            GameLog.Log($"Новая черта: {playerFeature.Label}", true, $"Новая черта:{Environment.NewLine}{playerFeature.Label}");
        }        
    }

    /// <summary>
    /// Удалить черту
    /// </summary>
    public void RemoveFeature(PlayerFeature playerFeature)
    {
        var feature = Features.FirstOrDefault(x => x.Name == playerFeature.Name);
        if (feature != null)
        {
            Features.Remove(playerFeature);
            GameLog.Log($"Потеряна черта: {playerFeature.Label}", true, $"Потеряна черта:{Environment.NewLine}{playerFeature.Label}");
        }   
    }

    /// <summary>
    /// Удалить черту по имени
    /// </summary>
    public void RemoveFeature(string playerFeatureName)
    {
        var feature = Features.FirstOrDefault(x => x.Name == playerFeatureName);
        if (feature != null)
        {
            Features.Remove(feature);
            GameLog.Log($"Потеряна черта: {feature.Label}", true, $"Потеряна черта:{Environment.NewLine}{feature.Label}");
        }
    }
}
