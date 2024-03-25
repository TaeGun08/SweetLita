using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] private GameObject inventory;
    [SerializeField] private int maxQuantiry;
    [SerializeField] private List<Slot> slot;
    private int questItems;
    private int qeustItemIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        inventory.SetActive(false);
    }

    private void Update()
    {
        inventoryOnOff();
    }

    /// <summary>
    /// 인벤토리를 껐다 킬 수 있게 하는 함수
    /// </summary>
    private void inventoryOnOff()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool invenOnOff = inventory == inventory.activeSelf ? false : true;
            inventory.SetActive(invenOnOff);
        }
    }

    public void SetItem(int _itemIndex, Item.ItemType _itemType, GameObject _itemObj)
    {
        for (int i = 0; i < slot.Count; i++)
        {
            Slot slotSc = slot[i];

            if (slotSc.GetItemIndex() == _itemIndex && slotSc.GetSlotQuantity() < maxQuantiry)
            {
                slotSc.SetSlot(_itemIndex, _itemObj);
                return;
            }
            else if (slotSc.GetItemIndex() == 0 && slotSc.GetSlotQuantity() < maxQuantiry)
            {
                slotSc.SetSlot(_itemIndex, _itemObj);
                return;
            }
        }
    }

    public bool QuestItemCheck(int _itemIndex , int _itemQuantity)
    {
        qeustItemIndex = _itemIndex;

        int itmeQuantity = 0;

        for (int i = 0; i < slot.Count; i++)
        {
            Slot slotSc = slot[i];

            if (slotSc.GetItemIndex() == qeustItemIndex)
            {
                itmeQuantity += slotSc.GetSlotQuantity();
            }
        }

        return itmeQuantity >= _itemQuantity;
    }

    public void QuestItem(int _itemIndex, int _questItem)
    {
        questItems = _questItem;

        for (int i = 0; i < slot.Count; i++)
        {
            Slot slotSc = slot[i];

            if (slotSc.GetItemIndex() == _itemIndex && slotSc.GetSlotQuantity() != 0)
            {
                if (questItems == 0)
                {
                    return;
                }

                questItems = slotSc.QuestItem(questItems);
            }
        }
    }

    public List<Slot> GetSlot()
    {
        return slot;
    }

    public int QuestItems()
    {
        return questItems;
    }
}
