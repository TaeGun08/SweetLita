using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [Header("슬롯 설정")]
    [SerializeField, Tooltip("현재 가지고 있는 아이템")] private int itemIndex;
    [SerializeField, Tooltip("현재 가지고 있는 아이템 개수")] private int slotQuantity;
    private bool maxItem = false;
    private int _itemQuantity;

    [SerializeField, Tooltip("아이템을 보여줄 이미지")] private Image itemImage;
    [SerializeField, Tooltip("아이템 개수를 표시할 텍스트")] private TMP_Text itemQuantityText;

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

        if (slotQuantity <= 0)
        {
            if (itemImage.sprite == null)
            {
                return;
            }

            slotQuantity = 0;
            itemImage.sprite = null;
            itemQuantityText.text = "";
        }
    }

    /// <summary>
    /// 슬롯에 아이템을 넣어줄 함수
    /// </summary>
    /// <param name="_itemIndex"></param>
    /// <param name="_itemObj"></param>
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

    /// <summary>
    /// 슬롯에 있는 아이템을 보내줄 함수
    /// </summary>
    /// <returns></returns>
    public int GetItemIndex()
    {
        return itemIndex;
    }

    /// <summary>
    /// 슬롯에 있는 아이템의 개수를 보내줄 함수
    /// </summary>
    /// <returns></returns>
    public int GetSlotQuantity()
    {
        return slotQuantity;
    }

    /// <summary>
    /// 퀘스트에 필요한 아이템을 지우기 위한 함수
    /// </summary>
    /// <param name="_slotQuantity"></param>
    /// <returns></returns>
    public int QuestItem(int _slotQuantity)
    {
        for (int i = 0; i < _slotQuantity; i++)
        {
            slotQuantity -= 1;
            itemQuantityText.text = slotQuantity.ToString();

            ++_itemQuantity;

            if (slotQuantity <= 0)
            {
                itemIndex = 0;
                slotQuantity = 0;
                itemImage.sprite = null;
                itemQuantityText.text = "";

                return _slotQuantity -= _itemQuantity;
            }
        }

        return 0;
    }

    /// <summary>
    /// 저정했던 데이터를 받아올 함수
    /// </summary>
    public void SetSlotData(int _itemIndex, int _slotQuantity)
    {
        itemIndex = _itemIndex;
        slotQuantity = _slotQuantity;
        itemQuantityText.text = slotQuantity.ToString();
    }
}
