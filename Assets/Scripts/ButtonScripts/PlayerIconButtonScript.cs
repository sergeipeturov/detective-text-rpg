using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIconButtonScript : MonoBehaviour
{
    public GameObject PanelPlayerInfo;
    public Autoscroll LogAutoscrollScript;
    bool panelState = false;

    // Start is called before the first frame update
    void Start()
    {
        panelState = false;
        PanelPlayerInfo.SetActive(panelState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        panelState = !panelState;
        PanelPlayerInfo.SetActive(panelState);
        LogAutoscrollScript.ScrollbarToBottom();
    }
}
