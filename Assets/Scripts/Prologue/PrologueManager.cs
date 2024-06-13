using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrologueManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    private float fadeTimer;
    [SerializeField] private bool fadeCheck = false;
    private bool fadeInOutCheck = false;

    [SerializeField] private List<GameObject> characterObejct;
    [SerializeField] private TMP_Text nameText;

    [SerializeField] private TMP_Text dialogueText; // NPC 대화창에 표시될 Text UI

    [SerializeField] private List<string> text; // NPC가 말할 전체 텍스트
    private string currentText; // 현재 표시 중인 텍스트
    private float delay;

    private bool next = false;
    private int nextIndex;

    private bool firstCheck = false;

    private float textTimer;

    private float nextCheckTimer;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);

        fadeTimer = 2;

        fadeCheck = true;

        fadeInOutCheck = true;

        delay = 0.05f;
    }

    private void Update()
    {
        fadeInOut();

        if (fadeCheck == false)
        {
            if (nextIndex == 7 && fadeImage.gameObject.activeSelf == false)
            {
                nextCheckTimer += Time.deltaTime;

                if (nextCheckTimer > 2f)
                {
                    fadeCheck = true;
                    fadeImage.gameObject.SetActive(true);
                }
            }

            if (firstCheck == false)
            {
                StartCoroutine(displayText());
                firstCheck = true;
            }

            if ((Input.GetKeyDown(KeyCode.Space) == true || Input.GetKeyDown(KeyCode.Mouse0)) && next == false && nextIndex < 7)
            {
                delay = 0.05f;

                StartCoroutine(displayText());

                if (nextIndex == 0)
                {
                    characterObejct[1].SetActive(true);
                }
                else if (nextIndex == 1)
                {
                    characterObejct[1].SetActive(false);
                }
                else if (nextIndex == 2)
                {
                    characterObejct[1].SetActive(true);
                }
                else if (nextIndex == 3)
                {
                    Image image = characterObejct[1].GetComponent<Image>();
                    Color color = image.color;
                    color.r = 0.5f;
                    color.g = 0.5f;
                    color.b = 0.5f;
                    image.color = color;
                    characterObejct[2].SetActive(true);
                }
                else if (nextIndex == 4)
                {
                    Image imageA = characterObejct[1].GetComponent<Image>();
                    Color colorA = imageA.color;
                    colorA.r = 1f;
                    colorA.g = 1f;
                    colorA.b = 1f;
                    imageA.color = colorA;
                    characterObejct[1].SetActive(false);
                    Image imageB = characterObejct[2].GetComponent<Image>();
                    Color colorB = imageB.color;
                    colorB.r = 0.5f;
                    colorB.g = 0.5f;
                    colorB.b = 0.5f;
                    imageB.color = colorB;
                }
                else
                {
                    characterObejct[1].SetActive(true);
                }

                nextIndex++;
                next = true;
            }

            if (next == true)
            {
                textTimer += Time.deltaTime;

                if (textTimer >= 2f)
                {
                    delay = 0.05f;
                    textTimer = 0f;
                    next = false;
                }
            }
        }
    }

    private IEnumerator displayText()
    {
        for (int iNum = 0; iNum <= text[nextIndex].Length; iNum++)
        {
            currentText = text[nextIndex].Substring(0, iNum);
            dialogueText.text = currentText;

            yield return new WaitForSeconds(delay);
        }
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
                    int saveIndex = 1;
                    string getSaveData = PlayerPrefs.GetString("saveDataKey");
                    int saveData = JsonConvert.DeserializeObject<int>(getSaveData);
                    if (saveIndex >= saveData)
                    {
                        string setSaveData = JsonConvert.SerializeObject(1);
                        PlayerPrefs.SetString("saveDataKey", setSaveData);
                    }

                    SceneManager.LoadSceneAsync("Chapter1");
                }
            }
        }
    }
}
