using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOffEquipmentButtonScript : MonoBehaviour
{
    /// <summary>
    /// Сообщает, что игрок снял экипированный предмет
    /// </summary>
    public event TakeOffEquipmentNotifier NotifyTakeOffEquipment;
    public delegate void TakeOffEquipmentNotifier();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeOffEquipment()
    {
        NotifyTakeOffEquipment?.Invoke();
    }
}
