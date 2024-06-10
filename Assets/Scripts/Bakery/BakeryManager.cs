using Newtonsoft.Json;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BakeryManager : MonoBehaviour
{
    public static BakeryManager Instance;

    private int next;

    [SerializeField] private List<Button> buttons;
    [SerializeField] private GameObject layoutObject;
    [SerializeField] private int choiceA = -1;
    [SerializeField] private int choiceB = -1;
    [SerializeField] private int choiceC = -1;
    [Space]
    [SerializeField] private List<BakingChoice> choice;
    [Space]
    [SerializeField] private GameObject whippingObject;
    [SerializeField] private int whippingCheck;
    [SerializeField] private List<int> whipping = new List<int>();
    [SerializeField] private TMP_Text whippingCount;
    [Space]
    [SerializeField] private GameObject choco;
    private bool chocoCheck = false;
    private bool chickCheck = false;
    [SerializeField] private int chocoIndex;
    private bool nextChoco;
    private float clickTimer;
    private bool timerOn = false;
    [Space]
    [SerializeField] private GameObject gameEndObject;
    [SerializeField] private List<Button> endButton;
    [Space]
    [SerializeField] private GameObject explanationWindow;
    [SerializeField] private Button gameStartButton;
    private bool gameStart = false;
    private bool gameEnd = false;
    private bool gameClear = false;
    private bool retry = false;
    [Space]
    [SerializeField] private Image fadeImage;
    private float fadeTimer;
    [SerializeField] private bool fadeCheck = false;
    private bool fadeInOutCheck = false;

    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        buttons[0].onClick.AddListener(() =>
        {
            if (next == 0)
            {
                layoutObject.SetActive(false);
            }

            buttons[0].gameObject.SetActive(false);
            whippingObject.SetActive(true);
            whippingCheck = 0;
            next++;
        });

        buttons[1].onClick.AddListener(() =>
        {
            if (next == 1)
            {
                layoutObject.SetActive(true);
                choiceA = -1;
                choiceB = -1;
                choiceC = -1;
                int count = choice.Count;
                for (int iNum = 0; iNum < count; iNum++)
                {
                    choice[iNum].GetReset();
                }
            }
            next--;
            whippingCheck = 0;
            whippingObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
        });

        gameStartButton.onClick.AddListener(() =>
        {
            explanationWindow.SetActive(false);
            gameStart = true;
        });

        endButton[0].onClick.AddListener(() =>
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
        });

        endButton[1].onClick.AddListener(() =>
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
            retry = true;
        });

        fadeTimer = 2;

        fadeCheck = true;

        fadeInOutCheck = true;

        for (int iNum = 0; iNum < 4; iNum++)
        {
            whipping.Add(0);
        }
    }

    private void Update()
    {
        fadeInOut();

        if (gameStart == true)
        {
            foodIndexCheck();
            whippingIndexCheck();
            chocoClickCheck();
        }
    }

    private void foodIndexCheck()
    {
        if (choiceA != -1 &&
            choiceB != -1 &&
            choiceC != -1 && next == 0 && buttons[1].gameObject.activeSelf == false)
        {
            buttons[0].gameObject.SetActive(true);
        }
        else if (next == 1 && buttons[0].gameObject.activeSelf == false && choco.activeSelf == false)
        {
            buttons[1].gameObject.SetActive(true);
        }
        else if ((choiceA == -1 ||
            choiceB == -1 ||
            choiceC == -1) && next == 0 && buttons[0].gameObject.activeSelf == true)
        {
            buttons[0].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
        }
    }

    private void whippingIndexCheck()
    {
        if (whippingCheck == 5 && choco.activeSelf == false)
        {
            buttons[1].gameObject.SetActive(false);
            choco.SetActive(true);
            chocoCheck = true;
            return;
        }

        if (whipping[0] == 1 &&
            whipping[1] == 1 &&
            whipping[2] == 1 &&
            whipping[3] == 1)
        {
            whippingCheck++;
            whippingCount.text = $"{whippingCheck}";
            whipping[0] = 0;
            whipping[1] = 0;
            whipping[2] = 0;
            whipping[3] = 0;
        }
    }

    private void chocoClickCheck()
    {
        if (chocoIndex == 12)
        {
            if (gameEnd == false)
            {
                if (choiceA == 2 &&
                    choiceB == 3 &&
                    choiceC == 8 && gameClear == false)
                {
                    text.text = "게임 클리어!";
                    gameClear = true;
                }

                gameEnd = true;

                gameEndObject.SetActive(true);

                text.gameObject.SetActive(true);
            }
            return;
        }

        if (timerOn == true)
        {
            clickTimer += Time.deltaTime;

            if (clickTimer < 3 && clickTimer >= 2 && chickCheck == false)
            {
                nextChoco = true;
            }
            if (clickTimer >= 3f)
            {
                timerOn = false;
                chocoIndex++;
                chickCheck = false;
                clickTimer = 0;
            }
        }

        if (chocoCheck == true && Input.GetKeyDown(KeyCode.Mouse0) && timerOn == false)
        {
            SkeletonGraphic skeletonGraphic = choco.GetComponent<SkeletonGraphic>();

            if (chocoIndex == 0)
            {
                skeletonGraphic.startingAnimation = "2_Cut";
            }
            else if (chocoIndex == 2)
            {
                skeletonGraphic.startingAnimation = "4_Cut";
            }
            else if (chocoIndex == 4)
            {
                skeletonGraphic.startingAnimation = "6_Cut";
            }
            else if (chocoIndex == 6)
            {
                skeletonGraphic.startingAnimation = "8_Cut";
            }
            else if (chocoIndex == 8)
            {
                skeletonGraphic.startingAnimation = "10_Cut";
            }
            else if (chocoIndex == 10)
            {
                skeletonGraphic.startingAnimation = "12_Cut";
            }

            skeletonGraphic.AnimationState.SetAnimation(0, skeletonGraphic.startingAnimation, false);

            chocoIndex++;
            timerOn = true;
        }

        if (nextChoco == true)
        {
            SkeletonGraphic skeletonGraphic = choco.GetComponent<SkeletonGraphic>();

            if (chocoIndex == 1)
            {
                skeletonGraphic.startingAnimation = "3_Line";
            }
            else if (chocoIndex == 3)
            {
                skeletonGraphic.startingAnimation = "5_Line";
            }
            else if (chocoIndex == 5)
            {
                skeletonGraphic.startingAnimation = "7_Line";
            }
            else if (chocoIndex == 7)
            {
                skeletonGraphic.startingAnimation = "9_Line";
            }
            else if (chocoIndex == 9)
            {
                skeletonGraphic.startingAnimation = "11_Line";
            }
            else if (chocoIndex == 11)
            {
                skeletonGraphic.startingAnimation = "13_Idle";
            }

            skeletonGraphic.AnimationState.SetAnimation(0, skeletonGraphic.startingAnimation, true);

            chickCheck = true;
            nextChoco = false;
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
                    if (retry == true && gameClear == true)
                    {
                        string getSaveData = JsonConvert.SerializeObject(6);
                        PlayerPrefs.SetString("saveDataKey", getSaveData);
                        SceneManager.LoadSceneAsync("Bakery");
                    }
                    else if (retry == true && gameClear == false)
                    {
                        SceneManager.LoadSceneAsync("Bakery");
                    }
                    else if (gameClear == false)
                    {
                        SceneManager.LoadSceneAsync("Chapter1");
                    }
                    else if (gameClear == true)
                    {
                        string getSaveData = JsonConvert.SerializeObject(6);
                        PlayerPrefs.SetString("saveDataKey", getSaveData);
                        SceneManager.LoadSceneAsync("Chapter1");
                    }
                }
            }
        }
    }

    public void SetChoiceA(int _index)
    {
        choiceA = _index;
    }

    public void SetChoiceB(int _index)
    {
        choiceB = _index;
    }

    public void SetChoiceC(int _index)
    {
        choiceC = _index;
    }


    public int GetChoiceA()
    {
        return choiceA;
    }

    public int GetChoiceB()
    {
        return choiceB;
    }

    public int GetChoiceC()
    {
        return choiceC;
    }

    public int GetNext()
    {
        return next;
    }

    public void SetWhippingIndex(int _number, int _index)
    {
        whipping[_number] = _index;
    }

    public int GetWhippingIndex(int _number)
    {
        return whipping[_number];
    }
}
