using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StorageWindowScript : MonoBehaviour
{
    /// <summary>
    /// Сообщает, что в окне хранилища с предметом инвентаря что-то сделали
    /// </summary>
    public event InventoryWindowEventNotifier StorageWindowEvent;
    public delegate void InventoryWindowEventNotifier(InventoryWindowEventArg e);

    public Toggle ToggleListElement;
    public GameObject ContentOfInventoryScrollView;
    public GameObject ContentOfStorageScrollView;
    public ToggleGroup ToggleGroupForInventoryList;
    public Toggle ToggleAllInteract;
    public Toggle ToggleOnlyInteract;
    public Toggle ToggleOnlyNonInteract;
    public Toggle ToggleAllEvidence;
    public Toggle ToggleOnlyEvidence;
    public Toggle ToggleOnlyNonEvidence;
    public Image Picture;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI CountText;

    // Start is called before the first frame update
    void Start()
    {
        ToggleAllInteract.onValueChanged.AddListener(ToggleAllInteractValueChanged);
        ToggleOnlyInteract.onValueChanged.AddListener(ToggleOnlyInteractValueChanged);
        ToggleOnlyNonInteract.onValueChanged.AddListener(ToggleOnlyNonInteractValueChanged);
        ToggleAllEvidence.onValueChanged.AddListener(ToggleAllEvidenceValueChanged);
        ToggleOnlyEvidence.onValueChanged.AddListener(ToggleOnlyEvidenceValueChanged);
        ToggleOnlyNonEvidence.onValueChanged.AddListener(ToggleOnlyNonEvidenceValueChanged);
        FilterList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    public void OnShow(InventoryCollection inventory, InventoryCollection storage)
    {
        playerInventory = inventory;
        allInventory = playerInventory.Items;
        playerStorage = storage;
        allStorage = playerStorage.Items;
        FilterList();
    }

    public void OnInventoryChanged()
    {
        FilterList();
    }

    public void OnToStorButtonClick()
    {
        var e = new InventoryWindowEventArg(selectedInventoryItem, InventoryWindowCommand.toStorage);
        StorageWindowEvent?.Invoke(e);
    }

    public void OnFromStorButtonClick()
    {
        var e = new InventoryWindowEventArg(selectedInventoryItem, InventoryWindowCommand.fromStorage);
        StorageWindowEvent?.Invoke(e);
    }

    private void ClearScrollContent()
    {
        //скролл хранилища
        var scrollContentList2 = new List<GameObject>();
        for (int i = 0; i < ContentOfStorageScrollView.transform.childCount; i++)
        {
            scrollContentList2.Add(ContentOfStorageScrollView.transform.GetChild(i).gameObject);
        }
        foreach (var item in scrollContentList2)
        {
            Debug.Log("Removing " + item.name);
            Destroy(item);
        }
        //скролл инвентаря
        var scrollContentList = new List<GameObject>();
        for (int i = 0; i < ContentOfInventoryScrollView.transform.childCount; i++)
        {
            scrollContentList.Add(ContentOfInventoryScrollView.transform.GetChild(i).gameObject);
        }
        foreach (var item in scrollContentList)
        {
            Destroy(item);
        }
    }

    private void CountToHeader()
    {
        if (playerInventory != null)
        {
            var res = $"Предметы{Environment.NewLine}{playerInventory.Count}/{playerInventory.MaxCount}";
            CountText.text = res;
        }
    }

    private void ListToScroll()
    {
        int num = 0;
        //скролл инвентаря
        foreach (var item in filteredInventory)
        {
            var listElement = Instantiate(ToggleListElement);
            listElement.onValueChanged.AddListener(delegate { ListElementValueChanged(listElement); });
            listElement.name = item.Name;
            listElement.GetComponentInChildren<Text>().text = item.Label;
            listElement.group = ToggleGroupForInventoryList;
            listElement.transform.SetParent(ContentOfInventoryScrollView.transform);

            //костыль чтобы выбирался первый элемент
            num++;
            if (num == 1)
            {
                listElement.isOn = true;
            }
        }
        //скролл хранилища
        foreach (var item in filteredStorage)
        {
            var listElement = Instantiate(ToggleListElement);
            listElement.onValueChanged.AddListener(delegate { ListElementValueChanged(listElement); });
            listElement.name = item.Name;
            listElement.GetComponentInChildren<Text>().text = item.Label;
            listElement.group = ToggleGroupForInventoryList;
            listElement.transform.SetParent(ContentOfStorageScrollView.transform);
        }
    }

    private void FilterList()
    {
        ClearScrollContent();
        //для инвентаря
        filteredInventory = new List<InventoryItem>();
        foreach (var item in allInventory)
        {
            if (tbInteractIndex == 0 && tbEvidenceIndex == 0)
            {
                filteredInventory.Add(item);
            }
            else if (tbInteractIndex == 0 && tbEvidenceIndex == 1)
            {
                if (item is EvidenceItem) filteredInventory.Add(item);
            }
            else if (tbInteractIndex == 0 && tbEvidenceIndex == 2)
            {
                if (!(item is EvidenceItem)) filteredInventory.Add(item);
            }
            else if (tbInteractIndex == 1 && tbEvidenceIndex == 0)
            {
                if (item.IsCanBeUsed) filteredInventory.Add(item);
            }
            else if (tbInteractIndex == 2 && tbEvidenceIndex == 0)
            {
                if (!item.IsCanBeUsed) filteredInventory.Add(item);
            }
            else if (tbInteractIndex == 1 && tbEvidenceIndex == 1)
            {
                if (item.IsCanBeUsed && (item is EvidenceItem)) filteredInventory.Add(item);
            }
            else if (tbInteractIndex == 1 && tbEvidenceIndex == 2)
            {
                if (item.IsCanBeUsed && (!(item is EvidenceItem))) filteredInventory.Add(item);
            }
            else if (tbInteractIndex == 2 && tbEvidenceIndex == 1)
            {
                if (!item.IsCanBeUsed && (item is EvidenceItem)) filteredInventory.Add(item);
            }
            else if (tbInteractIndex == 2 && tbEvidenceIndex == 2)
            {
                if (!item.IsCanBeUsed && (!(item is EvidenceItem))) filteredInventory.Add(item);
            }
        }
        //для хранилища
        filteredStorage = new List<InventoryItem>();
        foreach (var item in allStorage)
        {
            if (tbInteractIndex == 0 && tbEvidenceIndex == 0)
            {
                filteredStorage.Add(item);
            }
            else if (tbInteractIndex == 0 && tbEvidenceIndex == 1)
            {
                if (item is EvidenceItem) filteredStorage.Add(item);
            }
            else if (tbInteractIndex == 0 && tbEvidenceIndex == 2)
            {
                if (!(item is EvidenceItem)) filteredStorage.Add(item);
            }
            else if (tbInteractIndex == 1 && tbEvidenceIndex == 0)
            {
                if (item.IsCanBeUsed) filteredStorage.Add(item);
            }
            else if (tbInteractIndex == 2 && tbEvidenceIndex == 0)
            {
                if (!item.IsCanBeUsed) filteredStorage.Add(item);
            }
            else if (tbInteractIndex == 1 && tbEvidenceIndex == 1)
            {
                if (item.IsCanBeUsed && (item is EvidenceItem)) filteredStorage.Add(item);
            }
            else if (tbInteractIndex == 1 && tbEvidenceIndex == 2)
            {
                if (item.IsCanBeUsed && (!(item is EvidenceItem))) filteredStorage.Add(item);
            }
            else if (tbInteractIndex == 2 && tbEvidenceIndex == 1)
            {
                if (!item.IsCanBeUsed && (item is EvidenceItem)) filteredStorage.Add(item);
            }
            else if (tbInteractIndex == 2 && tbEvidenceIndex == 2)
            {
                if (!item.IsCanBeUsed && (!(item is EvidenceItem))) filteredStorage.Add(item);
            }
        }
        ListToScroll();
        CountToHeader();
    }

    private void ToggleAllInteractValueChanged(bool isOn)
    {
        if (isOn) { tbInteractIndex = 0; FilterList(); }
    }

    private void ToggleOnlyInteractValueChanged(bool isOn)
    {
        if (isOn) { tbInteractIndex = 1; FilterList(); }
    }

    private void ToggleOnlyNonInteractValueChanged(bool isOn)
    {
        if (isOn) { tbInteractIndex = 2; FilterList(); }
    }

    private void ToggleAllEvidenceValueChanged(bool isOn)
    {
        if (isOn) { tbEvidenceIndex = 0; FilterList(); }
    }

    private void ToggleOnlyEvidenceValueChanged(bool isOn)
    {
        if (isOn) { tbEvidenceIndex = 1; FilterList(); }
    }

    private void ToggleOnlyNonEvidenceValueChanged(bool isOn)
    {
        if (isOn) { tbEvidenceIndex = 2; FilterList(); }
    }

    /// <summary>
    /// Происходит при выборе элемента в списке инвентаря
    /// </summary>
    private void ListElementValueChanged(Toggle toggle)
    {
        if (toggle.isOn)
        {
            selectedInventoryItem = playerInventory.GetItemByName(toggle.name);
            if (selectedInventoryItem == null)
                selectedInventoryItem = playerStorage.GetItemByName(toggle.name);
            if (selectedInventoryItem.Picture != null)
                Picture.sprite = selectedInventoryItem.Picture;
            Description.text = GetDescriptionForSelectedInventoryItem();
        }
    }

    /// <summary>
    /// Генерирует описание для выбранного предмета
    /// </summary>
    private string GetDescriptionForSelectedInventoryItem()
    {
        string res = $"{selectedInventoryItem.Description}{Environment.NewLine}{Environment.NewLine}";
        if (selectedInventoryItem.IsCanBeUsed)
        {
            if (selectedInventoryItem.IsEquipment) res += $"Можно экипировать.";
            else res += $"Можно использовать.";
        }
        //TODO: добавить выдачу модификаторов в зависимости от настроек игры
        return res;
    }

    /// <summary>
    /// Список со всеми элементами инвентаря
    /// </summary>
    private List<InventoryItem> allInventory = new List<InventoryItem>();

    /// <summary>
    /// Список со всеми элементами хранилища
    /// </summary>
    private List<InventoryItem> allStorage = new List<InventoryItem>();

    /// <summary>
    /// Список предметов инвентаря, отфильтрованный
    /// </summary>
    private List<InventoryItem> filteredInventory = new List<InventoryItem>();

    /// <summary>
    /// Список предметов хранилища, отфильтрованный
    /// </summary>
    private List<InventoryItem> filteredStorage = new List<InventoryItem>();

    /// <summary>
    /// Класс, отвечающий за инвентарь игрока
    /// </summary>
    private InventoryCollection playerInventory = null;

    /// <summary>
    /// Класс, отвечающий за хранилище игрока
    /// </summary>
    private InventoryCollection playerStorage = null;

    /// <summary>
    /// Выбранный в списке предмет инвентаря
    /// </summary>
    private InventoryItem selectedInventoryItem = null;

    private int tbInteractIndex = 0;
    private int tbEvidenceIndex = 0;
}
