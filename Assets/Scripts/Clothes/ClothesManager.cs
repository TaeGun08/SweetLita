using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClothesManager : MonoBehaviour
{
    public static ClothesManager Instance;

    [SerializeField] private Canvas canvas;

    [SerializeField] private List<UIDrop> drop;

    [SerializeField] private List<int> type = new List<int>();
    [SerializeField] private List<int> index = new List<int>();

    [Space]
    [SerializeField] private GameObject explanationWindow;
    [SerializeField] private Button gameStartButton;
    private bool gameStart = false;
    [Space]
    [SerializeField] private Image fadeImage;
    private float fadeTimer;
    [SerializeField] private bool fadeCheck = false;
    private bool fadeInOutCheck = false;

    private int randomNumber;
    private bool randomCheck = false;

    [SerializeField] private int score;

    [Space]
    [SerializeField] private List<Button> buttons;
    [SerializeField] private GameObject scoreObject;
    [SerializeField] private TMP_Text scoreText;

    private bool retry = false;
    private bool gameClear = false;

    [SerializeField] private TMP_Text choiceText;
    [Space]
    [SerializeField] private Image characterImage;
    [SerializeField] private List<Sprite> characterSprite;

    [SerializeField] private GameObject choiceImage;
    [SerializeField] private TMP_Text choiceImageText;

    private float nextTimer;
    private bool timerOff = false;
    private bool nextCheck = false;

    [SerializeField] private List<GameObject> clearOverObject;
    [SerializeField] private GameObject cloudObject;
    [SerializeField] private TMP_Text checkText;

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

        gameStartButton.onClick.AddListener(() =>
        {
            explanationWindow.SetActive(false);
            gameStart = true;
        });

        buttonCheck();

        for (int iNum = 0; iNum < 4; iNum++)
        {
            type.Add(-1);
            index.Add(-1);
        }

        fadeTimer = 2;

        fadeCheck = true;

        fadeInOutCheck = true;
    }

    private void Update()
    {
        fadeInOut();

        if (gameStart == true)
        {
            gameStartCheck();
            dropCheck();

            if (nextCheck == true && timerOff == false)
            {
                nextTimer += Time.deltaTime;

                if (nextTimer >= 3f)
                {
                    choiceImage.SetActive(false);
                    timerOff = true;
                }
            }
        }
    }

    private void gameStartCheck()
    {
        if (randomCheck == false)
        {
            randomNumber = Random.Range(0, 3);

            if (randomNumber == 0)
            {
                choiceText.text = "외출에 알맞는 의상을 골라줘!";
                choiceImageText.text = "오늘은 외출을 할생각이야!";
            }
            else if (randomNumber == 1)
            {
                choiceText.text = "운동에 알맞는 의상을 골라줘!";
                choiceImageText.text = "오후에 운동을 하러 가는 날이야!";
            }
            else
            {
                choiceText.text = "학교에 알맞는 의상을 골라줘!";
                choiceImageText.text = "오늘은 학교에 가야해!";
            }
            randomCheck = true;
        }

        if (type[0] != -1 &&
            type[1] != -1 &&
            type[2] != -1 &&
            type[3] != -1 &&
            buttons[2].gameObject.activeSelf == false)
        {
            buttons[2].gameObject.SetActive(true);
        }
        else if (type[0] == -1 ||
            type[1] == -1 ||
            type[2] == -1 ||
            type[3] == -1 ||
            buttons[2].gameObject.activeSelf == false)
        {
            buttons[2].gameObject.SetActive(false);
        }

        if (score >= 80 && gameClear == false)
        {
            gameClear = true;
        }
        else if (score <= 79 && scoreObject.activeSelf == true)
        {
            characterImage.sprite = characterSprite[1];
        }

        nextCheck = true;
    }

    private void dropCheck()
    {
        if (type[1] == 1 && index[1] == 0 && drop[2].GetCheck() == false)
        {
            drop[2].SetCheck(true);
            type[2] = 99;
            index[2] = 99;
        }
        else if (type[1] == -1 && index[1] == -1 && drop[2].GetCheck() == true)
        {
            drop[2].SetCheck(false);
            type[2] = -1;
            index[2] = -1;
        }
    }

    private void buttonCheck()
    {
        buttons[0].onClick.AddListener(() =>
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
            fadeImage.transform.SetAsLastSibling();
        });

        buttons[1].onClick.AddListener(() =>
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
            fadeImage.transform.SetAsLastSibling();
            retry = true;
        });

        buttons[2].onClick.AddListener(() =>
        {
            type[0] = -1;
            buttons[0].gameObject.SetActive(true);
            buttons[1].gameObject.SetActive(true);
            buttons[2].gameObject.SetActive(false);
            scoreObject.SetActive(true);
            scoreText.text = $"{score}점";

            cloudObject.SetActive(true);
            cloudObject.transform.SetAsLastSibling();

            if (score <= 79)
            {
                clearOverObject[1].SetActive(true);
                clearOverObject[1].transform.SetAsLastSibling();
                checkText.text = "마음에 들지않아..";
            }
            else
            {
                clearOverObject[0].SetActive(true);
                clearOverObject[0].transform.SetAsLastSibling();
                checkText.text = "고마워!";
            }
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
                    if (retry == true && gameClear == true)
                    {
                        string getSaveData = JsonConvert.SerializeObject(5);
                        PlayerPrefs.SetString("saveDataKey", getSaveData);
                        SceneManager.LoadSceneAsync("Clothes");
                    }
                    else if (retry == true && gameClear == false)
                    {
                        SceneManager.LoadSceneAsync("Clothes");
                    }
                    else if (gameClear == false)
                    {
                        SceneManager.LoadSceneAsync("Chapter1");
                    }
                    else if (gameClear == true)
                    {
                        string getSaveData = JsonConvert.SerializeObject(5);
                        PlayerPrefs.SetString("saveDataKey", getSaveData);
                        SceneManager.LoadSceneAsync("Chapter1");
                    }
                }
            }
        }
    }

    public Canvas GetCanvas()
    {
        return canvas;
    }

    public int GameStart()
    {
        return randomNumber;
    }

    public void SetIndexCheck(int _number, int _index)
    {
        index[_number] = _index;
    }

    public void SetTypeCheck(int _number, int _type)
    {
        type[_number] = _type;
    }

    public void SetScore(int _score)
    {
        score += _score;
    }
}
