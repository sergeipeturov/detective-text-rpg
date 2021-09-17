using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Игровой лог
/// </summary>
public class GameLog
{
    public delegate void LogPopupNotifier(string popupText);
    /// <summary>
    /// Сообщает, что нужно показать попуп
    /// </summary>
    public static event LogPopupNotifier ShowPopup;

    private static GameLog instance;

    private GameLog()
    { }

    private static GameLog getInstance()
    {
        if (instance == null)
        {
            instance = new GameLog();
            instance.tmpLogText = GameObject.Find("Panels").transform.Find("p_playerInfo").transform.Find("p_log").transform.Find("scroll_log").transform.Find("Viewport").transform.Find("Content").GetComponentInChildren<TextMeshProUGUI>();
            instance.autoscroll = GameObject.Find("Panels").transform.Find("p_playerInfo").transform.Find("p_log").GetComponentInChildren<Autoscroll>();
        }
        return instance;
    }

    /// <summary>
    /// Добавить запись в лог
    /// </summary>
    /// <param name="logText">Текст для лога</param>
    /// <param name="showPopup">Показать при этом попуп</param>
    /// <param name="popupText">Текст для попупа (по умолчанию тот же, что для лога)</param>
    public static void Log(string logText, bool showPopup = false, string popupText = "")
    {
        getInstance().tmpLogText.text += $"{Environment.NewLine}{Environment.NewLine}[{getInstance().gameDate.ToShortString()}]: {logText}";
        instance.autoscroll.ScrollbarToBottom();
        if (showPopup)
        {
            if (popupText == "")
                ShowPopup?.Invoke(logText);
            else
                ShowPopup?.Invoke(popupText);
        }
    }

    /// <summary>
    /// Установить связь с календарем, чтобы в лог писалась дата сообщения
    /// </summary>
    public static void SetGameDate(Calendar calendar)
    {
        getInstance().gameDate = calendar;
    }

    private TextMeshProUGUI tmpLogText;
    private Autoscroll autoscroll;
    private Calendar gameDate;
}
