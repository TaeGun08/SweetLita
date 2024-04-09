using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestData : MonoBehaviour
{
    private GameManager gameManager;
    private QuestManager questManager;
    private MiniGameClearCheck miniGame;
    private NpcChatManager npcChatManager;

    private Inventory inventory;

    [Header("퀘스트 데이터 설정")]
    [SerializeField, Tooltip("Npc이름과 말풍선 오브젝트")] private GameObject npcNameAndChat;
    [SerializeField, Tooltip("선택 버튼 오브젝트")] private GameObject choiceButton;
    [SerializeField, Tooltip("대화 선택 버튼 오브젝트")] private GameObject talkChoiceButton;
    [SerializeField, Tooltip("퀘스트 수락버튼")] private Button acceptButton;
    [SerializeField, Tooltip("퀘스트 거절버튼")] private Button notAcceptButton;
    private TMP_Text chatWindowText; //Npc의 말을 표시할 텍스트
    private TMP_Text npcNameText; //Npc의 이름을 표시할 텍스트
    [SerializeField] private int chatIndex;  //다음 대화를 진행시켜주기 위한 변수
    [SerializeField] private bool playerMoveStop = false; //플레이어의 움직임을 멈추게 하는 변수
    [SerializeField] private Image playerImage;
    [SerializeField] private Image npcImage;
    [SerializeField] private List<Sprite> playerSprites;
    [SerializeField] private List<Sprite> npcBoySprites;
    [SerializeField] private List<Sprite> npcGirlSprites;

    private void Start()
    {
        gameManager = GameManager.Instance;

        questManager = QuestManager.Instance;

        inventory = Inventory.Instance;

        miniGame = MiniGameClearCheck.Instance;

        npcChatManager = NpcChatManager.Instance;

        chatWindowText = npcNameAndChat.transform.Find("ChatWindow/ChatWindowText").GetComponent<TMP_Text>();
        npcNameText = npcNameAndChat.transform.Find("NpcName/NpcNameText").GetComponent<TMP_Text>();

        acceptButton.onClick.AddListener(() =>
        {
            playerMoveStop = false;
            npcNameAndChat.SetActive(false);
            choiceButton.SetActive(false);
            npcChatManager.SetQuestCheck(false);
            chatIndex = 100;
        });

        notAcceptButton.onClick.AddListener(() =>
        {
            playerMoveStop = false;
            npcNameAndChat.SetActive(false);
            choiceButton.SetActive(false);
            npcChatManager.SetQuestCheck(false);
            chatIndex = 0;
            questManager.SetCurQuestIndex(0);
        });

        playerImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// 퀘스트 챕터 1을 담당하는 함수
    /// </summary>
    /// <param name="_npcIndex"></param>
    /// <param name="_questIndex"></param>
    public void NpcQuestChapter1(int _npcIndex, int _questIndex)
    {
        if (_npcIndex == 10 && _questIndex == 100 
            && questManager.QuestClearCheck(100) == false)
        {
            talkChoiceButton.SetActive(false);
            npcNameText.text = $"소년";
            npcImage.sprite = npcBoySprites[4];

            if (chatIndex == 0)
            {
                npcImage.sprite = npcBoySprites[4];
                npcImage.gameObject.SetActive(true);
                questManager.SetCurQuestIndex(100);
                playerMoveStop = true;
                chatIndex++;
                chatWindowText.text = $".....";
                npcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                npcImage.sprite = npcBoySprites[2];
                chatIndex++;
                chatWindowText.text = $".....";
            }
            else if (chatIndex == 2)
            {
                playerImage.gameObject.SetActive(true);
                npcImage.gameObject.SetActive(false);
                chatIndex++;
                chatWindowText.text = $"무슨 일 있어?";
            }
            else if (chatIndex == 3)
            {
                playerImage.gameObject.SetActive(false);
                npcImage.gameObject.SetActive(true);
                chatIndex++;
                chatWindowText.text = $".......퍼즐을";
            }
            else if (chatIndex == 4)
            {
                chatIndex++;
                chatWindowText.text = $"완성 못하겠어...";
            }
            else if (chatIndex == 5)
            {
                playerImage.gameObject.SetActive(true);
                npcImage.gameObject.SetActive(false);
                chatIndex++;
                npcNameText.text = $"리타";
                chatWindowText.text = $"퍼즐?";
            }
            else if (chatIndex == 6)
            {
                playerImage.gameObject.SetActive(false);
                npcImage.gameObject.SetActive(true);
                chatIndex++;
                chatWindowText.text = $"혼자서는 너무 어려운데...";
            }
            else if (chatIndex == 7)
            {
                chatIndex++;
                questMiniGame("PuzzleGame", 100, "너무 어려워..., 다시 도와줄거야?", "...대단해! 덕분에 퍼즐이 완성됐어");
                chatWindowText.text = $"도와줄 수 있어?";
            }
            else if (chatIndex == 100)
            {
                npcImage.sprite = npcBoySprites[0];
                npcNameAndChat.SetActive(true);
                playerMoveStop = true;
                questMiniGame("PuzzleGame", 100, "너무 어려워..., 다시 도와줄거야?", "...대단해! 덕분에 퍼즐이 완성됐어");
            }
            else if (chatIndex == 101)
            {
                npcImage.sprite = npcBoySprites[3];
                questManager.SetCurQuestIndex(0);
                questManager.SetQuestIndex(100);
                playerMoveStop = false;
                chatIndex = 0;
                choiceButton.SetActive(false);
                npcNameAndChat.SetActive(false);
                npcChatManager.SetQuestCheck(false);
            }
        }
        else if (_npcIndex == 11 && _questIndex == 101 
            && questManager.QuestClearCheck(100) == true 
            && questManager.QuestClearCheck(101) == false)
        {
            talkChoiceButton.SetActive(false);
            npcNameText.text = $"소녀";
            npcImage.sprite = npcGirlSprites[6];

            if (chatIndex == 0)
            {
                npcImage.sprite = npcGirlSprites[6];
                npcImage.gameObject.SetActive(true);
                questManager.SetCurQuestIndex(101);
                playerMoveStop = true;
                chatIndex++;
                chatWindowText.text = $"리타님... 큰일났어요....";
                npcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                chatIndex++;
                chatWindowText.text = $"빵집 아주머니께서 치즈가 필요하다고 하셨는데";
            }
            else if (chatIndex == 2)
            {
                playerImage.gameObject.SetActive(true);
                npcImage.gameObject.SetActive(false);
                chatIndex++;
                chatWindowText.text = $"제가 깜빡해버렸어요...";
            }
            else if (chatIndex == 3)
            {
                playerImage.gameObject.SetActive(false);
                npcImage.gameObject.SetActive(true);
                chatIndex++;
                chatWindowText.text = $"도와주실 수 있으신가요...?";
                choiceButton.SetActive(true);
            }
            else if (chatIndex == 100)
            {
                playerMoveStop = true;
                questClearIndex(100, 5, $"치즈가 부족해요...", $"리타님...! 감사해요!!!");
            }
            else if (chatIndex == 101)
            {
                playerMoveStop = false;
                chatIndex--;
                npcNameAndChat.SetActive(false);
                npcChatManager.SetQuestCheck(false);
            }
            else if (chatIndex == 201)
            {
                npcImage.sprite = npcGirlSprites[2];
                questManager.SetCurQuestIndex(0);
                playerMoveStop = false;
                chatIndex = 0;
                questManager.SetQuestIndex(101);
                choiceButton.SetActive(false);
                npcNameAndChat.SetActive(false);
                npcChatManager.SetQuestCheck(false);
            }
        }
        else if (_npcIndex == 10 && _questIndex == 102
            && questManager.QuestClearCheck(101) == true
            && questManager.QuestClearCheck(102) == false)
        {
            talkChoiceButton.SetActive(false);
            npcNameText.text = $"소년";
            npcImage.sprite = npcBoySprites[4];

            if (chatIndex == 0)
            {
                npcImage.sprite = npcBoySprites[4];
                npcImage.gameObject.SetActive(true);
                questManager.SetCurQuestIndex(102);
                playerMoveStop = true;
                chatIndex++;
                chatWindowText.text = $"리타...";
                npcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                chatIndex++;
                chatWindowText.text = $"......도와줘";
            }
            else if (chatIndex == 2)
            {
                playerImage.gameObject.SetActive(true);
                npcImage.gameObject.SetActive(false);
                chatIndex++;
                chatWindowText.text = $"무슨 일이야?";
            }
            else if (chatIndex == 3)
            {
                npcImage.sprite = npcBoySprites[2];
                playerImage.gameObject.SetActive(false);
                npcImage.gameObject.SetActive(true);
                chatIndex++;
                chatWindowText.text = $"물감 놀이를 하다가...";
            }
            else if (chatIndex == 4)
            {
                chatIndex++;
                chatWindowText.text = $"빵집 아주머니 레시피에 쏟아버렸어...";
            }
            else if (chatIndex == 5)
            {
                chatIndex++;
                chatWindowText.text = $"아주머니께 혼날 거 같아....";
            }
            else if (chatIndex == 6)
            {
                chatIndex++;
                questMiniGame("RecipeGame", 102, "혼나고 싶지 않아..., 한번만 더 도와줄 수 있어?", "도와줘서 고마워 리타...!");
                chatWindowText.text = $"도와줘";
            }
            else if (chatIndex == 100)
            {
                npcNameAndChat.SetActive(true);
                playerMoveStop = true;
                questMiniGame("RecipeGame", 102, "혼나고 싶지 않아..., 한번만 더 도와줄 수 있어?", "도와줘서 고마워 리타...!");
            }
            else if (chatIndex == 101)
            {
                npcImage.sprite = npcBoySprites[3];
                questManager.SetCurQuestIndex(0);
                questManager.SetQuestIndex(102);
                playerMoveStop = false;
                chatIndex = 0;
                choiceButton.SetActive(false);
                npcNameAndChat.SetActive(false);
                npcChatManager.SetQuestCheck(false);
            }
        }
    }

    /// <summary>
    /// 퀘스트를 통해 미니게임을 실행시키기 위한 함수
    /// </summary>
    /// <param name="_miniGameSceneName"></param>
    /// <param name="_miniGameClearCheck"></param>
    private void questMiniGame(string _miniGameSceneName, int _miniGameClearCheck, string _talkReTryText, string _clearTalkText)
    {
        if (miniGame.GetSaveMiniCheckData(_miniGameClearCheck) == false)
        {
            questManager.SetCurQuestIndex(_miniGameClearCheck);

            acceptButton.onClick.AddListener(() =>
            {
                SceneManager.LoadSceneAsync(_miniGameSceneName);
            });

            npcImage.sprite = npcBoySprites[2];

            chatWindowText.text = _talkReTryText;

            choiceButton.SetActive(true);

            npcChatManager.SetQuestCheck(false);
        }
        else
        {
            acceptButton.onClick.AddListener(() =>
            {
                playerMoveStop = false;
                npcNameAndChat.SetActive(false);
                choiceButton.SetActive(false);
                chatIndex = 100;
            });

            chatWindowText.text = _clearTalkText;
            chatIndex = 101;
        }

        gameManager.SetSaveCheck(true);
    }

    /// <summary>
    /// 퀘스트 아이템이 모두 모이면 퀘스트가 클리어가 되도록 하는 함수
    /// </summary>
    /// <param name="_itemIndex"></param>
    /// <param name="_itemQuantity"></param>
    /// <param name="_addIndex"></param>
    /// <param name="_notClearChat"></param>
    /// <param name="_clearChat"></param>
    private void questClearIndex(int _itemIndex, int _itemQuantity, string _notClearChat, 
        string _clearChat)
    {
        chatIndex++;
        if (inventory.QuestItemCheck(_itemIndex, _itemQuantity) == false)
        {
            chatWindowText.text = _notClearChat;
        }
        else
        {
            chatIndex = 201;
            inventory.QuestItem(_itemIndex, _itemQuantity);
            chatWindowText.text = _clearChat;
        }

        npcNameAndChat.SetActive(true);

        gameManager.SetSaveCheck(true);
    }

    /// <summary>
    /// 플레이어가 대화도중 움직임을 멈춰주는 함수
    /// </summary>
    /// <returns></returns>
    public bool PlayerMoveStop()
    {
        return playerMoveStop;
    }

    public void SetChatIndex(int _chatIndex)
    {
        chatIndex = _chatIndex;
    }
}
