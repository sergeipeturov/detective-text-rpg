using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceItem : InventoryItem
{
    /// <summary>
    /// Имя дела, для которого является вещдоком
    /// </summary>
    public string CaseName { get; set; }

    /// <summary>
    /// Заблокировано ли для использования на доске (когда используется не по назначению)
    /// </summary>
    public bool BlockedForBoard { get; set; } = false;

    public EvidenceItem() : base()
    { }

    public EvidenceItem(string name, string labelName) : base(name, labelName)
    { }
}
