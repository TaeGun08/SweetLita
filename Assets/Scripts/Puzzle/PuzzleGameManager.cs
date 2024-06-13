using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleGameManager : MonoBehaviour
{
    public static PuzzleGameManager Instance;

    [Header("퍼즐 설정")]
    [SerializeField, Tooltip("퍼즐보드")] private Transform puzzleBoardTrs;
    [SerializeField, Tooltip("퍼즐조각")] private GameObject pieceObj;
    private bool gameStart = false; //게임을 시작했는지 확인하는 변수
    private List<int> pieceIndex = new List<int>(); //퍼즐의 인덱스를 섞어주기 위한 리스트
    private List<GameObject> puzzleCheck = new List<GameObject>(); //생성된 퍼즐을 넣어주기 위한 리스트
    private List<GameObject> pieceObjCheck = new List<GameObject>(); //플레이어가 선택한 두개의 퍼즐 조각이 들어갈 리스트
    private int checkValue; //퍼즐 조각을 2개 이상 선택하지 못 하게 막는 변수
    private bool gameClear = false; //게임을 클리어 했으면 켜주는 변수
    private List<bool> clearChecks = new List<bool>(); //클리어를 체크하기 위한 변수, 16개가 모두 true일 때 게임 클리어
    private int clearIndex; //클리어 bool 리스트를 확인해 true인 만큼 인덱스를 받아오기 위한 변수
    [SerializeField] private GameObject claerTextObj; //클리어 했을 때 뜨는 텍스트
    [Space]
    [SerializeField] private List<Sprite> puzzleSprites;
    [SerializeField] private float gameOverTimer;
    private bool gameOver = false;
    [SerializeField] private Image timerImage;
    [SerializeField] private GameObject gameOverObj;
    [Space]
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeTimer;
    [SerializeField] private bool fadeCheck = false;
    private bool fadeInOutCheck = false;
    [Space]
    [SerializeField] private GameObject explanationWindow;
    [SerializeField] private Button gameStartButton;
    private bool gameStartCheck = false;
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

        Screen.SetResolution(1920, 1080, true);

        for (int i = 0; i < 2; i++)
        {
            pieceObjCheck.Add(null);
        }

        for (int i = 0; i < 16; i++)
        {
            clearChecks.Add(false);
        }

        gameStartButton.onClick.AddListener(() =>
        {
            explanationWindow.SetActive(false);
            gameStartCheck = true;
            vfxAudio.clip = audioClips[0];
            vfxAudio.Play();
        });

        buttons[0].onClick.AddListener(() =>
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
            vfxAudio.clip = audioClips[0];
            vfxAudio.Play();
        });

        buttons[1].onClick.AddListener(() =>
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
            retry = true;
            vfxAudio.clip = audioClips[0];
            vfxAudio.Play();
        });

        claerTextObj.SetActive(false);
        gameOverObj.SetActive(false);

        gameOverTimer = 100f;

        fadeImage.gameObject.SetActive(true);

        fadeTimer = 2;

        fadeCheck = true;

        fadeInOutCheck = true;

        gameStartCheck = false;
        
        textChangeTimer = 3;
    }

    private void Update()
    {
        fadeInOut();

        if (gameStartCheck == true && textChanageOn == false)
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
        else if (gameStartCheck == true && textChanageOn == true)
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

        if (gameStartCheck == true && textStartCheck == true)
        {
            if (gameClear == true)
            {
                if (claerTextObj.activeSelf == false)
                {
                    vfxAudio.clip = audioClips[1];
                    vfxAudio.Play();
                    claerTextObj.SetActive(true);
                }

                gameEndObject.SetActive(true);
                return;
            }

            if (gameOver == false)
            {
                gameOverTimer -= Time.deltaTime;

                timerImage.fillAmount = gameOverTimer / 100f;

                if (gameOverTimer <= 0)
                {
                    audioSource.gameObject.SetActive(false);
                    vfxAudio.clip = audioClips[2];
                    vfxAudio.Play();
                    gameEndObject.SetActive(true);
                    gameOverObj.SetActive(true);
                    gameOver = true;
                }
            }
            else
            {
                return;
            }

            if (clearIndex == 16)
            {
                gameClear = true;
            }

            puzzleCreate();
            piecePosChange();
        }
    }

    /// <summary>
    /// 퍼즐을 생성하는 함수
    /// </summary>
    private void puzzleCreate()
    {
        if (gameStart == false)
        {
            for (int i = 0; i < 16; i++)
            {
                pieceIndex.Add(i);
            }

            randomIndex();

            Vector3 trsPos = Vector3.zero;

            for (int i = 0; i < 16; i++)
            {
                GameObject pieceOb = Instantiate(pieceObj, puzzleBoardTrs);
                Image pieceImage = pieceOb.GetComponent<Image>();
                PuzzlePiece puzzlePieceSc = pieceOb.GetComponent<PuzzlePiece>();
                pieceOb.name = $"Piece{pieceIndex[i]}";
                pieceImage.sprite = setPuzzleSprite(pieceIndex[i]);
                puzzlePieceSc.SetRectTrs(rectPos(i, trsPos));
                trsPos = rectPos(i, trsPos);
                puzzlePieceSc.SetPieceIndex(pieceIndex[i]);
                puzzleCheck.Add(pieceOb);
            }

            gameStart = true;
        }
    }

    /// <summary>
    /// 퍼즐을 섞기 위한 함수
    /// </summary>
    private void randomIndex()
    {
        for (int i = 0; i < 16; i++)
        {
            int radom = Random.Range(1, 17);
            for (int j = 0; j < radom; j++)
            {
                int radomValue = pieceIndex[i];
                pieceIndex[i] = pieceIndex[j];
                pieceIndex[j] = radomValue;
            }
        }
    }

    /// <summary>
    /// 좌표를 넣어주기 위한 함수
    /// </summary>
    /// <param name="_i"></param>
    /// <param name="_trsPos"></param>
    /// <returns></returns>
    private Vector3 rectPos(int _i, Vector3 _trsPos)
    {
        switch (_i)
        {
            case 0:
                _trsPos.x = -300f;
                _trsPos.y = 300f;
                return _trsPos;
            case 1:
            case 2:
            case 3:
                _trsPos.x += 200f;
                _trsPos.y = 300f;
                return _trsPos;
            case 4:
                _trsPos.x = -300f;
                _trsPos.y = 100f;
                return _trsPos;
            case 5:
            case 6:
            case 7:
                _trsPos.x += 200f;
                _trsPos.y = 100f;
                return _trsPos;
            case 8:
                _trsPos.x = -300f;
                _trsPos.y = -100f;
                return _trsPos;
            case 9:
            case 10:
            case 11:
                _trsPos.x += 200f;
                _trsPos.y = -100f;
                return _trsPos;
            case 12:
                _trsPos.x = -300f;
                _trsPos.y = -300f;
                return _trsPos;
            case 13:
            case 14:
            case 15:
                _trsPos.x += 200f;
                _trsPos.y = -300f;
                return _trsPos;
        }

        return Vector3.zero;
    }

    /// <summary>
    /// 인덱스에 맞는 이미지를 넣어주기 위한 함수
    /// </summary>
    /// <param name="_index"></param>
    /// <returns></returns>
    private Sprite setPuzzleSprite(int _index)
    {
        switch (_index)
        {
            case 0:
                return puzzleSprites[0];
            case 1:
                return puzzleSprites[1];
            case 2:
                return puzzleSprites[2];
            case 3:
                return puzzleSprites[3];
            case 4:
                return puzzleSprites[4];
            case 5:
                return puzzleSprites[5];
            case 6:
                return puzzleSprites[6];
            case 7:
                return puzzleSprites[7];
            case 8:
                return puzzleSprites[8];
            case 9:
                return puzzleSprites[9];
            case 10:
                return puzzleSprites[10];
            case 11:
                return puzzleSprites[11];
            case 12:
                return puzzleSprites[12];
            case 13:
                return puzzleSprites[13];
            case 14:
                return puzzleSprites[14];
            case 15:
                return puzzleSprites[15];
        }

        return null;
    }

    /// <summary>
    /// 퍼즐 위치를 서로 바꾸기 위한 함수
    /// </summary>
    private void piecePosChange()
    {
        if (checkValue == 2)
        {
            PuzzlePiece pieceScA = pieceObjCheck[0].GetComponent<PuzzlePiece>();
            PuzzlePiece pieceScB = pieceObjCheck[1].GetComponent<PuzzlePiece>();

            Vector3 piecePos = pieceScA.GetRectTrs().localPosition;
            pieceScA.SetRectTrs(pieceScB.GetRectTrs().localPosition);
            pieceScB.SetRectTrs(piecePos);
            pieceScA.ChangeTrue();
            pieceScB.ChangeTrue();

            GameObject temp = null;

            if (pieceObjCheck != null)
            {
                #region
                int count = puzzleCheck.Count;
                for (int i = 0; i < count; i++)
                {
                    if (puzzleCheck[i] == pieceObjCheck[0])
                    {
                        temp = puzzleCheck[i];

                        for (int j = 0; j < count; j++)
                        {
                            if (puzzleCheck[j] == pieceObjCheck[1])
                            {
                                puzzleCheck[i] = pieceObjCheck[1];
                                puzzleCheck[j] = temp;

                                pieceObjCheck[0] = null;
                                pieceObjCheck[1] = null;

                                checkValue = 0;

                                gameClearCheck();
                                return;
                            }
                        }
                    }
                }
                #endregion
            }
        }
    }

    /// <summary>
    /// 조각의 순서가 맞다면 게임을 클리어해주는 함수
    /// </summary>
    private void gameClearCheck()
    {
        int count = puzzleCheck.Count;
        for (int i = 0; i < count; i++)
        {
            PuzzlePiece pieceSc = puzzleCheck[i].GetComponent<PuzzlePiece>();

            if (pieceSc.PieceIndexCheck() == i)
            {
                clearChecks[i] = true;
            }
            else
            {
                clearChecks[i] = false;
            }
        }

        clearIndex = checkBool();
    }

    /// <summary>
    /// 게임을 클리어하기 위해 클리어 인덱스를 상승시켜주는 함수
    /// </summary>
    /// <returns></returns>
    private int checkBool()
    {
        int clearValue = 0;
        for (int i = 0; i < clearChecks.Count; i++)
        {
            if (clearChecks[i] == true)
            {
                clearValue++;
            }
        }

        return clearValue;
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

                if (fadeColor.a >= 1 && gameClear == true)
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

                        SceneManager.LoadSceneAsync("Puzzle");
                    }
                    else if (retry == true && gameClear == false)
                    {
                        SceneManager.LoadSceneAsync("Puzzle");
                    }
                    else if (gameClear == false)
                    {
                        SceneManager.LoadSceneAsync("Chapter1");
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

                        SceneManager.LoadSceneAsync("Chapter1");
                    }
                }
            }
        }
    }

    /// <summary>
    /// 퍼즐 조각을 넣어주는 함수
    /// </summary>
    /// <param name="_piece"></param>
    public void SetPiece(GameObject _piece)
    {
        int count = pieceObjCheck.Count;
        for (int i = 0; i < count; i++)
        {
            if (pieceObjCheck[i] == null)
            {
                pieceObjCheck[i] = _piece;
                checkValue++;
                return;
            }
        }
    }

    /// <summary>
    /// 플레이어가 선택한 조각이 두개를 넘지 않도록 막아주는 함수
    /// </summary>
    /// <returns></returns>
    public bool PieceObjCheck()
    {
        if (checkValue != 2)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 게임 클리어 bool값을 다른 스크립트에 보내주기 위한 함수
    /// </summary>
    /// <returns></returns>
    public bool GameClear()
    {
        return gameClear;
    }

    public void SetAudio(AudioClip _clip)
    {
        vfxAudio.clip = _clip;
        vfxAudio.Play();
    }
}
