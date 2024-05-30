using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private int nextNode;

    private List<GameObject> nodeObject = new List<GameObject>();
    private List<KeyCode> nodeKey = new List<KeyCode>();

    private void Awake()
    {
        timer = musicTime;
    }

    private void Start()
    {
        musicManager = CookingMusicManager.Instance;

        nodeInstantiate();
    }

    private void Update()
    {
        gameTimer();
        nodeCheck();
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
        int createNodeNumber = 0;

        Vector3 trsPos = new Vector3(500, 800, 0);

        for (int i = 0; i < 23; i++)
        {
            int randomNode = Random.Range(0, 4);

            GameObject nodeObj = Instantiate(musicManager.GetNodeObject(), 
                nodeVec(createNodeNumber, trsPos), Quaternion.identity, musicManager.GetCanvas().transform);
            nodeObject.Add(nodeObj);

            MusicNode nodeSc = nodeObj.GetComponent<MusicNode>();
            nodeKey.Add(nodeSc.GetKeyCode(randomNode));

            trsPos = nodeVec(createNodeNumber, trsPos);

            createNodeNumber++;
        }
    }

    /// <summary>
    /// 노드에 맞게 방향키를 눌렀는지 체크하는 함수
    /// </summary>
    private void nodeCheck()
    {
        if (nextNode > 22 || playerHeart == 0)
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
            nodeObject[nextNode].SetActive(false);
            nextNode++;
        }
        else
        {
            for (int i = 0; i < 23; i++)
            {
                nodeObject[i].SetActive(true);
            }

            nextNode = 0;
            heartObject[--playerHeart].SetActive(false);
        }
    }

    /// <summary>
    /// 노드의 위치를 받아오기 위한 함수
    /// </summary>
    /// <param name="_number"></param>
    /// <param name="_trsPos"></param>
    /// <returns></returns>
    private Vector3 nodeVec(int _number, Vector3 _trsPos)
    {
        switch (_number)
        {
            case 0:
                return _trsPos;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                return new Vector3(_trsPos.x + 150f, _trsPos.y, 0);
            case 7:
                return new Vector3(_trsPos.x, _trsPos.y - 150f, 0);
            case 8:
                return new Vector3(_trsPos.x, _trsPos.y - 150f, 0);
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
                return new Vector3(_trsPos.x - 150f, _trsPos.y, 0);
            case 15:
                return new Vector3(_trsPos.x, _trsPos.y - 150f, 0);
            case 16:
                return new Vector3(_trsPos.x, _trsPos.y - 150f, 0);
            case 17:
            case 18:
            case 19:
            case 20:
            case 21:
            case 22:
                return new Vector3(_trsPos.x + 150f, _trsPos.y, 0);
        }

        return new Vector3(0, 0, 0);
    }
}
