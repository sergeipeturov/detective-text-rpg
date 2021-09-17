using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUIManager : MonoBehaviour
{
    public delegate void ActionClickEventNotifier(QuestAction action);
    /// <summary>
    /// Сообщает, что был произведен клик на какое-то действие
    /// </summary>
    public event ActionClickEventNotifier ActionClickEvent;

    public TextMeshProUGUI MainText;
    public TextMeshProUGUI ActionTextPrefab;
    public GameObject ContentOfActionScrollView;

    // Start is called before the first frame update
    void Start()
    {
        MainText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CurrentTextToScroll(string text)
    {
        if (text != "")
        {
            MainText.text += $"{text}{Environment.NewLine}{Environment.NewLine}";
        }
    }

    public void ActionListToScroll(List<QuestAction> actions)
    {
        actionList = actions;
        ClearScrollContent();
        foreach (var item in actionList)
        {
            var listElement = Instantiate(ActionTextPrefab);
            listElement.GetComponent<ActionTextScript>().Action = item;
            listElement.GetComponent<ActionTextScript>().SelfClickEvent += OnActionTextClick;
            listElement.text = item.Text;
            listElement.transform.SetParent(ContentOfActionScrollView.transform);
        }
    }

    private void OnActionTextClick(QuestAction action)
    {
        ActionClickEvent?.Invoke(action);
    }

    private void ClearScrollContent()
    {
        var scrollContentList = new List<GameObject>();
        for (int i = 0; i < ContentOfActionScrollView.transform.childCount; i++)
        {
            scrollContentList.Add(ContentOfActionScrollView.transform.GetChild(i).gameObject);
        }
        foreach (var item in scrollContentList)
        {
            Destroy(item);
        }
    }

    /// <summary>
    /// Текущий список действий
    /// </summary>
    private List<QuestAction> actionList = new List<QuestAction>();
}
