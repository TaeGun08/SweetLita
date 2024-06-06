using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Answer : MonoBehaviour
{
    [SerializeField] private int check;
    [SerializeField] private TMP_Text text;

    public int Check()
    {
        return check;
    }

    public void SetCheck(int _number)
    {
        text.text = $"{_number}";
        check = _number;
    }
}
