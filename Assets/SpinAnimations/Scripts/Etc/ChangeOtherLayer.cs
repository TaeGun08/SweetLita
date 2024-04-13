using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOtherLayer : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> spriteRender;
    private bool playerIn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerIn = false;
        }
    }

    private void Update()
    {
        int count = spriteRender.Count;
        if (playerIn == true)
        {
            for (int i = 0; i < count; i++)
            {
                spriteRender[i].sortingOrder = 4;
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                spriteRender[i].sortingOrder = -10;
            }
        }
    }
}
