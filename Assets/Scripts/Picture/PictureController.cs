using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [Space]
    [SerializeField] private GameObject gameEndObject;
    [SerializeField] private List<Button> buttons;

    private bool retry = false;
    private bool gameClear = false;

    private float textChangeTimer;
    private bool textChanageOn = false;
    private float textStartTimer;
    private bool textStartCheck = false;

    [SerializeField] private List<GameObject> clearOverObject;
    [SerializeField] private List<GameObject> gameStartObject;
    [Space]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource vfxAudio;
    [SerializeField] private List<AudioClip> audioClips;

    private void Start()
    {
        pictureManager = PictureManager.Instance;

        Screen.SetResolution(1920, 1080, true);

        gameStartButton.onClick.AddListener(() => 
        {
            explanationWindow.SetActive(false);
            gameStart = true;
            vfxAudio.clip = audioClips[0];
            vfxAudio.Play();
        });

        buttons[0].onClick.AddListener(() =>
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
            vfxAudio.clip = audioClips[0];
            vfxAudio.Play();
        });

        buttons[1].onClick.AddListener(() =>
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
            retry = true;
            vfxAudio.clip = audioClips[0];
            vfxAudio.Play();
        });

        timer = 2;

        fadeTimer = 2;

        fadeCheck = true;

        fadeInOutCheck = true;

        textChangeTimer = 3;

        frameImage.sprite = frameSprites[0];
    }

    private void Update()
    {
        fadeInOut();

        if (gameStart == true && textChanageOn == false)
        {
            textChangeTimer -= Time.deltaTime;

            if (textChangeTimer > 2 && gameStartObject[0].activeSelf == false)
            {
                gameStartObject[0].SetActive(true);
            }
            else if (textChangeTimer > 1 && textChangeTimer <= 2 && gameStartObject[1].activeSelf == false)
            {
                gameStartObject[0].SetActive(false);
                gameStartObject[1].SetActive(true);
            }
            else if (textChangeTimer > 0 && textChangeTimer <= 1 && gameStartObject[2].activeSelf == false)
            {
                gameStartObject[1].SetActive(false);
                gameStartObject[2].SetActive(true);
            }
            if (textChangeTimer <= 0)
            {
                gameStartObject[2].SetActive(false);
                textChanageOn = true;
            }
        }
        else if (gameStart == true && textChanageOn == true)
        {
            textStartTimer += Time.deltaTime;

            if (textStartTimer < 1f)
            {
                gameStartObject[3].SetActive(true);
            }
            else
            {
                gameStartObject[3].SetActive(false);
                textStartCheck = true;
            }
        }

        if (gameStart == true && textStartCheck == true)
        {
            if (pictureManager.GameClearCheck() == true && clearOverObject[0].activeSelf == false)
            {
                vfxAudio.clip = audioClips[1];
                vfxAudio.Play();
                gameClear = true;
                clearOverObject[0].SetActive(true);
                gameEndObject.SetActive(true);
                return;
            }
            else if (pictureManager.GameOverCheck() == true && clearOverObject[1].activeSelf == false)
            {
                audioSource.gameObject.SetActive(false);
                vfxAudio.clip = audioClips[2];
                vfxAudio.Play();
                clearOverObject[1].SetActive(true);
                gameEndObject.SetActive(true);
                return;
            }

            nextPicture();
            timerCheck();
        }
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
                    fadeTimer = 0;
                    fadeImage.gameObject.SetActive(false);
                    fadeInOutCheck = false;
                    fadeCheck = false;
                }
            }
            else if (fadeColor.a != 1 && fadeInOutCheck == false)
            {
                fadeTimer += Time.deltaTime / 2;
                fadeColor.a = fadeTimer;
                fadeImage.color = fadeColor;

                if (fadeColor.a >= 1)
                {
                    if (retry == true && gameClear == true)
                    {
                        int saveIndex = 3;
                        string getSaveData = PlayerPrefs.GetString("saveDataKey");
                        int saveData = JsonConvert.DeserializeObject<int>(getSaveData);
                        if (saveIndex >= saveData)
                        {
                            string setSaveData = JsonConvert.SerializeObject(4);
                            PlayerPrefs.SetString("saveDataKey", setSaveData);
                        }

                        SceneManager.LoadSceneAsync("Loading");
                        string setLoding = JsonConvert.SerializeObject("Picture");
                        PlayerPrefs.SetString("saveScene", setLoding);
                    }
                    else if (retry == true && gameClear == false)
                    {
                        SceneManager.LoadSceneAsync("Loading");
                        string setLoding = JsonConvert.SerializeObject("Picture");
                        PlayerPrefs.SetString("saveScene", setLoding);
                    }
                    else if (gameClear == false)
                    {
                        SceneManager.LoadSceneAsync("Loading");
                        string setLoding = JsonConvert.SerializeObject("Chapter1");
                        PlayerPrefs.SetString("saveScene", setLoding);
                    }
                    else if (gameClear == true)
                    {
                        int saveIndex = 3;
                        string getSaveData = PlayerPrefs.GetString("saveDataKey");
                        int saveData = JsonConvert.DeserializeObject<int>(getSaveData);
                        if (saveIndex >= saveData)
                        {
                            string setSaveData = JsonConvert.SerializeObject(4);
                            PlayerPrefs.SetString("saveDataKey", setSaveData);
                        }

                        SceneManager.LoadSceneAsync("Loading");
                        string setLoding = JsonConvert.SerializeObject("Chapter1");
                        PlayerPrefs.SetString("saveScene", setLoding);
                    }
                }
            }
        }
    }
}
