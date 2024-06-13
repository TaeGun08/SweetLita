using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChapterChoiceManager : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Button backButton;
    [Space]
    [SerializeField] private Image fadeImage;
    private float fadeTimer;
    [SerializeField] private bool fadeCheck = false;
    private bool fadeInOutCheck = false;

    private bool backCheck = false;

    [SerializeField] private AudioSource vfxAudio;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);

        button.onClick.AddListener(() =>
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
            vfxAudio.Play();
        });

        backButton.onClick.AddListener(() =>
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
            backCheck = true;
            vfxAudio.Play();
        });

        fadeTimer = 2;

        fadeCheck = true;

        fadeInOutCheck = true;
    }

    private void Update()
    {
        fadeInOut();
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
                        string setLoding = JsonConvert.SerializeObject("Main");
                        PlayerPrefs.SetString("saveScene", setLoding);
                    }
                    else
                    {
                        SceneManager.LoadSceneAsync("Loading");
                        string setLoding = JsonConvert.SerializeObject("Chapter1");
                        PlayerPrefs.SetString("saveScene", setLoding);
                    }
                }
            }
        }
    }
}
