using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheesePiece : MonoBehaviour
{
    [SerializeField] private GameObject starForce;
    private bool playerIn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerIn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerIn = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIn == true)
        {
            starForce.SetActive(true);
        }
    }
}
