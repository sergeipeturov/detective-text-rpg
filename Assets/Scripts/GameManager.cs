using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void GameStartNotifier();
    /// <summary>
    /// Сообщает, что игра начата
    /// </summary>
    public event GameStartNotifier GameStarted;

    public GameManagerData Data { get; set; }

    public UIManager UIManager { get; set; }
    public InventoryManager InventoryManager { get; set; }
    public OtherWindowsManager OtherWindowsManager { get; set; }

    void Awake()
    {
        UIManager = GetComponentInChildren<UIManager>();
        UIManager.CloseAll();
        InventoryManager = GetComponentInChildren<InventoryManager>();
        InventoryManager.CloseAllWindows();
        OtherWindowsManager = GetComponentInChildren<OtherWindowsManager>();
        OtherWindowsManager.CloseAll();

        isStarting = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (isStarting)
        {
            StartGame();
            GameStarted?.Invoke();
        }
    }

    private void StartGame()
    {
        if (isNewGame)
        {
            //начало новой игры
            //if (haveName)
            //{
            //plName = UIManager.InputedText;
            //Data = new GameManagerData(plName, plSurname);
            Data = new GameManagerData("Детектив", "Моралес");
            SetChildManagersData();
            SetTestData();
            isStarting = false;
            //}

            //if (haveSurname && !haveName)
            //{
            //    plSurname = UIManager.InputedText;
            //    UIManager.ShowInputBox("Введите имя персонажа.");
            //    haveName = true;
            //}

            //if (!haveSurname)
            //{
            //    UIManager.ShowInputBox("Введите фамилию персонажа.");
            //    haveSurname = true;
            //}
        }
        else
        {
            //здесь будет загрузка сохраненной игры
        }
    }

    private void SetChildManagersData()
    {
        InventoryManager.PlayerInventory = Data.Player.Inventory;
        InventoryManager.PlayerStorage = Data.Player.Storage;
        InventoryManager.CasesForLog = Data.Cases.AllCasesList;
        OtherWindowsManager.PlayerFeatures = Data.Player.Features;
        OtherWindowsManager.Cases = Data.Cases.CasesList;
    }

    private void SetTestData()
    {
        InventoryManager.AddItemToPlayer(new InventoryItem("pistol", "Пистолет") { IsCanBeUsed = true, Description = "Не оружие, а пушка.", IsEquipment = true });
        InventoryManager.AddItemToPlayer(new InventoryItem("revolver", "Револьвер") { Description = "Шестизарядный, мощный." });
        InventoryManager.AddItemToPlayer(new EvidenceItem("glass", "Разбитые очки") { CaseName = CasesNames.trying, Description = "Выглядят непрезентабельно." });
        InventoryManager.AddItemToPlayer(new EvidenceItem("machete", "Мачете") { CaseName = CasesNames.trying, IsCanBeUsed = true, Description = "Не смсит, но убивает." });
        InventoryManager.AddLicenseToPlayer(new PlayerLicense("lic_pd", "Лицензия частного детектива") { Description = "Дает право везде сувать свой нос." });
        InventoryManager.AddLicenseToPlayer(new PlayerLicense("lic_driver", "Водительские права") { Description = "Куплены в переходе." });
        Data.Player.Storage.AddItem(new InventoryItem("nothing", "Ничего особенного") { Description = "Это ничего особенного. Как всегда." });
        Data.Player.AddFeature(new PlayerFeature("feat_pd", "Частный детектив") { Description = "Суешь свой нос куда не надо." });
        Data.Player.AddFeature(new PlayerFeature("feat_cool", "Крутой детектив") { Description = "Именно про тебя писал Чандлер." });
        Data.Cases.OpenCaseAndSetCurrent(CasesNames.trying);
        Data.Locations.OpenLocationAndSetCurrent(LocationsNames.morales_office);
    }

    private bool isNewGame = true;
    private bool isStarting;
    //private bool haveSurname = false;
    //private bool haveName = false;
    //private string plSurname = "";
    //private string plName = "";
}
