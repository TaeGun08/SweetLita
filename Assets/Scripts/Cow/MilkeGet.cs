using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkeGet : MonoBehaviour
{
    private bool check = false;
    private bool milkeGet = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            check = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            check = false;
            milkeGet = true;
        }
    }

    public bool ReturnCheck()
    {
        return check;
    }

    public bool ReturnMilkeGet()
    {
        return milkeGet;
    }
}
