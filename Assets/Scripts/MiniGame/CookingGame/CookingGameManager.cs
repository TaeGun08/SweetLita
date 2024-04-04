using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class CookingGameManager : MonoBehaviour
{
    public static CookingGameManager Instance;

    [SerializeField] private Transform canvasTrs;
    [SerializeField] private List<GameObject> nodeObj;
    [SerializeField] private RectTransform randomTrs;
    [SerializeField] private GameObject nodeBox;
    [SerializeField] private int nodeIndex;
    [SerializeField] private int playerLife;
    [SerializeField] private int point;
    private bool nodeCreate = false;
    [SerializeField] private float destroyTimer = 0.0f;
    private bool gameStart = false;
    private float gameStartTimer;
    [SerializeField] private TMP_Text startTimeText;
    private bool level1Clear = false;
    private bool level2Clear = false;
    private bool gameClear = false;
    [SerializeField] private GameObject gameClearText;
    [SerializeField] private GameObject gameOverText;

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

        gameStartTimer = 3f;
    }

    private void Start()
    {
        startTimeText.gameObject.SetActive(true);

        gameClearText.SetActive(false);

        gameOverText.SetActive(false);
    }

    private void Update()
    {
        if (gameClear == true)
        {
            Destroy(nodeBox);
            gameClearText.SetActive(true);
            return;
        }
        else if (playerLife == 0)
        {
            Destroy(nodeBox);
            gameOverText.SetActive(true);
            return;
        }
        else if (gameStart == false)
        {
            gameStartTimer -= Time.deltaTime;

            if (gameStartTimer > 1f)
            {
                startTimeText.text = $"{gameStartTimer.ToString("F0")}";
            }

            if (gameStartTimer >= 0.1f && gameStartTimer <= 0.3f)
            {
                startTimeText.text = $"게임 스타트!";
            }
            else if (gameStartTimer <= -1f)
            {
                startTimeText.gameObject.SetActive(false);
                gameStart = true;
                gameStartTimer = 3f;
            }
            return;
        }

        nodeDestroyTimer();
        nodeIndexCheck();
        level1();
        level2();
        level3();
    }

    private void nodeDestroyTimer()
    {
        if (nodeCreate == true)
        {
            destroyTimer += Time.deltaTime;
        }
    }

    private void nodeIndexCheck()
    {
        if (destroyTimer >= 0.8f && destroyTimer <= 1.0f)
        {
            if (nodeIndex == 1)
            {
                keyCheck(true, false, false, false);
            }
            else if (nodeIndex == 2)
            {
                keyCheck(false, true, false, false);
            }
            else if (nodeIndex == 3)
            {
                keyCheck(false, false, true, false);
            }
            else if (nodeIndex == 4)
            {
                keyCheck(false, false, false, true);
            }
        }
        else if (destroyTimer >= 1.2f)
        {
            playerLife--;
            Destroy(nodeBox);
            destroyTimer = 0f;
            nodeCreate = false;
        }
        else
        {
            keyCheck(false, false, false, false);
        }
    }

    private void level1()
    {
        if (level1Clear == false)
        {
            if (point == 5)
            {
                level1Clear = true;
                return;
            }
            createNode();
        }
    }

    private void level2()
    {
        if (level1Clear == true && level2Clear == false)
        {
            if (point == 10)
            {
                level2Clear = true;
                return;
            }
            createNode();
        }
    }

    private void level3()
    {
        if (level2Clear == true && gameClear == false)
        {
            if (point == 15)
            {
                gameClear = true;
                return;
            }
            createNode();
        }
    }

    private void createNode()
    {
        if (nodeCreate == false)
        {
            randomNodeTrs();
            int index = Random.Range(1, 5);
            nodeIndex = index;
            nodeBox = Instantiate(nodeObj[index -1], randomTrs.position, Quaternion.identity, canvasTrs);
            nodeCreate = true;
        }
    }

    private void keyCheck(bool _up, bool _donw, bool _left, bool _right)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_up == true)
            {
                Destroy(nodeBox);
                destroyTimer = 0f;
                point++;
                nodeCreate = false;
            }
            else
            {
                Destroy(nodeBox);
                destroyTimer = 0f;
                playerLife--;
                nodeCreate = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_donw == true)
            {
                Destroy(nodeBox);
                destroyTimer = 0f;
                point++;
                nodeCreate = false;
            }
            else
            {
                Destroy(nodeBox);
                destroyTimer = 0f;
                playerLife--;
                nodeCreate = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_left == true)
            {
                Destroy(nodeBox);
                destroyTimer = 0f;
                point++;
                nodeCreate = false;
            }
            else
            {
                Destroy(nodeBox);
                destroyTimer = 0f;
                playerLife--;
                nodeCreate = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_right == true)
            {
                Destroy(nodeBox);
                destroyTimer = 0f;
                point++;
                nodeCreate = false;
            }
            else
            {
                Destroy(nodeBox);
                destroyTimer = 0f;
                playerLife--;
                nodeCreate = false;
            }
        }
    }

    private void randomNodeTrs()
    {
        float recX = Random.Range(-450f, 450f);
        float recY = Random.Range(-200f, 200f);
        Vector3 recTrs = randomTrs.localPosition;
        recTrs.x = recX;
        recTrs.y = recY;
        randomTrs.localPosition = recTrs;
    }
}
