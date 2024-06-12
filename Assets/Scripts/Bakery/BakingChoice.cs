using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BakingChoice : MonoBehaviour
{
    private BakeryManager bakeryManager;

    [SerializeField] private List<Sprite> sprites;
    private Image image;
    private Button button;

    [SerializeField] private bool click = false;

    [SerializeField] private int index;
    [SerializeField] private int indexCheck;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();

        buttonCheck();
    }

    private void Start()
    {
        bakeryManager = BakeryManager.Instance;
    }

    private void buttonCheck()
    {
        button.onClick.AddListener(() =>
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            if (click == false)
            {
                if (index < 3 && bakeryManager.GetChoiceA() == -1)
                {
                    bakeryManager.SetChoiceA(index);
                    image.sprite = sprites[1];
                    indexCheck = index;
                    click = true;
                }
                else if (index >= 3 && index < 6 && bakeryManager.GetChoiceB() == -1)
                {
                    bakeryManager.SetChoiceB(index);
                    image.sprite = sprites[1];
                    indexCheck = index;
                    click = true;
                }
                else if (index >= 6 && bakeryManager.GetChoiceC() == -1)
                {
                    bakeryManager.SetChoiceC(index);
                    image.sprite = sprites[1];
                    indexCheck = index;
                    click = true;
                }
            }
            else
            {
                if (index < 3)
                {
                    bakeryManager.SetChoiceA(-1);
                    image.sprite = sprites[0];
                    indexCheck = -1;
                    click = false;
                }
                else if (index >= 3 && index < 6)
                {
                    bakeryManager.SetChoiceB(-1);
                    image.sprite = sprites[0];
                    indexCheck = -1;
                    click = false;
                }
                else
                {
                    bakeryManager.SetChoiceC(-1);
                    image.sprite = sprites[0];
                    indexCheck = -1;
                    click = false;
                }
            }
        });
    }

    public int GetIndexCheck()
    {
        return indexCheck;
    }

    public void GetReset()
    {
        image.sprite = sprites[0];
        indexCheck = -1;
        click = false;
    }
}
