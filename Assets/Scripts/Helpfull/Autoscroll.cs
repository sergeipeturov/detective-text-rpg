using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Скрипт для автоскролинга в ScrollView. Вешать на ScrollView, в поле ScrollRect указать этот же ScrollView. Для прокрутки в самый низ вызвать в нужном месте ScrollbarToBottom()
/// </summary>
public class Autoscroll : MonoBehaviour
{
    public ScrollRect ScrollRect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Прокрутить скроллбар в самый низ
    /// </summary>
    public void ScrollbarToBottom()
    {
        //костыль, чтобы сдвигалось, когда activeState == true, а то без этого не сдвигалось
        if (ScrollRect.gameObject.activeSelf)
        {
            ScrollRect.gameObject.SetActive(false);
            ScrollRect.gameObject.SetActive(true);
        }
        ScrollRect.verticalNormalizedPosition = backup;
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)ScrollRect.transform);
    }

    float backup = 0;
}
