using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    /// <summary>
    /// Максимальное значение по умолчанию
    /// </summary>
    public double Default { get; set; }

    /// <summary>
    /// Текущее значение
    /// </summary>
    public double Current { get; set; }

    /// <summary>
    /// Максимальное значение (по-умолчанию + бафы - дебафы)
    /// </summary>
    public double Max { get; set; }

    /// <summary>
    /// Верхняя граница, при превышении которой состояние будет считаться наилучшим
    /// </summary>
    public double UpperLimit { get; set; }

    /// <summary>
    /// Граница, при превышении которой состояние будет считаться хорошим
    /// </summary>
    public double MiddleLimit { get; set; }

    /// <summary>
    /// Граница, при превышении которой состояние будет считаться плохим, а при принижении - наихудшим
    /// </summary>
    public double BottomLimit { get; set; }

    /// <summary>
    /// Строка для обозначения наилучшего состояния
    /// </summary>
    public string Better { get; set; } = "";
    /// <summary>
    /// Строка для обозначения хорошего состояния
    /// </summary>
    public string Good { get; set; } = "";
    /// <summary>
    /// Строка для обозначения плохого состояния
    /// </summary>
    public string Bad { get; set; } = "";
    /// <summary>
    /// Строка для обозначения наихудшего состояния
    /// </summary>
    public string Worst { get; set; } = "";

    /// <summary>
    /// Изменение в секунду игрового времени
    /// </summary>
    public double ChangePerSec { get; set; }

    /// <summary>
    /// Текущее состояние (цифра от 0 - наилучшее до 3 - наихудшее)
    /// </summary>
    public int CurrentState
    {
        get
        {
            if (Current > UpperLimit)
                return 0;
            else if (Current <= UpperLimit && Current > MiddleLimit)
                return 1;
            else if (Current <= MiddleLimit && Current > BottomLimit)
                return 2;
            else
                return 3;
        }
    }

    /// <summary>
    /// Текущее состояние (строка)
    /// </summary>
    public string CurrentStateStr
    {
        get
        {
            switch (CurrentState)
            {
                case 0:
                    return Better;
                case 1:
                    return Good;
                case 2:
                    return Bad;
                case 3:
                    return Worst;
                default:
                    return Worst;
            }
        }
    }

    public override string ToString()
    {
        return $"{CurrentStateStr} {Environment.NewLine}({Current.ToString("N0")})";
    }

    public PlayerStat()
    {
        Default = 100;
        Max = 100;
        Current = 100;
        UpperLimit = 95;
        MiddleLimit = 75;
        BottomLimit = 25;
        ChangePerSec = 0;
    }

    public PlayerStat(string better, string good, string bad, string worst, double changePerSec)
    {
        Default = 100;
        Max = 100;
        Current = 100;
        UpperLimit = 95;
        MiddleLimit = 75;
        BottomLimit = 25;
        Better = better; Good = good; Bad = bad; Worst = worst;
        ChangePerSec = changePerSec;
    }
}
