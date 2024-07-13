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

    [Header("���� ����")]
    [SerializeField, Tooltip("���񺸵�")] private Transform puzzleBoardTrs;
    [SerializeField, Tooltip("��������")] private GameObject pieceObj;
    private bool gameStart = false; //������ �����ߴ��� Ȯ���ϴ� ����
    private List<int> pieceIndex = new List<int>(); //������ �ε����� �����ֱ� ���� ����Ʈ
    private List<GameObject> puzzleCheck = new List<GameObject>(); //������ ������ �־��ֱ� ���� ����Ʈ
    private List<GameObject> pieceObjCheck = new List<GameObject>(); //�÷��̾ ������ �ΰ��� ���� ������ �� ����Ʈ
    private int checkValue; //���� ������ 2�� �̻� �������� �� �ϰ� ���� ����
    private bool gameClear = false; //������ Ŭ���� ������ ���ִ� ����
    private List<bool> clearChecks = new List<bool>(); //Ŭ��� üũ�ϱ� ���� ����, 16���� ��� true�� �� ���� Ŭ����
    private int clearIndex; //Ŭ���� bool ����Ʈ�� Ȯ���� true�� ��ŭ �ε����� �޾ƿ��� ���� ����
    [SerializeField] private GameObject claerTextObj; //Ŭ���� ���� �� �ߴ� �ؽ�Ʈ
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
    /// ������ �����ϴ� �Լ�
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
    /// ������ ���� ���� �Լ�
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
    /// ��ǥ�� �־��ֱ� ���� �Լ�
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
    /// �ε����� �´� �̹����� �־��ֱ� ���� �Լ�
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
    /// ���� ��ġ�� ���� �ٲٱ� ���� �Լ�
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
    /// ������ ������ �´ٸ� ������ Ŭ�������ִ� �Լ�
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
    /// ������ Ŭ�����ϱ� ���� Ŭ���� �ε����� ��½����ִ� �Լ�
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

                if (fadeColor.a >= 1)
                {
                    if (retry == true && gameClear == true)
                    {
                        int saveIndex = 1;
                        string getSaveData = PlayerPrefs.GetString("saveDataKey");
                        int saveData = JsonConvert.DeserializeObject<int>(getSaveData);
                        if (saveIndex >= saveData)
                        {
                            string setSaveData = JsonConvert.SerializeObject(2);
                            PlayerPrefs.SetString("saveDataKey", setSaveData);
                        }

                        SceneManager.LoadSceneAsync("Loading");
                        string setLoding = JsonConvert.SerializeObject("Puzzle");
                        PlayerPrefs.SetString("saveScene", setLoding);
                    }
                    else if (retry == true && gameClear == false)
                    {
                        SceneManager.LoadSceneAsync("Loading");
                        string setLoding = JsonConvert.SerializeObject("Puzzle");
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
                        int saveIndex = 1;
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

    /// <summary>
    /// ���� ������ �־��ִ� �Լ�
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
    /// �÷��̾ ������ ������ �ΰ��� ���� �ʵ��� �����ִ� �Լ�
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
    /// ���� Ŭ���� bool���� �ٸ� ��ũ��Ʈ�� �����ֱ� ���� �Լ�
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
