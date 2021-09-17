using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    /// <summary>
    /// Сообщает, что нужно экипировать (actionIndex = 0) или снять (actionIndex = 1) предмет
    /// </summary>
    public event EquipItemNotifier NotifyEquipItem;
    public delegate void EquipItemNotifier(InventoryItem item, int actionIndex);

    public GameObject InventoryWindow;
    public GameObject LicencesWindow;
    public GameObject StorageWindow;

    public InventoryCollection PlayerInventory { get; set; } = null;
    public InventoryCollection PlayerStorage { get; set; } = null;
    public List<CaseBase> CasesForLog { get; set; } = new List<CaseBase>();

    // Start is called before the first frame update
    void Start()
    {
        inventoryWindowScript = InventoryWindow.GetComponent<InventoryWindowScript>();
        inventoryWindowScript.InventoryWindowEvent += OnInventoryWindowCommand;
        storageWindowScript = StorageWindow.GetComponent<StorageWindowScript>();
        storageWindowScript.StorageWindowEvent += OnInventoryWindowCommand;
        licencesWindowScript = LicencesWindow.GetComponent<LicensesWindowScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLicensesWindow()
    {
        licencesWindowScript.OnShow(PlayerInventory);
        LicencesWindow.SetActive(true);
    }

    public void ShowInventoryWindow()
    {
        inventoryWindowScript.OnShow(PlayerInventory);
        InventoryWindow.SetActive(true);
    }

    public void ShowStorageWindow()
    {
        storageWindowScript.OnShow(PlayerInventory, PlayerStorage);
        StorageWindow.SetActive(true);
    }

    public void CloseAllWindows()
    {
        CloseLicencesWindow();
        CloseInventoryWindow();
        CloseStorageWindow();
    }

    public void AddItemToPlayer(InventoryItem inventoryItem)
    {
        if (PlayerInventory.Count < PlayerInventory.MaxCount)
        {
            var res = PlayerInventory?.AddItem(inventoryItem);
            if (res == "")
            {
                if (inventoryItem is EvidenceItem)
                    GameLog.Log($"Получен вещдок: {inventoryItem.Label} (дело: '{CasesForLog.FirstOrDefault(x => x.Name == (inventoryItem as EvidenceItem).CaseName)?.Label}')", true, 
                        $"Получен вещдок:{Environment.NewLine}{inventoryItem.Label}{Environment.NewLine}(дело: '{CasesForLog.FirstOrDefault(x => x.Name == (inventoryItem as EvidenceItem).CaseName)?.Label}')");
                else
                    GameLog.Log($"Получен предмет: {inventoryItem.Label}", true, $"Получен предмет:{Environment.NewLine}{inventoryItem.Label}");
            }
        }
        else
        {
            //TODO: здесь выдавать мессадж бокс с предупреждением, что в инвентаре не хватает места, а затем окно инвентаря с предлождением что-то выбросить
        }
    }

    public void AddLicenseToPlayer(PlayerLicense playerLicense)
    {
        PlayerInventory.AddLicense(playerLicense);
        GameLog.Log($"Получена лицензия: {playerLicense.Label}", true, $"Получена лицензия:{Environment.NewLine}{playerLicense.Label}");
    }

    private void CloseLicencesWindow()
    {
        LicencesWindow.SetActive(false);
    }

    private void CloseInventoryWindow()
    {
        InventoryWindow.SetActive(false);
    }

    private void CloseStorageWindow()
    {
        StorageWindow.SetActive(false);
    }
    
    private void OnInventoryWindowCommand(InventoryWindowEventArg e)
    {
        if (e.InventoryItem != null)
        {
            if (e.Command == InventoryWindowCommand.use)
            {
                if (e.InventoryItem.IsCanBeUsed)
                {
                    if (e.InventoryItem.IsEquipment)
                    {
                        NotifyEquipItem?.Invoke(e.InventoryItem, 0);
                    }
                }
                else
                {
                    //TODO: вызывать событие использования предмета
                    GameLog.Log($"Использован предмет: {e.InventoryItem.Label}");
                }
            }
            else if (e.Command == InventoryWindowCommand.throws)
            {
                var res = PlayerInventory?.RemoveItem(e.InventoryItem, false);
                if (res == "")
                {
                    GameLog.Log($"Выброшен предмет: {e.InventoryItem.Label}");
                    NotifyEquipItem?.Invoke(e.InventoryItem, 1);
                }
                else if (res == "evid")
                {
                    //TODO: здесь вызов бокса с вопросом, правда ли вы хотите удалить вещдок
                    if (PlayerInventory?.RemoveItem(e.InventoryItem) == "")
                    {
                        GameLog.Log($"Выброшен вещдок: {e.InventoryItem.Label}");
                        NotifyEquipItem?.Invoke(e.InventoryItem, 1);
                    }
                }
                inventoryWindowScript.OnInventoryChanged();
            }
            else if (e.Command == InventoryWindowCommand.toStorage)
            {
                PlayerStorage?.AddItem(e.InventoryItem);
                PlayerInventory?.RemoveItem(e.InventoryItem);
                GameLog.Log($"Предмет сдан в хранилище: {e.InventoryItem.Label}");
                NotifyEquipItem?.Invoke(e.InventoryItem, 1);
                storageWindowScript.OnInventoryChanged();
            }
            else if (e.Command == InventoryWindowCommand.fromStorage)
            {
                if (PlayerInventory.Count < PlayerInventory.MaxCount)
                {
                    PlayerInventory?.AddItem(e.InventoryItem);
                    PlayerStorage?.RemoveItem(e.InventoryItem);
                    GameLog.Log($"Предмет взият из хранилища: {e.InventoryItem.Label}");
                    storageWindowScript.OnInventoryChanged();
                }
                //TODO: здесь выдавать мессадж бокс с предупреждением, что в инвентаре не хватает места
            }
        }
    }

    private InventoryWindowScript inventoryWindowScript;
    private LicensesWindowScript licencesWindowScript;
    private StorageWindowScript storageWindowScript;
}

public class InventoryWindowEventArg
{
    /// <summary>
    /// Предмет инвентаря
    /// </summary>
    public InventoryItem InventoryItem { get; set; } = null;

    /// <summary>
    /// Команда что сделать с предметом инвентаря
    /// </summary>
    public InventoryWindowCommand Command { get; set; } = InventoryWindowCommand.none;

    public InventoryWindowEventArg() { }
    public InventoryWindowEventArg(InventoryItem item, InventoryWindowCommand command)
    {
        InventoryItem = item; Command = command;
    }
}

public enum InventoryWindowCommand
{
    none = 0,
    use,
    throws,
    toStorage,
    fromStorage
}
