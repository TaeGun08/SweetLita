using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    private ItemManager itemManager;

    [Header("슬롯 설정")]
    [SerializeField, Tooltip("현재 가지고 있는 아이템")] private int itemIndex;
    [SerializeField, Tooltip("현재 가지고 있는 아이템 개수")] private int slotQuantity;
    private bool maxItem = false;
    private int _itemQuantity;

    [SerializeField, Tooltip("아이템을 보여줄 이미지")] private Image itemImag;
    [SerializeField, Tooltip("아이템 개수를 표시할 텍스트")] private TMP_Text itemQuantityText;
    private bool setImage = false;

    private void Start()
    {
        inventory = Inventory.Instance;

        itemManager = ItemManager.Instance;

        if (itemIndex == 0)
        {
            itemQuantityText.text = "";
        }

        Color color = itemImag.color;
        color.a = 0;
        itemImag.color = color;
    }

    private void Update()
    {
        slotCheck();
        changeSprite();
    }

    /// <summary>
    /// 슬롯을 체크하고 관리하는 함수
    /// </summary>
    private void slotCheck()
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
            if (itemImag.sprite == null)
            {
                return;
            }

            slotQuantity = 0;
            itemImag.sprite = null;
            Color color = itemImag.color;
            color.a = 0;
            itemImag.color = color;
            itemQuantityText.text = "";
        }

        if (slotQuantity <= 0)
        {
            if (itemImag.sprite == null)
            {
                return;
            }

            slotQuantity = 0;
            itemImag.sprite = null;
            Color color = itemImag.color;
            color.a = 0;
            itemImag.color = color;
            itemQuantityText.text = "";
        }
    }

    private void changeSprite()
    {
        if (inventory.InventoryObj().activeSelf == true && setImage == false)
        {
            if (itemIndex == 0 || slotQuantity == 0)
            {
                itemImag.sprite = null;
                Color color = itemImag.color;
                color.a = 0;
                itemImag.color = color;
                return;
            }

            itemImag.sprite = itemManager.GetItemSprite(itemIndex);
            setImage = true;
        }
        else if (inventory.InventoryObj().activeSelf == false && setImage == true)
        {
            setImage = false;
        }
    }

    /// <summary>
    /// 슬롯에 아이템을 넣어줄 함수
    /// </summary>
    /// <param name="_itemIndex"></param>
    /// <param name="_itemObj"></param>
    public void SetSlot(int _itemIndex)
    {
        if (maxItem == true)
        {
            return;
        }

        if (itemIndex == 0)
        {
            itemIndex = _itemIndex;
            slotQuantity = 1;
            if (inventory.InventoryObj().activeSelf == true)
            {
                itemImag.sprite = itemManager.GetItemSprite(itemIndex);
            }
            itemQuantityText.text = slotQuantity.ToString();
        }
        else
        {
            if (inventory.InventoryObj().activeSelf == true)
            {
                itemImag.sprite = itemManager.GetItemSprite(itemIndex);
            }
            ++slotQuantity;
            itemQuantityText.text = slotQuantity.ToString();
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
            if (inventory.InventoryObj().activeSelf == true)
            {
                itemImag.sprite = itemManager.GetItemSprite(itemIndex);
            }
            itemQuantityText.text = slotQuantity.ToString();
            Destroy(_itemObj);
        }
        else
        {
            if (inventory.InventoryObj().activeSelf == true)
            {
                itemImag.sprite = itemManager.GetItemSprite(itemIndex);
            }
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
