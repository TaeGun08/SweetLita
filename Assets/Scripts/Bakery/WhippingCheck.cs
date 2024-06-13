using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WhippingCheck : MonoBehaviour, IPointerEnterHandler
{
    private BakeryManager bakeryManager;

    [SerializeField] private int index;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
        if (bakeryManager.GetWhippingIndex(0) == 0 && index == 0)
        {
            bakeryManager.SetWhippingIndex(0, 1);
        }
        else if (bakeryManager.GetWhippingIndex(0) != 0 && index == 1)
        {
            bakeryManager.SetWhippingIndex(1, 1);
        }
        else if (bakeryManager.GetWhippingIndex(1) != 0 && index == 2)
        {
            bakeryManager.SetWhippingIndex(2, 1);
        }
        else if (bakeryManager.GetWhippingIndex(2) != 0 && index == 3)
        {
            bakeryManager.SetWhippingIndex(3, 1);
        }
    }

    private void Start()
    {
        bakeryManager = BakeryManager.Instance;
    }
}
