using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestData : MonoBehaviour
{
    private NpcChatManager npcChatManager;
    private QuestManager questManager;

    private Inventory inventory;

    [Header("퀘스트 데이터 설정")]
    [SerializeField, Tooltip("Npc이름과 말풍선 오브젝트")] private GameObject NpcNameAndChat;
    [SerializeField, Tooltip("선택 버튼 오브젝트")] private GameObject ChoiceButtons;
    [SerializeField, Tooltip("퀘스트 수락버튼")] private Button acceptButton;
    [SerializeField, Tooltip("퀘스트 거절버튼")] private Button notAcceptButton;
    private TMP_Text ChatWindowText; //Npc의 말을 표시할 텍스트
    private TMP_Text NpcNameText; //Npc의 이름을 표시할 텍스트
    private int chatIndex;  //다음 대화를 진행시켜주기 위한 변수
    [SerializeField] private bool playerMoveStop = false; //플레이어의 움직임을 멈추게 하는 변수
    [SerializeField] private Image npcImage;
    [SerializeField] private List<Sprite> npcSprites;

    private void Start()
    {
        npcChatManager = NpcChatManager.Instance;

        questManager = QuestManager.Instance;

        inventory = Inventory.Instance;

        ChatWindowText = NpcNameAndChat.transform.Find("ChatWindow/ChatWindowText").GetComponent<TMP_Text>();
        NpcNameText = NpcNameAndChat.transform.Find("NpcName/NpcNameText").GetComponent<TMP_Text>();

        acceptButton.onClick.AddListener(() =>
        {
            playerMoveStop = false;
            NpcNameAndChat.SetActive(false);
            ChoiceButtons.SetActive(false);
            chatIndex = 100;
        });

        notAcceptButton.onClick.AddListener(() =>
        {
            playerMoveStop = false;
            NpcNameAndChat.SetActive(false);
            ChoiceButtons.SetActive(false);
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
            questManager.SetCurQuestIndex(100);

            if (chatIndex == 0)
            {
                npcImage.sprite = npcSprites[0];
                playerMoveStop = true;
                chatIndex++;
                ChatWindowText.text = $"너무 어려워요..";
                NpcNameText.text = $"영식이";
                NpcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                ChatWindowText.text = $"퍼즐을 맞추고 있는데 못 맞추겠어요, 도와주시나요..?";
                ChoiceButtons.SetActive(true);
            }
            else if (chatIndex == 100)
            {
                questClearIndex(100, 22, 100, $"다시 도와주실 건가요..?", $"고맙습니다..");
            }
            else if (chatIndex == 101)
            {
                chatIndex--;
                NpcNameAndChat.SetActive(false);
            }
        }
        else if (_npcIndex == 11 && _questIndex == 101 
            && questManager.QuestClearCheck(100) == true 
            && questManager.QuestClearCheck(101) == false)
        {
            questManager.SetCurQuestIndex(101);

            if (chatIndex == 0)
            {
                npcImage.sprite = npcSprites[1];
                playerMoveStop = true;
                chatIndex++;
                ChatWindowText.text = $"나 좀 도와줄 수 있어?";
                NpcNameText.text = $"꽃분이";
                NpcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                ChatWindowText.text = $"우유 폭포에서 우유를 가져와야 하는데 손이 부족해, 도와줄래?";
                ChoiceButtons.SetActive(true);
            }
            else if (chatIndex == 100)
            {
                questClearIndex(101, 1, 101, $"어디가, 도와주기로 했잖아", $"고마워 다음에 도움이 필요하면 나도 도와줄게!");
            }
            else if (chatIndex == 101)
            {
                chatIndex--;
                NpcNameAndChat.SetActive(false);
            }
        }
        else if (_npcIndex == 12 && _questIndex == 102
            && questManager.QuestClearCheck(101) == true
            && questManager.QuestClearCheck(102) == false)
        {
            questManager.SetCurQuestIndex(102);

            if (chatIndex == 0)
            {
                npcImage.sprite = npcSprites[0];
                playerMoveStop = true;
                chatIndex++;
                ChatWindowText.text = $"나 좀 도와줘";
                NpcNameText.text = $"영식이";
                NpcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                ChatWindowText.text = $"옆에 보이는 아이템을 6개만 가져다 줘";
                ChoiceButtons.SetActive(true);
            }
            else if (chatIndex == 100)
            {
                questClearIndex(102, 6, 102, $"아직 못 구해온 거야?", $"고마워! 이제 덕삼이한테 가봐!");
            }
            else if (chatIndex == 101)
            {
                playerMoveStop = false;
                chatIndex--;
                NpcNameAndChat.SetActive(false);
            }
        }
        else if (chatIndex == 0)
        {
            playerMoveStop = false;
            chatIndex = 0;
            NpcNameAndChat.SetActive(false);
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
            ChatWindowText.text = _notClearChat;
        }
        else
        {
            inventory.QuestItem(_itemIndex, _itemQuantity);
            ChatWindowText.text = _clearChat;
            questManager.SetQuestIndex(_addIndex);
            chatIndex = 0;
        }

        NpcNameAndChat.SetActive(true);
    }

    /// <summary>
    /// 플레이어가 대화도중 움직임을 멈춰주는 함수
    /// </summary>
    /// <returns></returns>
    public bool PlayerMoveStop()
    {
        return playerMoveStop;
    }
}
