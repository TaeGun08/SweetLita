using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RecipeGameManager : MonoBehaviour
{
    public static RecipeGameManager Instance;

    private MiniGameClearCheck miniGame;

    [Header("������ ���� �⺻ ����")]
    [SerializeField, Range(0, 2)] private int gameNumber;
    private bool gameStart = false;
    private bool gameEnd = false;
    [Space]
    [SerializeField] private Image timerBar;
    [SerializeField] private TMP_Text timerText;
    private float gameTimer;
    [Space]
    [SerializeField] private List<GameObject> choiceGame;
    [SerializeField] private Transform backGroundTrs;
    [SerializeField] private RectTransform randomTrs; 
    [SerializeField] private GameObject textOb;
    private GameObject textGetSetObj;
    [Space]
    [SerializeField] private List<string> game1Text;
    [Space]
    [SerializeField] private List<string> game2Text;
    [Space]
    [SerializeField] private List<string> game3Text;
    [Space]
    private int textIndexCheck;
    private int gameClear;
    [SerializeField] private GameObject gameClearObj;
    [SerializeField] private Image fadeImage;
    private float fadeTimer;
    private bool fadeIn = false;

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

        gameNumber = Random.Range(0, 3);
        gameStart = true;
        timerBar.fillAmount = 1f;
        gameTimer = 60f;

        fadeTimer = 1f;

        fadeImage.gameObject.SetActive(true);
    }

    private void Start()
    {
        miniGame = MiniGameClearCheck.Instance;
    }

    private void Update()
    {
        if (fadeIn == false)
        {
            fadeTimer -= Time.deltaTime;

            Color imageColor = fadeImage.color;
            imageColor.a = fadeTimer;
            fadeImage.color = imageColor;

            if (imageColor.a <= 0)
            {
                fadeIn = true;
                fadeTimer = 0;
                fadeImage.gameObject.SetActive(false);
            }
            return;
        }

        if (gameClear == 5)
        {
            if (gameClearObj.activeSelf == false)
            {
                gameClearObj.SetActive(true);
            }

            fadeImage.gameObject.SetActive(true);

            fadeTimer += Time.deltaTime;

            Color imageColor = fadeImage.color;
            imageColor.a = fadeTimer;
            fadeImage.color = imageColor;

            if (imageColor.a >= 1)
            {
                miniGame.SetSaveMiniCheckData(102);
                SceneManager.LoadSceneAsync("DessertVillage");
            }
            return;
        }

        if (gameEnd == true)
        {
            return;
        }

        gameTimer -= Time.deltaTime;
        timerBar.fillAmount = gameTimer / 60;
        timerText.text = $"{gameTimer.ToString("F0")} / 60";

        if (gameTimer <= 0.0f)
        {
            gameEnd = true;
        }

        if (gameStart == true)
        {
            if (gameNumber == 0)
            {
                int index = 10;
                for (int i = 0; i < 5; i++)
                {
                    radomTextObj();
                    GameObject textObj = Instantiate(textOb, randomTrs.position, Quaternion.identity, backGroundTrs);
                    RecipeTextDrag recipeDragSc = textObj.GetComponent<RecipeTextDrag>();
                    recipeDragSc.SetTextValue(index++, game1Text[i]);
                }

                choiceGame[0].SetActive(true);
            }
            else if (gameNumber == 1)
            {
                int index = 20;
                for (int i = 0; i < 5; i++)
                {
                    radomTextObj();
                    GameObject textObj = Instantiate(textOb, randomTrs.position, Quaternion.identity, backGroundTrs);
                    RecipeTextDrag recipeDragSc = textObj.GetComponent<RecipeTextDrag>();
                    recipeDragSc.SetTextValue(index++, game2Text[i]);
                }

                choiceGame[1].SetActive(true);
            }
            else
            {
                int index = 30;
                for (int i = 0; i < 5; i++)
                {
                    radomTextObj();
                    GameObject textObj = Instantiate(textOb, randomTrs.position, Quaternion.identity, backGroundTrs);
                    RecipeTextDrag recipeDragSc = textObj.GetComponent<RecipeTextDrag>();
                    recipeDragSc.SetTextValue(index++, game3Text[i]);
                }

                choiceGame[2].SetActive(true);
            }
            gameStart = false;
        }
    }

    /// <summary>
    /// ���� ��ġ�� �ؽ�Ʈ�� �����ϴ� �Լ�
    /// </summary>
    private void radomTextObj()
    {
        float recX = Random.Range(-450f, -110f);
        float recY = Random.Range(-200f, 90f);
        Vector3 recTrs = randomTrs.localPosition;
        recTrs.x = recX;
        recTrs.y = recY;
        randomTrs.localPosition = recTrs;
    }

    /// <summary>
    /// ������ �ؽ�Ʈ�� �ٽ� ��׶��� ��ġ�� �ű�� ���� �Լ�
    /// </summary>
    public Transform BackGroundTrs()
    {
        return backGroundTrs;
    }

    /// <summary>
    /// ������ �ؽ�Ʈ�� �ε����� �־��ֱ� ���� �Լ�
    /// </summary>
    /// <param name="_textIndexCheck"></param>
    public void SetTextIndex(int _textIndexCheck)
    {
        textIndexCheck = _textIndexCheck;
    }

    /// <summary>
    /// ������ �ؽ�Ʈ�� �ε����� ��ȯ�ϱ� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public int GetTextIndex()
    {
        return textIndexCheck;
    }

    /// <summary>
    /// ���� Ŭ��� ���� �Լ�
    /// </summary>
    public void GameClearCheck()
    {
        gameClear += 1;
    }

    public void SetTextObj(GameObject _textGetSetObj)
    {
        textGetSetObj = _textGetSetObj;
    }

    public GameObject GetTextObj() 
    {
        return textGetSetObj;
    }
}