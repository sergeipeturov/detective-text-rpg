using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReputation
{
    /// <summary>
    /// Неподкупность
    /// </summary>
    public int Corruption { get; private set; }

    /// <summary>
    /// Законность
    /// </summary>
    public int Lawyerity { get; private set; }

    /// <summary>
    /// Отображаемая неподкупность
    /// </summary>
    public string CorruptionToString
    {
        get
        {
            if (Corruption > -5 && Corruption < 5)
                return "Сомнительный";
            else if (Corruption >= 5)
                return "Неподкупный";
            else
                return "Продажный";
        }
    }

    /// <summary>
    /// Отображаемое соблюдение законов
    /// </summary>
    public string LawyerityToString
    {
        get
        {
            if (Lawyerity > -5 && Lawyerity < 5)
                return "Эгоист";
            else if (Lawyerity >= 5)
                return "Законник";
            else
                return "Мерзавец";
        }
    }

    public override string ToString()
    {
        return $"{CorruptionToString} {LawyerityToString}";
    }
}
