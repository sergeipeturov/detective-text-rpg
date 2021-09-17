using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    /// <summary>
    /// Имя для обращения (уникальное)
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Отображаемое имя
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Изображение
    /// </summary>
    public Sprite Picture;

    /// <summary>
    /// Можно ли использовать в любое время через окно инвенторя
    /// </summary>
    public bool IsCanBeUsed { get; set; }

    /// <summary>
    /// Максимальное кол-во использований (0 = бесконечно)
    /// </summary>
    public int MaxNumberOfUses { get; set; }
    /// <summary>
    /// Текущее кол-во использований
    /// </summary>
    public int NumberOfUses { get; set; }
    /// <summary>
    /// Осталось использований
    /// </summary>
    public int NumberOfUsesLeft { get { return MaxNumberOfUses - NumberOfUses; } }

    /// <summary>
    /// Можно ли экипировать в спец. слот
    /// </summary>
    public bool IsEquipment { get; set; }

    /// <summary>
    /// Модификаторы (действуют все время, пока предмет в инвенторе, но не в хранилище)
    /// </summary>
    public InventoryModificators Modificators { get; set; } = new InventoryModificators();

    public InventoryItem()
    { }

    public InventoryItem(string name, string labelname)
    {
        Name = name; Label = labelname;
        Description = "";
        Picture = null;
        IsCanBeUsed = false;
        MaxNumberOfUses = 1;
        NumberOfUses = 0;
        IsEquipment = false;
    }
}
