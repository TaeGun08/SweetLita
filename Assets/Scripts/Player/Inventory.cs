using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private int maxQuantiry;
    [SerializeField] private List<Slot> slot;

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

            if (slotSc.GetItemIndex() == _itemIndex && slotSc.GetSlotQuantiry() < maxQuantiry)
            {
                slotSc.SetSlot(_itemIndex, _itemObj);
                return;
            }
            else if (slotSc.GetItemIndex() == 0 && slotSc.GetSlotQuantiry() < maxQuantiry)
            {
                slotSc.SetSlot(_itemIndex, _itemObj);
                return;
            }
        }
    }
}
