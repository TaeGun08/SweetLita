using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;
    [Space]
    [SerializeField] private Image fadeImage;
    private float fadeTimer;
    [SerializeField] private bool fadeCheck = false;
    private bool fadeInOutCheck = false;

    [SerializeField] private AudioSource vfxAudio;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);

        buttons[0].onClick.AddListener(() =>
        {
            vfxAudio.Play();
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
        });

        buttons[1].onClick.AddListener(() =>
        {
            vfxAudio.Play();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });

        buttons[2].onClick.AddListener(() =>
        {
            vfxAudio.Play();

            string setSaveData = JsonConvert.SerializeObject(0);
            PlayerPrefs.SetString("saveDataKey", setSaveData);
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
                    string setLoding = JsonConvert.SerializeObject("ChapterChoice");
                    PlayerPrefs.SetString("saveScene", setLoding);
                    SceneManager.LoadSceneAsync("Loading");
                }
            }
        }
    }
}
