using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CaseState : int
{
    /// <summary>
    /// Не начато
    /// </summary>
    NotAssigned = 0,
    /// <summary>
    /// Открыто
    /// </summary>
    Opened,
    /// <summary>
    /// Отклонено
    /// </summary>
    Denied,
    /// <summary>
    /// Провалено
    /// </summary>
    Failed,
    /// <summary>
    /// Закрыто
    /// </summary>
    Closed
}
