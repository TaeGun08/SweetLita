using Newtonsoft.Json;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StudyController : MonoBehaviour
{
    [Header("공부 게임 설정")]
    [SerializeField] private List<Button> answerButtons;
    [SerializeField] private Image timerImage;
    [Space]
    [SerializeField] private List<GameObject> lifeImage;
    [Space]
    [SerializeField] private List<GameObject> clearTextAndOverText;
    [Space]
    [SerializeField] private TMP_Text roundText;
    [Space]
    [SerializeField] private List<ChangeAnswer> changeAnswers;

    private int life = 3;

    private float timer;

    private int answerInt;

    private int round;

    private bool check = false;

    private int randomA;
    private int randomB;
    private int randomC;

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
    [Space]
    [SerializeField] private List<GameObject> gameStartObject;
    private float textChangeTimer;
    private bool textChanageOn = false;
    private float textStartTimer;
    private bool textStartCheck = false;
    [Space]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource vfxAudio;
    [SerializeField] private List<AudioClip> audioClips;

    private bool gameEndChecker = false;

    private void Awake()
    {
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
            gameEndChecker = true;
        });

        buttons[1].onClick.AddListener(() =>
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
            retry = true;
            vfxAudio.clip = audioClips[0];
            vfxAudio.Play();
            gameEndChecker = true;
        });

        timer = 80f;

        buttonCheck();

        fadeTimer = 2;

        fadeCheck = true;

        fadeInOutCheck = true;

        textChangeTimer = 3;
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

        if (gameStart == true && textStartCheck == true && gameEndChecker == false)
        {
            if (round == 3)
            {
                if (vfxAudio.clip != audioClips[1])
                {
                    vfxAudio.clip = audioClips[1];
                    vfxAudio.Play();
                }
                gameEndObject.SetActive(true);
                clearTextAndOverText[0].SetActive(true);
                gameClear = true;
                return;
            }
            else if (life == 0 || timer <= 0)
            {
                if (vfxAudio.clip != audioClips[2])
                {
                    audioSource.gameObject.SetActive(false);
                    vfxAudio.clip = audioClips[2];
                    vfxAudio.Play();
                }
                gameEndObject.SetActive(true);
                clearTextAndOverText[1].SetActive(true);
                return;
            }

            if (round == 0)
            {
                roundText.text = $"1 라운드";
            }
            else if (round == 1)
            {
                roundText.text = $"2 라운드";
            }
            else
            {
                roundText.text = $"마지막 라운드";
            }

            gameStartCheck();
        }
    }

    private void gameStartCheck()
    {
        timer -= Time.deltaTime;
        timerImage.fillAmount = timer / 80f;

        if (check == false)
        {
            randomA = Random.Range(0, 10);
            randomC = Random.Range(0, 3);
            answerCheck(randomA, randomC);
            int buttonRandom = Random.Range(0, 3);

            if (randomC == 0)
            {
                buttonCheck(buttonRandom, randomA + randomB);

                changeAnswers[0].SetSpriteNumber(randomA);
                changeAnswers[1].SetSpriteNumber(0);
                changeAnswers[2].SetSpriteNumber(randomB);

                answerInt = randomA + randomB;
            }
            else if (randomC == 1)
            {
                buttonCheck(buttonRandom, randomA - randomB);

                changeAnswers[0].SetSpriteNumber(randomA);
                changeAnswers[1].SetSpriteNumber(1);
                changeAnswers[2].SetSpriteNumber(randomB);

                answerInt = randomA - randomB;
            }
            else if (randomC == 2)
            {
                buttonCheck(buttonRandom, randomA * randomB);

                changeAnswers[0].SetSpriteNumber(randomA);
                changeAnswers[1].SetSpriteNumber(2);
                changeAnswers[2].SetSpriteNumber(randomB);

                answerInt = randomA * randomB;
            }
            else if (randomC == 3)
            {
                buttonCheck(buttonRandom, randomA / randomB);

                changeAnswers[0].SetSpriteNumber(randomA);
                changeAnswers[1].SetSpriteNumber(3);
                changeAnswers[2].SetSpriteNumber(randomB);

                answerInt = randomA / randomB;
            }

            check = true;
        }
    }

    private void answerCheck(int _number, int _randomC)
    {
        if (_randomC == 0)
        {
            #region
            if (_number == 0)
            {
                randomB = Random.Range(0, 10);
            }
            else if (_number == 1)
            {
                randomB = Random.Range(0, 9);
            }
            else if (_number == 2)
            {
                randomB = Random.Range(0, 8);
            }
            else if (_number == 3)
            {
                randomB = Random.Range(0, 7);
            }
            else if (_number == 4)
            {
                randomB = Random.Range(0, 6);
            }
            else if (_number == 5)
            {
                randomB = Random.Range(0, 5);
            }
            else if (_number == 6)
            {
                randomB = Random.Range(0, 4);
            }
            else if (_number == 7)
            {
                randomB = Random.Range(0, 3);
            }
            else if (_number == 8)
            {
                randomB = Random.Range(0, 2);
            }
            else if (_number == 9)
            {
                randomB = Random.Range(0, 1);
            }
            #endregion
        }
        else if (_randomC == 1)
        {
            randomB = Random.Range(0, _number);
        }
        else if (_randomC == 2)
        {
            #region
            if (_number == 0)
            {
                randomB = Random.Range(0, 10);
            }
            else if (_number == 1)
            {
                randomB = Random.Range(0, 10);
            }
            else if (_number == 2)
            {
                randomB = Random.Range(0, 5);
            }
            else if (_number == 3)
            {
                randomB = Random.Range(0, 4);
            }
            else if (_number == 4)
            {
                randomB = Random.Range(0, 3);
            }
            else if (_number == 5)
            {
                randomB = Random.Range(0, 2);
            }
            else if (_number == 6)
            {
                randomB = Random.Range(0, 2);
            }
            else if (_number == 7)
            {
                randomB = Random.Range(0, 2);
            }
            else if (_number == 8)
            {
                randomB = Random.Range(0, 2);
            }
            else if (_number == 9)
            {
                randomB = Random.Range(0, 2);
            }
            #endregion
        }
        else
        {
            randomB = Random.Range(1, _number);
        }
    }

    private void buttonCheck(int _check, int _number)
    {
        if (_check == 0)
        {
            int ranA = Random.Range(0, 10);
            int ranB = Random.Range(0, 10);

            #region
            if (_number == 0)
            {
                ranA = Random.Range(1, 5);
                ranB = Random.Range(5, 10);
            }
            else if (_number == 9)
            {
                ranA = Random.Range(5, 9);
                ranB = Random.Range(0, 5);
            }
            else
            {
                ranA = Random.Range(1, 5);
                ranB = Random.Range(5, 10);

                if (ranA == _number)
                {
                    ranA--;
                    if (ranA < 0)
                    {
                        ranA = 2;
                    }
                }

                if (ranB == _number)
                {
                    ranB++;

                    if (ranB > 9)
                    {
                        ranB = 8;
                    }

                    if (ranA == ranB)
                    {
                        ranB -= 2;

                        if (ranB < 0)
                        {
                            ranB += 5;
                        }
                    }
                }
            }
            #endregion

            Answer scA = answerButtons[0].GetComponent<Answer>();
            scA.SetCheck(_number);
            scA.SetSpriteNumber(_number);

            Answer scB = answerButtons[1].GetComponent<Answer>();
            scB.SetCheck(ranA);
            scB.SetSpriteNumber(ranA);

            Answer scC = answerButtons[2].GetComponent<Answer>();
            scC.SetCheck(ranB);
            scC.SetSpriteNumber(ranB);
        }
        else if (_check == 1)
        {
            int ranA = Random.Range(0, 10);
            int ranB = Random.Range(0, 10);
            #region
            if (_number == 0)
            {
                ranA = Random.Range(1, 5);
                ranB = Random.Range(5, 10);
            }
            else if (_number == 9)
            {
                ranA = Random.Range(5, 9);
                ranB = Random.Range(0, 5);
            }
            else
            {
                ranA = Random.Range(1, 5);
                ranB = Random.Range(5, 10);

                if (ranA == _number)
                {
                    ranA--;
                    if (ranA < 0)
                    {
                        ranA = 2;
                    }
                }

                if (ranB == _number)
                {
                    ranB++;

                    if (ranB > 9)
                    {
                        ranB = 8;
                    }

                    if (ranA == ranB)
                    {
                        ranB -= 2;

                        if (ranB < 0)
                        {
                            ranB += 5;
                        }
                    }
                }
            }
            #endregion

            Answer scA = answerButtons[0].GetComponent<Answer>();
            scA.SetCheck(ranA);
            scA.SetSpriteNumber(ranA);

            Answer scB = answerButtons[1].GetComponent<Answer>();
            scB.SetCheck(_number);
            scB.SetSpriteNumber(_number);

            Answer scC = answerButtons[2].GetComponent<Answer>();
            scC.SetCheck(ranB);
            scC.SetSpriteNumber(ranB);
        }
        else
        {
            int ranA = Random.Range(0, 10);
            int ranB = Random.Range(0, 10);

            #region
            if (_number == 0)
            {
                ranA = Random.Range(1, 5);
                ranB = Random.Range(5, 10);
            }
            else if (_number == 9)
            {
                ranA = Random.Range(5, 9);
                ranB = Random.Range(0, 5);
            }
            else
            {
                ranA = Random.Range(1, 5);
                ranB = Random.Range(5, 10);

                if (ranA == _number)
                {
                    ranA--;
                    if (ranA < 0)
                    {
                        ranA = 2;
                    }
                }

                if (ranB == _number)
                {
                    ranB++;

                    if (ranB > 9)
                    {
                        ranB = 8;
                    }

                    if (ranA == ranB)
                    {
                        ranB -= 2;

                        if (ranB < 0)
                        {
                            ranB += 5;
                        }
                    }
                }
            }
            #endregion

            Answer scA = answerButtons[0].GetComponent<Answer>();
            scA.SetCheck(ranA);
            scA.SetSpriteNumber(ranA);

            Answer scB = answerButtons[1].GetComponent<Answer>();
            scB.SetCheck(ranB);
            scB.SetSpriteNumber(ranB);

            Answer scC = answerButtons[2].GetComponent<Answer>();
            scC.SetCheck(_number);
            scC.SetSpriteNumber(_number);
        }
    }

    private void buttonCheck()
    {
        answerButtons[0].onClick.AddListener(() =>
        {
            vfxAudio.clip = audioClips[0];
            vfxAudio.Play();

            Answer sc = answerButtons[0].GetComponent<Answer>();

            SkeletonGraphic spine = lifeImage[life - 1].GetComponent<SkeletonGraphic>();

            if (answerInt == sc.Check())
            {
                round++;
                check = false;
                timer = 80f;
            }
            else
            {
                life--;
                spine.startingAnimation = "Break";

                spine.AnimationState.SetAnimation(0, spine.startingAnimation, false);
            }
        });

        answerButtons[1].onClick.AddListener(() =>
        {
            vfxAudio.clip = audioClips[0];
            vfxAudio.Play();

            Answer sc = answerButtons[1].GetComponent<Answer>();

            SkeletonGraphic spine = lifeImage[life - 1].GetComponent<SkeletonGraphic>();

            if (answerInt == sc.Check())
            {
                round++;
                check = false;
                timer = 80f;
            }
            else
            {
                life--;
                spine.startingAnimation = "Break";

                spine.AnimationState.SetAnimation(0, spine.startingAnimation, false);
            }
        });

        answerButtons[2].onClick.AddListener(() =>
        {
            vfxAudio.clip = audioClips[0];
            vfxAudio.Play();

            Answer sc = answerButtons[2].GetComponent<Answer>();

            SkeletonGraphic spine = lifeImage[life - 1].GetComponent<SkeletonGraphic>();

            if (answerInt == sc.Check())
            {
                round++;
                check = false;
                timer = 80f;
            }
            else
            {
                life--;
                spine.startingAnimation = "Break";

                spine.AnimationState.SetAnimation(0, spine.startingAnimation, false);
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
                        int saveIndex = 7;
                        string getSaveData = PlayerPrefs.GetString("saveDataKey");
                        int saveData = JsonConvert.DeserializeObject<int>(getSaveData);
                        if (saveIndex >= saveData)
                        {
                            string setSaveData = JsonConvert.SerializeObject(8);
                            PlayerPrefs.SetString("saveDataKey", setSaveData);
                        }

                        SceneManager.LoadSceneAsync("Loading");
                        string setLoding = JsonConvert.SerializeObject("Study");
                        PlayerPrefs.SetString("saveScene", setLoding);
                    }
                    else if (retry == true && gameClear == false)
                    {
                        SceneManager.LoadSceneAsync("Loading");
                        string setLoding = JsonConvert.SerializeObject("Study");
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
                        int saveIndex = 7;
                        string getSaveData = PlayerPrefs.GetString("saveDataKey");
                        int saveData = JsonConvert.DeserializeObject<int>(getSaveData);
                        if (saveIndex >= saveData)
                        {
                            string setSaveData = JsonConvert.SerializeObject(8);
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
