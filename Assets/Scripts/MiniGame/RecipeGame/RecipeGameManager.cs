using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeGameManager : MonoBehaviour
{
    public static RecipeGameManager Instance;

    [Header("레시피 게임 기본 설정")]
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

        gameNumber = Random.Range(0, 2);
        gameStart = true;
        timerBar.fillAmount = 1f;
        gameTimer = 60f;
    }

    private void Update()
    {
        if (gameClear == 5)
        {
            if (gameClearObj.activeSelf == false)
            {
                gameClearObj.SetActive(true);
            }
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
    /// 랜덤 위치에 텍스트를 생성하는 함수
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
    /// 레시피 텍스트를 다시 백그라운드 위치로 옮기기 위한 함수
    /// </summary>
    public Transform BackGroundTrs()
    {
        return backGroundTrs;
    }

    /// <summary>
    /// 레시피 텍스트의 인덱스를 넣어주기 위한 함수
    /// </summary>
    /// <param name="_textIndexCheck"></param>
    public void SetTextIndex(int _textIndexCheck)
    {
        textIndexCheck = _textIndexCheck;
    }

    /// <summary>
    /// 레시피 텍스트의 인덱스를 반환하기 위한 함수
    /// </summary>
    /// <returns></returns>
    public int GetTextIndex()
    {
        return textIndexCheck;
    }

    /// <summary>
    /// 게임 클리어를 위한 함수
    /// </summary>
    public void GameClearCheck()
    {
        gameClear += 1;
    }
}
