using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPersonality
{
    /// <summary>
    /// Хладнокровность
    /// </summary>
    public int Cool { get; private set; }

    /// <summary>
    /// Принципиальность
    /// </summary>
    public int Integrity { get; private set; }

    /// <summary>
    /// Храбрость
    /// </summary>
    public int Bravity { get; private set; }

    /// <summary>
    /// Отображаемая Хладнокровность
    /// </summary>
    public string CoolToString
    {
        get
        {
            if (Cool > -5 && Cool < 5)
                return "Невозмутимый";
            else if (Cool >= 5)
                return "Хладнокровный";
            else
                return "Нервный";
        }
    }

    /// <summary>
    /// Отображаемая Принципиальность
    /// </summary>
    public string IntegrityToString
    {
        get
        {
            if (Integrity > -5 && Integrity < 5)
                return "Циничный";
            else if (Integrity >= 5)
                return "Благородный";
            else
                return "Беспринципный";
        }
    }

    /// <summary>
    /// Отображаемая Храбрость
    /// </summary>
    public string BravityToString
    {
        get
        {
            if (Bravity > -5 && Bravity < 5)
                return "Смельчак";
            else if (Bravity >= 5)
                return "Храбрец";
            else
                return "Червяк";
        }
    }

    public override string ToString()
    {
        return $"{CoolToString} {IntegrityToString} {BravityToString}";
    }
}
