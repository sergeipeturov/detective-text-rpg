using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject InputBox;
    public GameObject MessageBox;
    public GameObject StatusBar;
    public GameObject PlayerInfo;
    public GameObject EquipmentBar;
    public TextMeshProUGUI CurrentCaseTMP;
    public TextMeshProUGUI DateAndLocation;
    public TextMeshProUGUI Popup;

    private const float maxPopupTime = 1.5f;

    /// <summary>
    /// Текст, который был введен в последний InputBox
    /// </summary>
    public string InputedText { get; private set; } = "";

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        sbPlayerIcon = StatusBar.transform.Find("Player Icon").gameObject;
        sbHealth = StatusBar.transform.Find("Health").gameObject;
        sbEnergy = StatusBar.transform.Find("Energy").gameObject;
        sbMoral = StatusBar.transform.Find("Moral").gameObject;
        sbAlcohol = StatusBar.transform.Find("Alcohol").gameObject;
        sbMoney = StatusBar.transform.Find("Money").gameObject;
        piEquipment = EquipmentBar.transform.Find("Equipment").gameObject;
        piPersonality = PlayerInfo.transform.Find("tmp_personality").gameObject.GetComponent<TextMeshProUGUI>();
        piReputation = PlayerInfo.transform.Find("tmp_reputation").gameObject.GetComponent<TextMeshProUGUI>();
        GameLog.ShowPopup += OnShowPopup;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPopupShowing)
        {
            if (popupQueue.Count > 0)
            {
                Popup.text = popupQueue[0];
                Popup.gameObject.SetActive(true);
            }
            else
            {
                isPopupShowing = false;
                ClosePopup();
            }
            currentPopupTime += Time.deltaTime;
            if (currentPopupTime > maxPopupTime)
            {
                ClosePopup();
                popupQueue.RemoveAt(0);
            }
        }
    }

    void FixedUpdate()
    {
        if (gm != null && gm.Data != null && gm.Data.Player != null)
        {
            sbPlayerIcon.GetComponentInChildren<TextMeshProUGUI>().text = gm.Data.Player.FullName;
            sbHealth.GetComponentInChildren<TextMeshProUGUI>().text = gm.Data.Player.Stats.Health.ToString();
            sbEnergy.GetComponentInChildren<TextMeshProUGUI>().text = gm.Data.Player.Stats.Energy.ToString();
            sbMoral.GetComponentInChildren<TextMeshProUGUI>().text = gm.Data.Player.Stats.Moral.ToString();
            sbAlcohol.GetComponentInChildren<TextMeshProUGUI>().text = gm.Data.Player.Stats.Alcohol.ToString();
            sbMoney.GetComponentInChildren<TextMeshProUGUI>().text = gm.Data.Player.Money.ToString();
            if (gm.Data.Player.Equipment == null)
                piEquipment.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "-";
            else
                piEquipment.GetComponentsInChildren<TextMeshProUGUI>()[1].text = gm.Data.Player.Equipment.Label;
            piPersonality.GetComponentInChildren<TextMeshProUGUI>().text = gm.Data.Player.Personality.ToString();
            piReputation.GetComponentInChildren<TextMeshProUGUI>().text = gm.Data.Player.Reputation.ToString();

            if (gm.Data.Cases.CurrentCase == null)
                CurrentCaseTMP.text = $"Дело:{Environment.NewLine}-";
            else
                CurrentCaseTMP.text = $"Дело:{Environment.NewLine}{gm.Data.Cases.CurrentCase.Label}{Environment.NewLine}{Environment.NewLine}!: {gm.Data.Cases.CurrentCase.CurrentGoal}";

            if (gm.Data.CurrentDate != null && gm.Data.Locations.CurrentLocation != null)
                DateAndLocation.text = $"{gm.Data.CurrentDate.ToString()}{Environment.NewLine}{gm.Data.Locations.CurrentLocation.FullAddress}";
            else
                DateAndLocation.text = "-";
        }
    }

    public void ShowInputBox(string inputText)
    {
        var txt = InputBox.transform.Find("Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>();
        var txt2 = InputBox.transform.Find("InputField (TMP)").gameObject.GetComponent<TMP_InputField>();
        txt.text = inputText;
        txt2.text = "";
        InputBox.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseInputBox()
    {
        var txt = InputBox.transform.Find("InputField (TMP)").gameObject.GetComponent<TMP_InputField>();
        InputedText = txt.text;
        InputBox.SetActive(false);
        Time.timeScale = 1;
    }

    public void ShowMessageBox(string messageText)
    {
        var txt = MessageBox.transform.Find("Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>();
        txt.text = messageText;
        MessageBox.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseMessageBox()
    {
        MessageBox.SetActive(false);
        Time.timeScale = 1;
    }

    public void CloseAllBoxes()
    {
        InputBox.SetActive(false);
        MessageBox.SetActive(false);
    }

    public void ClosePopup()
    {
        Popup.gameObject.SetActive(false);
        currentPopupTime = 0.0f;
        //isPopupShowing = false;
    }

    public void OnPopupClick()
    {
        currentPopupTime = maxPopupTime;
    }

    public void CloseAll()
    {
        CloseAllBoxes();
        ClosePopup();
    }

    private void OnShowPopup(string popupText)
    {
        popupQueue.Add(popupText);
        //Popup.text = popupText;
        //Popup.gameObject.SetActive(true);
        isPopupShowing = true;
    }

    private GameObject sbPlayerIcon;
    private GameObject sbHealth;
    private GameObject sbEnergy;
    private GameObject sbMoral;
    private GameObject sbAlcohol;
    private GameObject sbMoney;
    private GameObject piEquipment;
    private TextMeshProUGUI piPersonality;
    private TextMeshProUGUI piReputation;
    private GameManager gm;

    private float currentPopupTime = 0.0f;
    private bool isPopupShowing = false;
    private List<string> popupQueue = new List<string>();
}
