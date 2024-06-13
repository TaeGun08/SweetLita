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

        changeFaceTimer = 3f;
        randomTime = changeFaceTimer;

        cowFace[1].SetActive(false);
        cowFace[2].SetActive(false);

        fadeTimer = 2;

        fadeCheck = true;

        fadeInOutCheck = true;

        moveCheck = true;

        textChangeTimer = 3;
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
            if (check.ReturnChear() == true && gameClear == false && vfxAudio.clip != audioClips[1] && gameEndChecker == false)
            {
                vfxAudio.clip = audioClips[1];
                vfxAudio.Play();
                gameClear = true;
                gameClearText.SetActive(true);
                gameEndObject.SetActive(true);
            }
            else if (gameOver == true && vfxAudio.clip != audioClips[2] && gameEndChecker == false)
            {
                audioSource.gameObject.SetActive(false);
                vfxAudio.clip = audioClips[2];
                vfxAudio.Play();
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

            if (changeFaceTimer <= randomTime * 0.3f && faceCheck.gameObject.activeSelf == false)
            {
                faceCheck.SetActive(true);
                vfxAudio.clip = audioClips[3];
                vfxAudio.Play();
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
                        int saveIndex = 2;
                        string getSaveData = PlayerPrefs.GetString("saveDataKey");
                        int saveData = JsonConvert.DeserializeObject<int>(getSaveData);
                        if (saveIndex >= saveData)
                        {
                            string setSaveData = JsonConvert.SerializeObject(2);
                            PlayerPrefs.SetString("saveDataKey", setSaveData);
                        }

                        SceneManager.LoadSceneAsync("Loading");
                        string setLoding = JsonConvert.SerializeObject("Cow");
                        PlayerPrefs.SetString("saveScene", setLoding);
                    }
                    else if (retry == true && gameClear == false)
                    {
                        SceneManager.LoadSceneAsync("Loading");
                        string setLoding = JsonConvert.SerializeObject("Cow");
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
                        int saveIndex = 2;
                        string getSaveData = PlayerPrefs.GetString("saveDataKey");
                        int saveData = JsonConvert.DeserializeObject<int>(getSaveData);
                        if (saveIndex >= saveData)
                        {
                            string setSaveData = JsonConvert.SerializeObject(2);
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
