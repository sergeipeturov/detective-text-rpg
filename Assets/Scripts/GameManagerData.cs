using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerData
{
    /// <summary>
    /// Игрок
    /// </summary>
    public Player Player { get; set; }

    /// <summary>
    /// Управление делами
    /// </summary>
    public CasesManager Cases { get; set; }

    /// <summary>
    /// Управление локациями
    /// </summary>
    public LocationsManager Locations { get; set; } = new LocationsManager();

    /// <summary>
    /// Управление инвентарем
    /// </summary>
    //public InventoryManager Inventory { get; set; }

    /// <summary>
    /// Текущая дата и время
    /// </summary>
    public Calendar CurrentDate { get; set; }

    public GameManagerData(InventoryManager inventoryManager)
    {
        CurrentDate = new Calendar(Calendar.StartDate, new Clock(22, 17, 00));
        Cases = new CasesManager(CurrentDate);
        Player = new Player();
    }

    public GameManagerData(string playerName, string playerSurname, bool isPlayerMale = true)
    {
        //Inventory = inventoryManager;

        CurrentDate = new Calendar(Calendar.StartDate, new Clock(22, 17, 00));
        GameLog.SetGameDate(CurrentDate);
        Cases = new CasesManager(CurrentDate);
        Player = new Player(playerName, playerSurname, isPlayerMale);
        Player.Money.AddDollars(20);
    }
}
