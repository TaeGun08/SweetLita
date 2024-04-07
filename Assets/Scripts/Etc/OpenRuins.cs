using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class OpenRuins : MonoBehaviour
{
    private SkeletonAnimation skeletonAnim;

    private bool open = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            open = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            open = false;
        }
    }

    private void Awake()
    {
        skeletonAnim = GetComponent<SkeletonAnimation>();

        skeletonAnim.timeScale = 0;
    }

    private void Update()
    {
        if (open == true)
        {
            skeletonAnim.timeScale = 1;
        }
    }
}
