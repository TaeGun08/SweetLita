using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAnswer : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetSpriteNumber(int _number)
    {
        image.sprite = sprites[_number];
    }
}
