using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTextScript : MonoBehaviour
{
    public delegate void SelfClickEventNotifier(QuestAction action);
    /// <summary>
    /// Сообщает, что был произведен клик на этот объект
    /// </summary>
    public event SelfClickEventNotifier SelfClickEvent;

    /// <summary>
    /// Действие, соответствующее этому тексту
    /// </summary>
    public QuestAction Action { get; set; } = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        SelfClickEvent?.Invoke(Action);
    }
}
