using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Chapter1Manager : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private List<Button> buttons;
    [Space]
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject obj;
    private float fadeTimer;
    [SerializeField] private bool fadeCheck = false;
    private bool fadeInOutCheck = false;

    [SerializeField] private int nextSceneIndex;

    [SerializeField] private GameObject nextUpdate;

    private int saveDataIndex;

    private bool backCheck = false;

    [SerializeField] private AudioSource vfxAudio;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);

        if (PlayerPrefs.GetString("saveDataKey") != string.Empty)
        {
            string getStringData = PlayerPrefs.GetString("saveDataKey");
            saveDataIndex = JsonConvert.DeserializeObject<int>(getStringData);
        }
        else
        {
            saveDataIndex = 0;

            string setStringData = JsonConvert.SerializeObject(0);
            PlayerPrefs.SetString("saveDataKey", setStringData);
        }

        backButton.onClick.AddListener(() => 
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
            backCheck = true;
            vfxAudio.Play();
        });

        buttonClick();

        fadeTimer = 2;

        fadeCheck = true;

        fadeInOutCheck = true;
    }

    private void Update()
    {
        fadeInOut();
        clearCheck();
    }

    private void clearCheck()
    {
        int count = buttons.Count;
        for (int iNum = 0; iNum < count; iNum++)
        {
            if (saveDataIndex >= iNum && buttons[iNum].gameObject.activeSelf == false)
            {
                buttons[iNum].gameObject.SetActive(true);

                if (saveDataIndex == 8)
                {
                    nextUpdate.SetActive(true);
                }
            }
        }
    }

    private void buttonClick()
    {
        buttons[0].onClick.AddListener(() =>
        {
            vfxAudio.Play();
            fadeCheck = true;
            nextSceneIndex = 0;
            fadeImage.gameObject.SetActive(true);
        });

        buttons[1].onClick.AddListener(() =>
        {
            vfxAudio.Play();
            fadeCheck = true;
            nextSceneIndex = 1;
            fadeImage.gameObject.SetActive(true);
        });

        buttons[2].onClick.AddListener(() =>
        {
            vfxAudio.Play();
            fadeCheck = true;
            nextSceneIndex = 2;
            fadeImage.gameObject.SetActive(true);
        });

        buttons[3].onClick.AddListener(() =>
        {
            vfxAudio.Play();
            fadeCheck = true;
            nextSceneIndex = 3;
            fadeImage.gameObject.SetActive(true);
        });

        buttons[4].onClick.AddListener(() =>
        {
            vfxAudio.Play();
            fadeCheck = true;
            nextSceneIndex = 4;
            fadeImage.gameObject.SetActive(true);
        });

        buttons[5].onClick.AddListener(() =>
        {
            vfxAudio.Play();
            fadeCheck = true;
            nextSceneIndex = 5;
            fadeImage.gameObject.SetActive(true);
        });

        buttons[6].onClick.AddListener(() =>
        {
            vfxAudio.Play();
            fadeCheck = true;
            nextSceneIndex = 6;
            fadeImage.gameObject.SetActive(true);
        });

        buttons[7].onClick.AddListener(() =>
        {
            vfxAudio.Play();
            fadeCheck = true;
            nextSceneIndex = 7;
            fadeImage.gameObject.SetActive(true);
        });
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
                    if (backCheck == true)
                    {
                        SceneManager.LoadSceneAsync("Loading");
                        string setLoding = JsonConvert.SerializeObject("ChapterChoice");
                        PlayerPrefs.SetString("saveScene", setLoding);
                    }
                    else
                    {
                        nextScene();
                    }
                }
            }
        }
    }

    private void nextScene()
    {
        if (nextSceneIndex == 0)
        {
            SceneManager.LoadSceneAsync("Loading");
            string setLoding = JsonConvert.SerializeObject("Prologue");
            PlayerPrefs.SetString("saveScene", setLoding);
        }
        else if (nextSceneIndex == 1)
        {
            SceneManager.LoadSceneAsync("Loading");
            string setLoding = JsonConvert.SerializeObject("Puzzle");
            PlayerPrefs.SetString("saveScene", setLoding);
        }
        else if (nextSceneIndex == 2)
        {
            SceneManager.LoadSceneAsync("Loading");
            string setLoding = JsonConvert.SerializeObject("CookingMusic");
            PlayerPrefs.SetString("saveScene", setLoding);
        }
        else if (nextSceneIndex == 3)
        {
            SceneManager.LoadSceneAsync("Loading");
            string setLoding = JsonConvert.SerializeObject("Picture");
            PlayerPrefs.SetString("saveScene", setLoding);
        }
        else if (nextSceneIndex == 4)
        {
            SceneManager.LoadSceneAsync("Loading");
            string setLoding = JsonConvert.SerializeObject("Clothes");
            PlayerPrefs.SetString("saveScene", setLoding);
        }
        else if (nextSceneIndex == 5)
        {
            SceneManager.LoadSceneAsync("Loading");
            string setLoding = JsonConvert.SerializeObject("Bakery");
            PlayerPrefs.SetString("saveScene", setLoding);
        }
        else if (nextSceneIndex == 6)
        {
            SceneManager.LoadSceneAsync("Loading");
            string setLoding = JsonConvert.SerializeObject("Cow");
            PlayerPrefs.SetString("saveScene", setLoding);
        }
        else if (nextSceneIndex == 7)
        {
            SceneManager.LoadSceneAsync("Loading");
            string setLoding = JsonConvert.SerializeObject("Study");
            PlayerPrefs.SetString("saveScene", setLoding);
        }
    }
}
