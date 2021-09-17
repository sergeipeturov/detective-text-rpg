using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLicense
{
    /// <summary>
    /// Имя для обращения (уникальное)
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Имя для отображения
    /// </summary>
    public string Label { get; set; } = "";

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    /// Изображение
    /// </summary>
    public Sprite Picture;

    public PlayerLicense()
    { }

    public PlayerLicense(string name, string label)
    { 
        Name = name; Label = label; 
    }
}
