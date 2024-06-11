using Newtonsoft.Json;
using Spine.Unity;
using System;
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

    [Space]
    [SerializeField] private List<GameObject> dishChildObject;
    [Space]
    [SerializeField] private GameObject whippingObjectCheck;
    private bool whippingNextCheck = false;
    private float nextWhippingTimer;

    private bool whipCheck = false;
    private float whipTimer;

    private bool whippingEnd = false;

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

            SkeletonGraphic sc = whippingObjectCheck.GetComponent<SkeletonGraphic>();
            sc.startingAnimation = "1";
            sc.AnimationState.SetAnimation(0, sc.startingAnimation, false);
            whippingNextCheck = true;
            whipCheck = true;
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
            dishObject();
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
        if (whippingCheck == 5 && choco.activeSelf == false && whippingEnd == true)
        {
            whippingCheck = 0;
            buttons[1].gameObject.SetActive(false);
            choco.SetActive(true);
            chocoCheck = true;
            return;
        }

        if (whippingNextCheck == true)
        {
            nextWhippingTimer += Time.deltaTime;

            if (nextWhippingTimer >= 4)
            {
                nextWhippingTimer = 0;
                whippingNextCheck = false;
            }
        }

        if (whipCheck == true)
        {
            whipTimer += Time.deltaTime;

            if (whipTimer >= 2)
            {
                if (whippingEnd == false && whippingCheck == 5)
                {
                    whippingEnd = true;
                }
                whipTimer = 0;
                whipCheck = false;
            }
        }

        if (whipping[0] == 1 &&
            whipping[1] == 1 &&
            whipping[2] == 1 &&
            whipping[3] == 1 && whipCheck == false && whippingCheck != 5)
        {
            SkeletonGraphic sc = whippingObjectCheck.GetComponent<SkeletonGraphic>();
            sc.startingAnimation = "2";
            sc.AnimationState.SetAnimation(0, sc.startingAnimation, false);
            whippingCheck++;
            whippingCount.text = $"{whippingCheck}";
            whipping[0] = 0;
            whipping[1] = 0;
            whipping[2] = 0;
            whipping[3] = 0;
            whipCheck = true;
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
                else if (choiceA == 0 &&
                           choiceB == 3 &&
                           choiceC == 8 && gameClear == false)
                {
                    text.text = "게임 클리어!";
                    gameClear = true;
                }
                else if (choiceA == 1 &&
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

        if (choiceA == 0)
        {
            SkeletonGraphic skeletonGraphic = choco.GetComponent<SkeletonGraphic>();

            skeletonGraphic.Skeleton.SetSkin("Skin3");
        }
        else if (choiceA == 1)
        {
            SkeletonGraphic skeletonGraphic = choco.GetComponent<SkeletonGraphic>();

            skeletonGraphic.Skeleton.SetSkin("Skin2");
        }
        else
        {
            SkeletonGraphic skeletonGraphic = choco.GetComponent<SkeletonGraphic>();

            skeletonGraphic.Skeleton.SetSkin("Skin1");
        }


        if (chocoCheck == true && Input.GetKeyDown(KeyCode.Mouse0) && timerOn == false)
        {
            SkeletonGraphic skeletonGraphic = choco.GetComponent<SkeletonGraphic>();

            if (choiceA == 0)
            {
                skeletonGraphic.Skeleton.SetSkin("Skin3");
            }
            else if (choiceA == 1)
            {
                skeletonGraphic.Skeleton.SetSkin("Skin2");
            }
            else
            {
                skeletonGraphic.Skeleton.SetSkin("Skin1");
            }

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

            if (choiceA == 0)
            {
                skeletonGraphic.Skeleton.SetSkin("Skin3");
            }
            else if (choiceA == 1)
            {
                skeletonGraphic.Skeleton.SetSkin("Skin2");
            }
            else
            {
                skeletonGraphic.Skeleton.SetSkin("Skin1");
            }

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

    private void dishObject()
    {
        if (choiceA == 0)
        {
            dishChildObject[0].SetActive(true);
        }
        else if (choiceA == 1)
        {
            dishChildObject[1].SetActive(true);
        }
        else if (choiceA == 2)
        {
            dishChildObject[2].SetActive(true);
        }
        else if (choiceA == -1 &&
            (dishChildObject[0].activeSelf == true ||
            dishChildObject[1].activeSelf == true ||
            dishChildObject[2].activeSelf == true))
        {
            dishChildObject[0].SetActive(false);
            dishChildObject[1].SetActive(false);
            dishChildObject[2].SetActive(false);
        }

        if (choiceB == 3 && dishChildObject[3].activeSelf == false)
        {
            dishChildObject[3].SetActive(true);
        }
        else if (choiceB == 4 && dishChildObject[4].activeSelf == false)
        {
            dishChildObject[4].SetActive(true);
        }
        else if (choiceB == 5 && dishChildObject[5].activeSelf == false)
        {
            dishChildObject[5].SetActive(true);
        }
        else if (choiceB == -1 &&
            (dishChildObject[3].activeSelf == true ||
            dishChildObject[4].activeSelf == true ||
            dishChildObject[5].activeSelf == true))
        {
            dishChildObject[3].SetActive(false);
            dishChildObject[4].SetActive(false);
            dishChildObject[5].SetActive(false);
        }

        if (choiceC == 6 && dishChildObject[6].activeSelf == false)
        {
            dishChildObject[6].SetActive(true);
        }
        else if (choiceC == 7 && dishChildObject[7].activeSelf == false)
        {
            dishChildObject[7].SetActive(true);
        }
        else if (choiceC == 8 && dishChildObject[8].activeSelf == false)
        {
            dishChildObject[8].SetActive(true);
        }
        else if (choiceC == -1 && 
            (dishChildObject[6].activeSelf == true || 
            dishChildObject[7].activeSelf == true || 
            dishChildObject[8].activeSelf == true))
        {
            dishChildObject[6].SetActive(false);
            dishChildObject[7].SetActive(false);
            dishChildObject[8].SetActive(false);
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
