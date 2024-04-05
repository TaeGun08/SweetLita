using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestData : MonoBehaviour
{
    private NpcChatManager npcChatManager;
    private QuestManager questManager;
    private MiniGameClearCheck miniGame;

    private Inventory inventory;

    [Header("퀘스트 데이터 설정")]
    [SerializeField, Tooltip("Npc이름과 말풍선 오브젝트")] private GameObject npcNameAndChat;
    [SerializeField, Tooltip("선택 버튼 오브젝트")] private GameObject choiceButton;
    [SerializeField, Tooltip("대화 선택 버튼 오브젝트")] private GameObject talkChoiceButton;
    [SerializeField, Tooltip("퀘스트 수락버튼")] private Button acceptButton;
    [SerializeField, Tooltip("퀘스트 거절버튼")] private Button notAcceptButton;
    private TMP_Text chatWindowText; //Npc의 말을 표시할 텍스트
    private TMP_Text npcNameText; //Npc의 이름을 표시할 텍스트
    private int chatIndex;  //다음 대화를 진행시켜주기 위한 변수
    [SerializeField] private bool playerMoveStop = false; //플레이어의 움직임을 멈추게 하는 변수
    [SerializeField] private Image npcImage;
    [SerializeField] private List<Sprite> npcSprites;

    private void Start()
    {
        npcChatManager = NpcChatManager.Instance;

        questManager = QuestManager.Instance;

        inventory = Inventory.Instance;

        miniGame = MiniGameClearCheck.Instance;

        chatWindowText = npcNameAndChat.transform.Find("ChatWindow/ChatWindowText").GetComponent<TMP_Text>();
        npcNameText = npcNameAndChat.transform.Find("NpcName/NpcNameText").GetComponent<TMP_Text>();

        acceptButton.onClick.AddListener(() =>
        {
            playerMoveStop = false;
            npcNameAndChat.SetActive(false);
            choiceButton.SetActive(false);
            chatIndex = 100;
        });

        notAcceptButton.onClick.AddListener(() =>
        {
            playerMoveStop = false;
            npcNameAndChat.SetActive(false);
            choiceButton.SetActive(false);
            chatIndex = 0;
            questManager.SetCurQuestIndex(0);
        });
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
            npcNameText.text = $"영식이";
            npcImage.sprite = npcSprites[0];

            if (chatIndex == 0)
            {
                questManager.SetCurQuestIndex(100);
                playerMoveStop = true;
                chatIndex++;
                chatWindowText.text = $"너무 어려워요..";
                npcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                chatWindowText.text = $"퍼즐을 맞추고 있는데 못 맞추겠어요, 도와주시나요..?";
                questMiniGame("PuzzleGame", 100);
            }
            else if (chatIndex == 100)
            {
                npcNameAndChat.SetActive(true);
                playerMoveStop = true;
                questMiniGame("PuzzleGame", 100);
            }
            else if (chatIndex == 101)
            {
                questManager.SetCurQuestIndex(0);
                questManager.SetQuestIndex(100);
                playerMoveStop = false;
                chatIndex = 0;
                choiceButton.SetActive(false);
                npcNameAndChat.SetActive(false);
            }
        }
        else if (_npcIndex == 11 && _questIndex == 101 
            && questManager.QuestClearCheck(100) == true 
            && questManager.QuestClearCheck(101) == false)
        {
            talkChoiceButton.SetActive(false);
            npcNameText.text = $"꽃분이";
            npcImage.sprite = npcSprites[1];
            talkChoiceButton.SetActive(false);

            if (chatIndex == 0)
            {
                questManager.SetCurQuestIndex(101);
                playerMoveStop = true;
                chatIndex++;
                chatWindowText.text = $"나 좀 도와줄 수 있어?";
                npcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                chatWindowText.text = $"우유 폭포에서 우유를 가져와야 하는데 손이 부족해, 도와줄래?";
                choiceButton.SetActive(true);
            }
            else if (chatIndex == 100)
            {
                questManager.SetCurQuestIndex(101);
                playerMoveStop = true;
                questClearIndex(100, 1, 100, $"어디가, 도와주기로 했잖아", $"고마워 다음에 도움이 필요하면 나도 도와줄게!");
            }
            else if (chatIndex == 101)
            {
                questManager.SetCurQuestIndex(0);
                questManager.SetQuestIndex(101);
                playerMoveStop = false;
                chatIndex = 0;
                choiceButton.SetActive(false);
                npcNameAndChat.SetActive(false);
            }
        }
        else if (_npcIndex == 10 && _questIndex == 102
            && questManager.QuestClearCheck(101) == true
            && questManager.QuestClearCheck(102) == false)
        {
            talkChoiceButton.SetActive(false);
            npcNameText.text = $"영식이";
            npcImage.sprite = npcSprites[0];

            if (chatIndex == 0)
            {
                questManager.SetCurQuestIndex(102);
                playerMoveStop = true;
                chatIndex++;
                chatWindowText.text = $"레시피에 글자가 비어있네..";
                npcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                chatWindowText.text = $"혹시.., 저 좀 도와주실 수 있나요..?";
                questMiniGame("RecipeGame", 102);
            }
            else if (chatIndex == 100)
            {
                npcNameAndChat.SetActive(true);
                playerMoveStop = true;
                questMiniGame("RecipeGame", 102);
            }
            else if (chatIndex == 101)
            {
                questManager.SetCurQuestIndex(0);
                questManager.SetQuestIndex(102);
                playerMoveStop = false;
                chatIndex = 0;
                choiceButton.SetActive(false);
                npcNameAndChat.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 퀘스트를 통해 미니게임을 실행시키기 위한 함수
    /// </summary>
    /// <param name="_miniGameSceneName"></param>
    /// <param name="_miniGameClearCheck"></param>
    private void questMiniGame(string _miniGameSceneName, int _miniGameClearCheck)
    {
        if (miniGame.GetSaveMiniCheckData(_miniGameClearCheck) == false)
        {
            questManager.SetCurQuestIndex(_miniGameClearCheck);

            acceptButton.onClick.AddListener(() =>
            {
                SceneManager.LoadSceneAsync(_miniGameSceneName);
            });

            choiceButton.SetActive(true);
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

            chatWindowText.text = $"감사합니다..!";
            chatIndex = 101;
        }
    }

    /// <summary>
    /// 퀘스트 아이템이 모두 모이면 퀘스트가 클리어가 되도록 하는 함수
    /// </summary>
    /// <param name="_itemIndex"></param>
    /// <param name="_itemQuantity"></param>
    /// <param name="_addIndex"></param>
    /// <param name="_notClearChat"></param>
    /// <param name="_clearChat"></param>
    private void questClearIndex(int _itemIndex, int _itemQuantity, int _addIndex, string _notClearChat, 
        string _clearChat)
    {
        chatIndex++;
        if (inventory.QuestItemCheck(_itemIndex, _itemQuantity) == false)
        {
            chatWindowText.text = _notClearChat;
        }
        else
        {
            inventory.QuestItem(_itemIndex, _itemQuantity);
            chatWindowText.text = _clearChat;
            questManager.SetQuestIndex(_addIndex);
            chatIndex = 101;
        }

        npcNameAndChat.SetActive(true);
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
