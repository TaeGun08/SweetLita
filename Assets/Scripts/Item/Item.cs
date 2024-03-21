using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("아이템 설정")]
    [SerializeField] private int itemIndex;

    public int ItemIndex()
    {
        return itemIndex;
    }
}
