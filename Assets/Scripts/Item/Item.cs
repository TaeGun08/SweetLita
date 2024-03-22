using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Ingredient,

    }

    [Header("아이템 설정")]
    [SerializeField] private ItemType itemType;
    [SerializeField] private int itemIndex;

    /// <summary>
    /// 아이템 타입을 넘겨주기 위한 함수
    /// </summary>
    /// <returns></returns>
    public ItemType GetItemType()
    {
        return itemType;
    }

    /// <summary>
    /// 아이템 인덱스를 넘겨주기 위한 함수
    /// </summary>
    /// <returns></returns>
    public int GetItemIndex()
    {
        return itemIndex;
    }
}
