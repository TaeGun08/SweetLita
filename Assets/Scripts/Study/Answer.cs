using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    [SerializeField] private int check;
    [SerializeField] private List<Sprite> sprites;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public int Check()
    {
        return check;
    }

    public void SetSpriteNumber(int _number)
    {
        image.sprite = sprites[_number];
    }

    public void SetCheck(int _number)
    {
        check = _number;
    }
}
