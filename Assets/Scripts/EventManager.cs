using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameManager GameManager;
    public InputManager InputManager;
    public TakeOffEquipmentButtonScript TakeOffEquipmentButton;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.ClickEvent += OnClickEvent;
        TakeOffEquipmentButton.NotifyTakeOffEquipment += OnTakeOffEquipment;
        GameManager.InventoryManager.NotifyEquipItem += OnEquipItem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Обрабатывает щелчок мыши по различным объектам (не кнопкам и не UI-элементам)
    /// </summary>
    private void OnClickEvent(ClickEventArgs e)
    {
        if (e.Sender.name == "safe_1")
        {
            GameManager.InventoryManager.ShowInventoryWindow();
            //GameManager.InventoryManager.ShowStorageWindow();
        }
        if (e.Sender.name == "lics")
        {
            GameManager.InventoryManager.ShowLicensesWindow();
        }
        if (e.Sender.name == "archive_1")
        {
            GameManager.OtherWindowsManager.ShowArchiveWindow();
        }
    }

    private void OnEquipItem(InventoryItem item, int action)
    {
        if (action == 0)
        {
            GameManager?.Data?.Player?.EquipItem(item);
        }
        else if (action == 1)
        {
            if (GameManager?.Data?.Player?.Equipment != null && GameManager?.Data?.Player?.Equipment.Name == item.Name)
            {
                GameManager?.Data?.Player?.TakeOffEquipment();
            }
        }
    }

    /// <summary>
    /// Обрабатывает событие снятия экипированного предмета
    /// </summary>
    private void OnTakeOffEquipment()
    {
        GameManager?.Data?.Player?.TakeOffEquipment();
    }
}

public class ClickEventArgs
{
    /// <summary>
    /// Объект, по которому кликнули
    /// </summary>
    public GameObject Sender { get; set; } = null;

    public ClickEventArgs() { }
    public ClickEventArgs(GameObject sender) { Sender = sender; }
}
