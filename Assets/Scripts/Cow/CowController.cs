using Newtonsoft.Json;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CowController : MonoBehaviour
{
    private SkeletonAnimation spineAnim;

    [Header("소 유적 게임 설정")]
    [SerializeField, Tooltip("소 얼굴")] private List<GameObject> cowFace;
    [SerializeField] private GameObject faceCheck;
    [SerializeField, Tooltip("햄스터")] private GameObject hamsterObject;
    [Space]
    [SerializeField, Tooltip("게임클리어 텍스트")] private GameObject gameClearText;
    [SerializeField, Tooltip("게임오버 텍스트")] private GameObject gameOverText;
    [SerializeField] private MilkeGet milkeGet;
    [SerializeField] private ClearCheck check;
    [Space]
    [SerializeField] private Image fadeImage;
    private float fadeTimer;
    [SerializeField] private bool fadeCheck = false;
    private bool fadeInOutCheck = false;
    [Space]
    [SerializeField] private GameObject explanationWindow;
    [SerializeField] private Button gameStartButton;
    private bool gameStart = false;
    [Space]
    [SerializeField] private GameObject gameEndObject;
    [SerializeField] private List<Button> buttons;

    private bool retry = false;
    private bool gameClear = false;

    private bool changeFace = false;
    private float changeFaceTimer;
    private float randomTime;

    private float angryCowTimer;

    private bool gameOver;

    private bool moveCheck = false;

    [SerializeField] private TMP_Text gameClearOverText;
    private float textChangeTimer;
    private bool textChanageOn = false;
    private float textStartTimer;
    private bool textStartCheck = false;

    private void Awake()
    {
        gameStartButton.onClick.AddListener(() =>
        {
            explanationWindow.SetActive(false);
            gameStart = true;
        });

        buttons[0].onClick.AddListener(() =>
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
        });

        buttons[1].onClick.AddListener(() =>
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
            retry = true;
        });

        changeFaceTimer = 3f;
        randomTime = changeFaceTimer;

        cowFace[1].SetActive(false);
        cowFace[2].SetActive(false);

        fadeTimer = 2;

        fadeCheck = true;

        fadeInOutCheck = true;

        moveCheck = true;

        textChangeTimer = 3;
        gameClearOverText.text = "";
        gameClearOverText.gameObject.SetActive(true);
    }

    private void Start()
    {
        spineAnim = hamsterObject.GetComponent<SkeletonAnimation>();
    }

    private void Update()
    {
        fadeInOut();

        if (gameStart == true && textChanageOn == false)
        {
            textChangeTimer -= Time.deltaTime;
            gameClearOverText.text = $"{(int)(textChangeTimer + 1)}";
            if (textChangeTimer <= 0)
            {
                gameClearOverText.text = $"";
                textChanageOn = true;
            }
        }
        else if (gameStart == true && textChanageOn == true)
        {
            textStartTimer += Time.deltaTime;

            if (textStartTimer < 1f)
            {
                gameClearOverText.text = $"게임 스타트!";
            }
            else
            {
                gameClearOverText.text = $"";
                gameClearOverText.gameObject.SetActive(false);
                textStartCheck = true;
            }
        }

        if (gameStart == true && textStartCheck == true)
        {
            if (check.ReturnChear() == true && gameClear == false)
            {
                gameClear = true;
                gameClearText.SetActive(true);
                gameEndObject.SetActive(true);
            }
            else if (gameOver == true)
            {
                cowFace[2].SetActive(true);
                gameOverText.SetActive(true);
                gameEndObject.SetActive(true);
            }

            if (gameClear == true || gameOver == true)
            {
                return;
            }

            changeCowFace();
            hamsterMoveCheck();
        }
    }

    /// <summary>
    /// 일정 시간마다 소의 얼굴을 바꿔줌
    /// </summary>
    private void changeCowFace()
    {
        if (changeFace == false)
        {
            changeFaceTimer -= Time.deltaTime;

            if (changeFaceTimer <= randomTime * 0.3f)
            {
                faceCheck.SetActive(true);
            }

            if (changeFaceTimer <= 0)
            {
                changeFace = true;
                cowFace[1].SetActive(true);
                angryCowTimer = 2f;
            }
        }
        else
        {
            if (moveCheck == false && milkeGet.ReturnCheck() == true)
            {
                gameOver = true;
                return;
            }

            angryCowTimer -= Time.deltaTime;

            if (faceCheck.activeSelf == true)
            {
                faceCheck.SetActive(false);
            }

            if (angryCowTimer <= 0)
            {
                cowFace[1].SetActive(false);
                float cowFaceRandomTime = Random.Range(2.0f, 5.0f);
                randomTime = cowFaceRandomTime;
                changeFaceTimer = cowFaceRandomTime;
                changeFace = false;
                moveCheck = true;
            }
        }

        if (milkeGet.ReturnMilkeGet() == true && check.gameObject.activeSelf == false)
        {
            check.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 햄스터가 상황에 맞춰 움직이게 만들어줌
    /// </summary>
    private void hamsterMoveCheck()
    {
        if (changeFace == true && Input.GetKey(KeyCode.RightArrow))
        {
            moveCheck = false;
            return;
        }

        if (milkeGet.ReturnMilkeGet() == false && Input.GetKey(KeyCode.RightArrow))
        {
            spineAnim.AnimationName = "Walk";
            hamsterObject.transform.position += new Vector3(1f, 0f, 0f) * 1f * Time.deltaTime;
        }
        else
        {
            spineAnim.AnimationName = "Idle";
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
                        string getSaveData = JsonConvert.SerializeObject(7);
                        PlayerPrefs.SetString("saveDataKey", getSaveData);
                        SceneManager.LoadSceneAsync("Cow");
                    }
                    else if (retry == true && gameClear == false)
                    {
                        SceneManager.LoadSceneAsync("Cow");
                    }
                    else if (gameClear == false)
                    {
                        SceneManager.LoadSceneAsync("Chapter1");
                    }
                    else if (gameClear == true)
                    {
                        string getSaveData = JsonConvert.SerializeObject(7);
                        PlayerPrefs.SetString("saveDataKey", getSaveData);
                        SceneManager.LoadSceneAsync("Chapter1");
                    }
                }
            }
        }
    }
}
