using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CookingNode : MonoBehaviour
{
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rect.sizeDelta -= new Vector2(200f, 200f) * Time.deltaTime / 1.5f;
    }
}
