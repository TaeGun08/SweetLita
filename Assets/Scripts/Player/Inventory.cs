using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("아이템 설정")]
    [SerializeField] private List<GameObject> item;
    [SerializeField] private List<GameObject> slot;
    [SerializeField] private int itemIndex = 0;
    [SerializeField] private int slotIndex = 0;

    public void SetItem(GameObject _item)
    {
        if (0 == item.Count)
        {
            item.Add(_item);
        }
        else
        {
            for (int i = 0; i < item.Count; i++)
            {
                for (int j = 0; j < item.Count; j++)
                {
                    if (item[i].gameObject.name == item[j].gameObject.name)
                    {
                        return;
                    }
                    else
                    {
                        item.Add(_item);
                    }
                }
            }
        }
    }

    public int InventoryCheck()
    {
        for (int i = 0; i < item.Count; i++)
        {
            Item itemSc = item[i].GetComponent<Item>();
            itemIndex = itemSc.ItemIndex();
        }

        return itemIndex;
    }

    public int SlotCheck()
    {
        for (int i = 0; i < slot.Count; i++)
        {
            Slot slotSc = slot[i].GetComponent<Slot>();
            slotIndex = slotSc.SlotIndex();
        }

        return slotIndex;
    }
}
