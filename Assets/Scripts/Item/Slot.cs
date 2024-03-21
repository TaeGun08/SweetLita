using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private List<GameObject> item;
    [SerializeField] private int slotIndex;

    public void ListItem(GameObject _item)
    {
        if (item.Count == 20)
        {
            return;
        }

        item.Add(_item);
    }

    public int SlotIndex()
    {
        return slotIndex = item.Count;
    }
}
