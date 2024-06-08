using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PictureController : MonoBehaviour
{
    private PictureManager pictureManager;

    [Header("그림 설정")]
    [SerializeField, Tooltip("다음 사진까지의 시간")] private float nextTime;
    [SerializeField] private float timer;
    [SerializeField] private List<int> nextNumber = new List<int>();
    private int nextIndex;
    private bool choiceStart = false;
    [SerializeField] private TMP_Text gameClearOverText;
    [Space]
    [SerializeField] private Image frameImage;
    [SerializeField] private List<Sprite> frameSprites;
    [Space]
    [SerializeField] private GameObject explanationWindow;
    [SerializeField] private Button gameStartButton;
    private bool gameStart = false;
    [Space]
    [SerializeField] private Image fadeImage;
    private float fadeTimer;
    [SerializeField] private bool fadeCheck = false;
    private bool fadeInOutCheck = false;

    private void Start()
    {
        pictureManager = PictureManager.Instance;

        gameStartButton.onClick.AddListener(() => 
        {
            explanationWindow.SetActive(false);
            gameStart = true;
        });

        timer = 3;

        fadeTimer = 2;

        fadeCheck = true;

        fadeInOutCheck = true;

        frameImage.sprite = frameSprites[0];
    }

    private void Update()
    {
        fadeInOut();

        if (gameStart == false && fadeCheck == false)
        {
            return;
        }

        if (pictureManager.GameClearCheck() == true && gameClearOverText.gameObject.activeSelf == false)
        {
            gameClearOverText.text = "게임 클리어!";
            gameClearOverText.gameObject.SetActive(true);
            return;
        }
        else if (pictureManager.GameOverCheck() == true)
        {
            gameClearOverText.text = "게임 오버!";
            gameClearOverText.gameObject.SetActive(true);
            return;
        }

        nextPicture();
        timerCheck();
    }

    private void timerCheck()
    {
        if (nextIndex >= nextNumber.Count && choiceStart == false)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                for (int i = 0; i < nextNumber.Count; i++)
                {
                    frameImage.sprite = frameSprites[1];
                    pictureManager.GetChoicePictureObject(nextNumber[i]).SetActive(true);
                }

                pictureManager.GetPictureObject(nextNumber[nextIndex - 1]).SetActive(false);

                choiceStart = true;
            }
            return;
        }

        if (nextNumber.Count >= 3 && choiceStart == false)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                if (nextIndex > 0)
                {
                    pictureManager.GetPictureObject(nextNumber[nextIndex - 1]).SetActive(false);
                }

                pictureManager.GetPictureObject(nextNumber[nextIndex]).transform.SetAsLastSibling();
                pictureManager.GetPictureObject(nextNumber[nextIndex++]).SetActive(true);
                timer = nextTime;
            }
        }
    }

    /// <summary>
    /// 그림에 번호를 정하는 함수
    /// </summary>
    private void nextPicture()
    {
        if (nextNumber.Count == 3)
        {
            return;
        }

        int randomNumber = Random.Range(0, 3);

        if (nextNumber.Count == 0)
        {
            nextNumber.Add(randomNumber);
            pictureManager.SetPictureIndex(randomNumber);
        }
        else if (nextNumber.Count != 0)
        {
            if (nextNumberCheck(randomNumber) == false)
            {
                nextNumber.Add(randomNumber);
                pictureManager.SetPictureIndex(randomNumber);
            }
        }
    }

    /// <summary>
    /// 다음 그림의 번호를 확인하는 함수
    /// </summary>
    /// <param name="_number"></param>
    /// <returns></returns>
    private bool nextNumberCheck(int _number)
    {
        for (int i = 0; i < nextNumber.Count; i++)
        {
            if (nextNumber[i] == _number)
            {
                return true;
            }
        }

        return false;
    }

    private void fadeInOut()
    {
        if (fadeCheck == true && fadeImage.gameObject.activeSelf == true)
        {
            Color fadeColor = fadeImage.color;

            if (fadeColor.a != 0 && fadeInOutCheck == true)
            {
                fadeTimer -= Time.deltaTime;
                fadeColor.a = fadeTimer;
                fadeImage.color = fadeColor;

                if (fadeColor.a <= 0)
                {
                    fadeTimer = 2;
                    fadeImage.gameObject.SetActive(false);
                    fadeInOutCheck = false;
                    fadeCheck = false;
                }
            }
            else if (fadeColor.a != 1 && fadeInOutCheck == false)
            {
                fadeTimer += Time.deltaTime;
                fadeColor.a = fadeTimer;
                fadeImage.color = fadeColor;

                if (fadeColor.a >= 1)
                {
                    fadeTimer = 2;
                    fadeImage.gameObject.SetActive(false);
                    fadeInOutCheck = true;
                    fadeCheck = false;
                }
            }
        }
    }
}
