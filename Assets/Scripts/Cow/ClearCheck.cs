using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCheck : MonoBehaviour
{
    private bool clear = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            clear = true;
        }
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public bool ReturnChear()
    {
        return clear;
    }
}
