using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArchiveWindowScript : MonoBehaviour
{
    public GameManager GameManager; //это конечно совсем не правильно, но так проще и быстрей, чем что-то придумывать
    public Toggle ToggleListElement;
    public GameObject ContentOfScrollView;
    public ToggleGroup ToggleGroupForInventoryList;
    public Image Picture;
    public TextMeshProUGUI CaseText;
    public Button SetCurrentButton;
    public TextMeshProUGUI CaseState;

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

    public void OnShow(List<CaseBase> cases)
    {
        casesList = cases;
        ListToScroll();
    }

    public void SetSelectedCaseAsCurrent()
    {
        if (selectedCase != null)
            GameManager.Data.Cases.SetCurrentCase(selectedCase.Name);
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
        foreach (var item in casesList)
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
            selectedCase = casesList.FirstOrDefault(x => x.Name == toggle.name);
            if (selectedCase.Picture != null)
                Picture.sprite = selectedCase.Picture;
            CaseText.text = selectedCase.FullTextAsString;
            string closingDate = selectedCase.State == global::CaseState.Closed || selectedCase.State == global::CaseState.Denied || selectedCase.State == global::CaseState.Failed ? $"{Environment.NewLine}Дата закрытия: {selectedCase.ClosingDate.ToShortString()}." : "";
            string currentGoal = selectedCase.State == global::CaseState.Closed || selectedCase.State == global::CaseState.Denied || selectedCase.State == global::CaseState.Failed ? "" : $"{Environment.NewLine}!: {selectedCase.CurrentGoal}";
            CaseState.text = $"Статус: {selectedCase.StateStr}.{Environment.NewLine}Дата открытия: {selectedCase.OpeningDate.ToShortString()}.{closingDate}{Environment.NewLine}{currentGoal}";

            bool isSelectedCaseCurrent = GameManager.Data.Cases.CurrentCase.Name == selectedCase.Name;
            SetCurrentButton.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = isSelectedCaseCurrent ? "Текущее" : "Сделать текущим";
            SetCurrentButton.enabled = !isSelectedCaseCurrent;
            //SetCurrentButton.interactable = !isSelectedCaseCurrent;
        }
    }

    /// <summary>
    /// Все дела
    /// </summary>
    private List<CaseBase> casesList = new List<CaseBase>();

    /// <summary>
    /// Выбранное дело
    /// </summary>
    private CaseBase selectedCase = null;
}
