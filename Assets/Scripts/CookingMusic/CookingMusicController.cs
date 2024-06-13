using Newtonsoft.Json;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CookingMusicController : MonoBehaviour
{
    private CookingMusicManager musicManager;

    [Header("쿠킹 뮤직 설정")]
    [SerializeField, Tooltip("제한 시간")] private float musicTime;
    private float timer;
    [SerializeField, Tooltip("목숨")] private int playerHeart;
    [Space]
    [SerializeField] private List<GameObject> heartObject;
    [Space]
    [SerializeField] private Image timerImage;
    [SerializeField] private List<Transform> gridTrs;
    [Space]
    [SerializeField] private List<GameObject> clearOverObject;
    private bool gameClear = false;

    private int nextNode;

    private List<GameObject> nodeObject = new List<GameObject>();
    private List<KeyCode> nodeKey = new List<KeyCode>();

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

        timer = musicTime;

        fadeTimer = 2;

        fadeCheck = true;

        fadeInOutCheck = true;

        textChangeTimer = 3;
    }

    private void Start()
    {
        musicManager = CookingMusicManager.Instance;

        nodeInstantiate();
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
            else if (textChangeTimer  > 1&& textChangeTimer <= 2 && gameStartObject[1].activeSelf == false)
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
            if (gameClear == true && vfxAudio.clip != audioClips[1] && gameEndChecker == false)
            {
                vfxAudio.clip = audioClips[1];
                vfxAudio.Play();
                gameEndObject.SetActive(true);
                clearOverObject[0].gameObject.SetActive(true);
                return;
            }
            else if (playerHeart == 0 && vfxAudio.clip != audioClips[2] && gameEndChecker == false)
            {
                audioSource.gameObject.SetActive(false);
                vfxAudio.clip = audioClips[2];
                vfxAudio.Play();
                gameEndObject.SetActive(true);
                clearOverObject[1].gameObject.SetActive(true);
                return;
            }
            else if (timer <= 0 && vfxAudio.clip != audioClips[2] && gameEndChecker == false)
            {
                audioSource.gameObject.SetActive(false);
                vfxAudio.clip = audioClips[2];
                vfxAudio.Play();
                gameEndObject.SetActive(true);
                clearOverObject[1].gameObject.SetActive(true);
                return;
            }

            if (vfxAudio.clip == audioClips[0] && gameEndChecker == false)
            {
                gameTimer();
                nodeCheck();
            }
        }
    }

    private void gameTimer()
    {
        timer -= Time.deltaTime;
        timerImage.fillAmount = timer / musicTime;
    }

    /// <summary>
    /// 게임이 시작될 때 노드 오브젝트를 생성하게 하는 함수
    /// </summary>
    private void nodeInstantiate()
    {
        for (int iNum = 0; iNum < 20; iNum++)
        {
            int randomNode = Random.Range(0, 4);

            GameObject nodeObj = Instantiate(musicManager.GetNodeObject(randomNode), getGridTrs(iNum));

            nodeObject.Add(nodeObj);

            MusicNode nodeSc = nodeObj.GetComponent<MusicNode>();
            nodeKey.Add(nodeSc.GetKeyCode(randomNode));
        }
    }

    /// <summary>
    /// 노드에 맞게 방향키를 눌렀는지 체크하는 함수
    /// </summary>
    private void nodeCheck()
    {
        if (nextNode > 19)
        {
            gameClear = true;
            return;
        }
        else if (playerHeart == 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            keyCodeCheck(KeyCode.UpArrow);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            keyCodeCheck(KeyCode.DownArrow);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            keyCodeCheck(KeyCode.LeftArrow);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            keyCodeCheck(KeyCode.RightArrow);
        }
    }

    /// <summary>
    /// 받아온 키코드 값이랑 조건식 비교하는 함수
    /// </summary>
    /// <param name="_keyCode"></param>
    private void keyCodeCheck(KeyCode _keyCode)
    {
        if (Input.GetKeyDown(nodeKey[nextNode]) == Input.GetKeyDown(_keyCode))
        {
            SkeletonGraphic spineAnim = nodeObject[nextNode].GetComponent<SkeletonGraphic>();

            nextNode++;

            if (_keyCode == KeyCode.UpArrow)
            {
                spineAnim.startingAnimation = "SUp";
            }
            else if (_keyCode == KeyCode.DownArrow)
            {
                spineAnim.startingAnimation = "SDown";
            }
            else if (_keyCode == KeyCode.LeftArrow)
            {
                spineAnim.startingAnimation = "SLeft";
            }
            else if (_keyCode == KeyCode.RightArrow)
            {
                spineAnim.startingAnimation = "SRight";
            }

            spineAnim.AnimationState.SetAnimation(0, spineAnim.startingAnimation, false);
        }
        else
        {
            SkeletonGraphic spineAnim = nodeObject[nextNode].GetComponent<SkeletonGraphic>();

            if (nodeKey[nextNode] == KeyCode.UpArrow)
            {
                spineAnim.startingAnimation = "FUp";
            }
            else if (nodeKey[nextNode] == KeyCode.DownArrow)
            {
                spineAnim.startingAnimation = "FDown";
            }
            else if (nodeKey[nextNode] == KeyCode.LeftArrow)
            {
                spineAnim.startingAnimation = "FLeft";
            }
            else if (nodeKey[nextNode] == KeyCode.RightArrow)
            {
                spineAnim.startingAnimation = "FRight";
            }

            spineAnim.AnimationState.SetAnimation(0, spineAnim.startingAnimation, false);

            SkeletonGraphic sc = heartObject[--playerHeart].GetComponent<SkeletonGraphic>();

            sc.startingAnimation = "Lita_Life";

            sc.AnimationState.SetAnimation(0, sc.startingAnimation, false);
        }
    }

    private Transform getGridTrs(int _iNum)
    {
        if (_iNum < 6)
        {
            return gridTrs[0];
        }
        else if (_iNum == 6)
        {
            return gridTrs[1];
        }
        else if (_iNum > 6 && _iNum < 13)
        {
            return gridTrs[2];
        }
        else if (_iNum == 13) 
        {
            return gridTrs[3];
        }
        else
        {
            return gridTrs[4];
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
                            string setSaveData = JsonConvert.SerializeObject(3);
                            PlayerPrefs.SetString("saveDataKey", setSaveData);
                        }

                        SceneManager.LoadSceneAsync("Loading");
                        string setLoding = JsonConvert.SerializeObject("CookingMusic");
                        PlayerPrefs.SetString("saveScene", setLoding);
                    }
                    else if (retry == true && gameClear == false)
                    {
                        SceneManager.LoadSceneAsync("Loading");
                        string setLoding = JsonConvert.SerializeObject("CookingMusic");
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
                            string setSaveData = JsonConvert.SerializeObject(3);
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
