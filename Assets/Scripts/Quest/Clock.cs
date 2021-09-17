using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock 
{
    public delegate void DayEndNotifier();
    /// <summary>
    /// Сообщает, что прошли сутки
    /// </summary>
    public event DayEndNotifier NotifyDayEnd;
    public delegate void TimeChangeNotifier(double timediffinseconds);
    /// <summary>
    /// Сообщает, что прошло столько то времени в секундах
    /// </summary>
    public event TimeChangeNotifier NotifyTimeChange;

    /// <summary>
    /// Часы
    /// </summary>
    public int Hours { get { return hours; } }
    private int hours;
    
    /// <summary>
    /// Минуты
    /// </summary>
    public int Minutes { get { return minutes; } }
    private int minutes;
    
    /// <summary>
    /// Секунды
    /// </summary>
    public int Seconds { get { return seconds; } }
    private int seconds;

    /// <summary>
    /// Является ли это время отдыхом
    /// </summary>
    public bool IsRest { get; set; }

    public override string ToString()
    {
        return Hours.ToString() + ":" + Minutes.ToString();
    }

    public string ToFullString()
    {
        return Hours.ToString() + ":" + Minutes.ToString() + ":" + Seconds.ToString();
    }

    public Clock()
    {
        hours = 22;
        minutes = 17;
        seconds = 0;
        IsRest = false;
    }

    public Clock(int h, int m, int s, bool isRest = false)
    {
        hours = h;
        minutes = m;
        seconds = s;
        IsRest = isRest;
    }

    public Clock(Clock clock)
    {
        hours = clock.Hours;
        minutes = clock.Minutes;
        seconds = clock.Seconds;
        IsRest = clock.IsRest;
    }

    /// <summary>
    /// Добавить секунды
    /// </summary>
    public void AddSeconds(int val, bool isRest = false)
    {
        IsRest = isRest;
        seconds += val;
        double diffSec = val;
        int diffMinutes = 0;
        while (seconds >= 60)
        {
            diffMinutes += 1;
            seconds -= 60;
        }
        AddMinutes(diffMinutes);
        if (NotifyTimeChange != null) NotifyTimeChange.Invoke(diffSec);
    }

    /// <summary>
    /// Добавить минуты
    /// </summary>
    public void AddMinutes(int val, bool isRest = false)
    {
        IsRest = isRest;
        minutes += val;
        double diffSec = val * 60;
        int diffHours = 0;
        while (minutes >= 60)
        {
            diffHours += 1;
            minutes -= 60;
        }
        AddHours(diffHours);
        if (NotifyTimeChange != null) NotifyTimeChange.Invoke(diffSec);
    }

    /// <summary>
    /// Добавить часы
    /// </summary>
    public void AddHours(int val, bool isRest = false)
    {
        IsRest = isRest;
        hours += val;
        double diffSec = val * 3600;
        int diffDays = 0;
        if (hours >= 24)
        {
            diffDays += 1;
            hours -= 24;
        }
        if (NotifyTimeChange != null) NotifyTimeChange.Invoke(diffSec);
        if (NotifyDayEnd != null) NotifyDayEnd.Invoke();
    }

    /// <summary>
    /// Добавить время
    /// </summary>
    public void AddTime(Clock clock)
    {
        AddHours(clock.Hours, clock.IsRest);
        AddMinutes(clock.Minutes, clock.IsRest);
        AddSeconds(clock.Seconds, clock.IsRest);
    }
}
