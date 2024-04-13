using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpineOtherLayer : MonoBehaviour
{
    [SerializeField] private MeshRenderer spriteRender;
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
        if (playerIn == true)
        {
            spriteRender.sortingOrder = 4;
        }
        else
        {
            spriteRender.sortingOrder = -10;
        }
    }
}
