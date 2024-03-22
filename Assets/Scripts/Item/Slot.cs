using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private int itemIndex;
    [SerializeField] private int slotQuantity;
    private bool maxItem = false;

    private void Update()
    {
        if (slotQuantity == 20)
        {
            if (maxItem == true)
            {
                return;
            }

            maxItem = true;
        }
        else if (slotQuantity < 20)
        {
            if (maxItem == false)
            {
                return;
            }

            maxItem = false;
        }
    }

    public void SetSlot(int _itemIndex, GameObject _itemObj)
    {
        if (maxItem == true)
        {
            return;
        }

        if (itemIndex == 0)
        {
            itemIndex = _itemIndex;
            slotQuantity = 1;
        }
        else
        {
            ++slotQuantity;
            Destroy(_itemObj);
        }
    }

    public int GetItemIndex()
    {
        return itemIndex;
    }

    public int GetSlotQuantiry()
    {
        return slotQuantity;
    }
}
