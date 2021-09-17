using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    /// <summary>
    /// Здоровье
    /// </summary>
    public PlayerStat Health { get; private set; } = new PlayerStat("В ударе", "Здоровый", "Потрепанный", "При смерти", 0);

    /// <summary>
    /// Энергия
    /// </summary>
    public PlayerStat Energy { get; private set; } = new PlayerStat("Бодрый", "Свежий", "Уставший", "Никакой", 2/7200);

    /// <summary>
    /// Психика
    /// </summary>
    public PlayerStat Moral { get; private set; } = new PlayerStat("Ясный ум", "В порядке", "В стрессе", "На грани", 2/7200);

    /// <summary>
    /// Трезвость
    /// </summary>
    public PlayerStat Alcohol { get; private set; } = new PlayerStat("Трезвый", "Выпивший", "Бухой", "В дрова", -2/3600);

}
