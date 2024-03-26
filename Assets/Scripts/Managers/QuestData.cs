using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestData : MonoBehaviour
{
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

    private void Start()
    {
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
            if (chatIndex == 0)
            {
                playerMoveStop = true;
                chatIndex++;
                ChatWindowText.text = $"나 좀 도와줘";
                NpcNameText.text = $"김덕영";
                NpcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                ChatWindowText.text = $"옆에 보이는 아이템을 22개만 가져다 줘";
                ChoiceButtons.SetActive(true);
            }
            else if (chatIndex == 100)
            {
                questClearIndex(100, 22, 100, $"아직 못 구해온 거야?", $"고마워! 이제 덕일이한테 가봐!");
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
            if (chatIndex == 0)
            {
                playerMoveStop = true;
                chatIndex++;
                ChatWindowText.text = $"나 좀 도와줘";
                NpcNameText.text = $"김덕일";
                NpcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                ChatWindowText.text = $"옆에 보이는 아이템을 12개만 가져다 줘";
                ChoiceButtons.SetActive(true);
            }
            else if (chatIndex == 100)
            {
                questClearIndex(101, 12, 101, $"아직 못 구해온 거야?", $"고마워! 이제 덕이한테 가봐!");
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
            if (chatIndex == 0)
            {
                playerMoveStop = true;
                chatIndex++;
                ChatWindowText.text = $"나 좀 도와줘";
                NpcNameText.text = $"김덕이";
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
