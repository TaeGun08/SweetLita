using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [Header("아이템 이미지")]
    [SerializeField] private List<Sprite> itemSprites;

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

    public Sprite GetItemSprite(int _itemIndex)
    {
        switch (_itemIndex)
        {
            case 100:
                return itemSprites[0];
            case 101:
                return itemSprites[1];
            case 102:
                return itemSprites[2];
        }

        return null;
    }
}
