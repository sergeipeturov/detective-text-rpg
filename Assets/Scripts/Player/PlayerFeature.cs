using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeature
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
    /// Описание
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Изображение
    /// </summary>
    public Sprite Picture { get; set; }

    /// <summary>
    /// Модификаторы
    /// </summary>
    public InventoryModificators Modificators { get; set; }

    public PlayerFeature()
    {
        Name = "none";
        Label = "";
        Description = "";
        Modificators = new InventoryModificators();
    }

    public PlayerFeature(string name, string label)
    {
        Name = name;
        Label = label;
        Description = "";
        Modificators = new InventoryModificators();
    }
}
