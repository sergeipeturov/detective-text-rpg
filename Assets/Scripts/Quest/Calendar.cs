using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calendar 
{
    public delegate void DayEndNotifier();
    /// <summary>
    /// Сообщает, что закончились сутки
    /// </summary>
    public event DayEndNotifier NotifyDayEnd;
    public delegate void TimeChangeNotifier(double diffSec);
    /// <summary>
    /// Сообщает, что прошло столько то времени в секундах
    /// </summary>
    public event TimeChangeNotifier NotifyTimeChange;

    /// <summary>
    /// Дата начала игры
    /// </summary>
    public static DateTime StartDate { get { return new DateTime(1942, 06, 01); } }

    /// <summary>
    /// Текущая дата
    /// </summary>
    public DateTime CurrentDate { get; set; }

    /// <summary>
    /// Текущее время
    /// </summary>
    public Clock CurrentTime { get; set; }

    public override string ToString()
    {
        return $"{CurrentDate.ToString("ddd, dd MMMM yyyy")}, {CurrentTime.ToString()}";
    }

    public string ToShortString(bool withTime = true)
    {
        if (withTime)
            return $"{CurrentDate.ToString("dd.MM.yyyy")}, {CurrentTime.ToString()}";
        else
            return $"{CurrentDate.ToString("dd.MM.yyyy")}";
    }

    public Calendar()
    {
        CurrentDate = StartDate;
        CurrentTime = new Clock();
        CurrentTime.NotifyDayEnd += NewDay;
        CurrentTime.NotifyTimeChange += TimeChanged;
        TimeChanged(0);
    }

    public Calendar(DateTime date, Clock time)
    {
        CurrentDate = date;
        CurrentTime = time;
        CurrentTime.NotifyDayEnd += NewDay;
        CurrentTime.NotifyTimeChange += TimeChanged;
        TimeChanged(0);
    }

    public Calendar(Calendar calendar)
    {
        CurrentDate = calendar.CurrentDate;
        CurrentTime = new Clock(calendar.CurrentTime);
    }

    /// <summary>
    /// Принимает от часов событие окончания суток и передает далее
    /// </summary>
    private void NewDay()
    {
        CurrentDate.AddDays(1);
        if (NotifyDayEnd != null) NotifyDayEnd.Invoke();
    }

    /// <summary>
    /// Принимает от часов событие что прошло столько то времени в секундах и передает далее
    /// </summary>
    private void TimeChanged(double diffSec)
    {
        if (NotifyTimeChange != null) NotifyTimeChange.Invoke(diffSec);
    }
}
