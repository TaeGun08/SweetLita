using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PictureChoice : MonoBehaviour, IPointerClickHandler
{
    private PictureManager pictureManager;

    [Header("사진 설정")]
    [SerializeField, Tooltip("사진의 인덱스")] private int pictureIndex;
    private TMP_Text numberText;
    [SerializeField] private int number;
    private bool choiceNumberCheck = false;
    [SerializeField] private AudioSource vfxAudio;
    [SerializeField] private AudioClip audioClip;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (pictureManager.GameClearCheck() == true)
        {
            return;
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (choiceNumberCheck == false)
            {
                vfxAudio.clip = audioClip;
                vfxAudio.Play();
                bool numberTextOn = numberText.gameObject == numberText.gameObject.activeSelf ? false : true;
                numberText.gameObject.SetActive(numberTextOn);
                numberText.text = $"{pictureManager.GetChoiceNumber()+ 1}";
                number = pictureManager.GetChoiceNumber();
                pictureManager.PictureCheck(pictureIndex);
                choiceNumberCheck = true;
                pictureManager.SetChoiceNumber(true);
            }
            else if (choiceNumberCheck == true && number == (pictureManager.GetChoiceNumber() - 1))
            {
                vfxAudio.clip = audioClip;
                vfxAudio.Play();
                bool numberTextOn = numberText.gameObject == numberText.gameObject.activeSelf ? false : true;
                numberText.gameObject.SetActive(numberTextOn);
                pictureManager.SetChoiceNumber(numberTextOn);
                number = -1;
                pictureManager.PictureCheck(-2);
                choiceNumberCheck = false;
            }
        }
    }

    private void Awake()
    {
        numberText = transform.GetChild(1).GetComponent<TMP_Text>();

        numberText.gameObject.SetActive(false);
    }

    private void Start()
    {
        pictureManager = PictureManager.Instance;
    }
}
