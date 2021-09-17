using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LicensesWindowScript : MonoBehaviour
{
    public Toggle ToggleListElement;
    public GameObject ContentOfScrollView;
    public ToggleGroup ToggleGroupForInventoryList;
    public Image Picture;
    public TextMeshProUGUI Description;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    public void OnShow(InventoryCollection inventory)
    {
        playerInventory = inventory;
        allLics = playerInventory.Licenses;
        ListToScroll();
    }

    private void ClearScrollContent()
    {
        var scrollContentList = new List<GameObject>();
        for (int i = 0; i < ContentOfScrollView.transform.childCount; i++)
        {
            scrollContentList.Add(ContentOfScrollView.transform.GetChild(i).gameObject);
        }
        foreach (var item in scrollContentList)
        {
            Destroy(item);
        }
    }

    private void ListToScroll()
    {
        ClearScrollContent();
        int num = 0;
        foreach (var item in allLics)
        {            
            var listElement = Instantiate(ToggleListElement);
            listElement.onValueChanged.AddListener(delegate { ListElementValueChanged(listElement); });
            listElement.name = item.Name;
            listElement.GetComponentInChildren<Text>().text = item.Label;
            listElement.group = ToggleGroupForInventoryList;
            listElement.transform.SetParent(ContentOfScrollView.transform);

            //костыль чтобы выбирался первый элемент
            num++;
            if (num == 1)
            {
                listElement.isOn = true;
            }
        }
    }

    /// <summary>
    /// Происходит при выборе элемента в списке
    /// </summary>
    private void ListElementValueChanged(Toggle toggle)
    {
        if (toggle.isOn)
        {
            selectedLic = playerInventory.GetLicenseByName(toggle.name);
            if (selectedLic.Picture != null)
                Picture.sprite = selectedLic.Picture;
            Description.text = selectedLic.Description;
        }
    }

    /// <summary>
    /// Класс, отвечающий за инвентарь игрока
    /// </summary>
    private InventoryCollection playerInventory = null;

    /// <summary>
    /// Все лицензии игрока
    /// </summary>
    private List<PlayerLicense> allLics = new List<PlayerLicense>();

    /// <summary>
    /// Выбранная в списке лицензия
    /// </summary>
    private PlayerLicense selectedLic = null;
}
