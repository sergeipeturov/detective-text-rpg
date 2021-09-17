using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Менеджер по работе со всеми окнами, кроме окон инвенторя (за них отвечает InventoryManager)
/// </summary>
public class OtherWindowsManager : MonoBehaviour
{
    public GameObject FeaturesWindow;
    public GameObject ArchiveWindow;

    /// <summary>
    /// Список черт игрока для отображения в окне черт
    /// </summary>
    public List<PlayerFeature> PlayerFeatures { get; set; }

    /// <summary>
    /// Список дел для отображения в окне архива
    /// </summary>
    public List<CaseBase> Cases { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        featuresWindowScript = FeaturesWindow.GetComponent<FeaturesWindowScript>();
        archiveWindowScript = ArchiveWindow.GetComponent<ArchiveWindowScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowFeaturesWindow()
    {
        featuresWindowScript.OnShow(PlayerFeatures);
        FeaturesWindow.SetActive(true);
    }

    public void ShowArchiveWindow()
    {
        archiveWindowScript.OnShow(Cases);
        ArchiveWindow.SetActive(true);
    }

    public void CloseAll()
    {
        FeaturesWindow.SetActive(false);
        ArchiveWindow.SetActive(false);
    }

    private FeaturesWindowScript featuresWindowScript;
    private ArchiveWindowScript archiveWindowScript;
}
