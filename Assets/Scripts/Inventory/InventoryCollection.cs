using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryCollection
{
    /// <summary>
    /// Коллекция инвентаря
    /// </summary>
    public List<InventoryItem> Items { get; private set; } = new List<InventoryItem>();

    /// <summary>
    /// Макс. кол-во предметов (0 - бесконечно)
    /// </summary>
    public int MaxCount { get; set; } = 0;

    /// <summary>
    /// Текущее кол-во предметов
    /// </summary>
    public int Count { get { return Items.Count; } }

    /// <summary>
    /// Коллекция лицензий
    /// </summary>
    public List<PlayerLicense> Licenses { get; private set; } = new List<PlayerLicense>();

    /// <summary>
    /// Добавить предмет. Возвращает код: "" - успех, "max" - превышение макс.кол-ва, StartsWith("err") - исключение.
    /// При добавлении предметов с одинаковыми именами либо игнорируется, либо обновляется максимальное кол-во использований предмета
    /// </summary>
    public string AddItem(InventoryItem item)
    {
        try
        {
            if (MaxCount == 0)
            {
                Items.Add(item);
                return "";
            }
            else
            {
                if (Count <= MaxCount)
                {
                    var itemAlredyAdded = GetItemByName(item.Name);
                    if (itemAlredyAdded != null)
                    {
                        if (itemAlredyAdded.MaxNumberOfUses != 0)
                        {
                            itemAlredyAdded.MaxNumberOfUses += item.MaxNumberOfUses;
                            return "";
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        Items.Add(item);
                        return "";
                    }
                }
                else
                {
                    return "max";
                }
            }
        }
        catch (Exception ex)
        {
            return $"err: {ex.Message}";
        }
    }

    /// <summary>
    /// Удалить предмет. Возвращает код: "" - успех, "evid" - это вещдок, StartsWith("err") - исключение.
    /// Если ignoreEvidence = true, вещдок удалится, если false - не удалится
    /// </summary>
    public string RemoveItem(InventoryItem item, bool ignoreEvidence = true)
    {
        try
        {
            if (HasItem(item))
            {
                if (item is EvidenceItem)
                {
                    if (ignoreEvidence)
                    {
                        Items.Remove(Items.FirstOrDefault(x => x.Name == item.Name));
                        return "";
                    }
                    else
                    {
                        return "evid";
                    }
                }
                else
                {
                    Items.Remove(Items.FirstOrDefault(x => x.Name == item.Name));
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            return $"err: {ex.Message}";
        }
    }

    /// <summary>
    /// Проверить, есть ли такой педмет (проверка по имени)
    /// </summary>
    public bool HasItem(InventoryItem item)
    {
        return Items.Any(x => x.Name == item.Name);
    }

    /// <summary>
    /// Получить предмет по имени. Если не найден, возвращает null
    /// </summary>
    public InventoryItem GetItemByName(string name)
    {
        return Items.FirstOrDefault(x => x.Name == name);
    }

    /// <summary>
    /// Добавить лицензию
    /// </summary>
    public void AddLicense(PlayerLicense lic)
    {
        Licenses.Add(lic);
    }

    /// <summary>
    /// Удалить лицензию
    /// </summary>
    public void RemoveLicense(PlayerLicense lic)
    {
        var licToRemove = Licenses.FirstOrDefault(x => x.Name == lic.Name);
        if (licToRemove != null)
            Licenses.Remove(licToRemove);
    }

    /// <summary>
    /// Получить лицензию по имени. Если не найдена, возвращает null
    /// </summary>
    public PlayerLicense GetLicenseByName(string name)
    {
        return Licenses.FirstOrDefault(x => x.Name == name);
    }
}
