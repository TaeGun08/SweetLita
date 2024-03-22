using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private int itemIndex;
    [SerializeField] private int slotQuantity;
    private bool maxItem = false;

    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemQuantityText;

    private void Start()
    {
        if (itemIndex == 0)
        {
            itemQuantityText.text = "";
        }
    }

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

        if (itemIndex == 0)
        {
            if (slotQuantity == 0)
            {
                return;
            }

            itemImage.sprite = null;
            slotQuantity = 0;
            itemQuantityText.text = slotQuantity.ToString();
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
            SpriteRenderer itemSpR = _itemObj.GetComponent<SpriteRenderer>();
            Sprite itemSr = itemSpR.sprite;
            itemImage.sprite = itemSr;
            itemQuantityText.text = slotQuantity.ToString();
            Destroy(_itemObj);
        }
        else
        {
            SpriteRenderer itemSpR = _itemObj.GetComponent<SpriteRenderer>();
            Sprite itemSr = itemSpR.sprite;
            itemImage.sprite = itemSr;
            ++slotQuantity;
            itemQuantityText.text = slotQuantity.ToString();
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
