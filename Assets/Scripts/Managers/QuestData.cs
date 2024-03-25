using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestData : MonoBehaviour
{
    private QuestManager questManager;

    private Inventory inventory;

    [SerializeField] private GameObject NpcNameAndChat;
    [SerializeField] private GameObject ChoiceButtons;
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button notAcceptButton;
    private TMP_Text ChatWindowText;
    private TMP_Text NpcNameText;
    [SerializeField] private int questIndex;
    [SerializeField] private int questCheckIndex;
    [SerializeField] private int checkIndex;
    [SerializeField] private int chatIndex;

    private void Start()
    {
        questManager = QuestManager.Instance;

        inventory = Inventory.Instance;

        ChatWindowText = NpcNameAndChat.transform.Find("ChatWindow/ChatWindowText").GetComponent<TMP_Text>();
        NpcNameText = NpcNameAndChat.transform.Find("NpcName/NpcNameText").GetComponent<TMP_Text>();

        acceptButton.onClick.AddListener(() =>
        {
            NpcNameAndChat.SetActive(false);
            ChoiceButtons.SetActive(false);
            chatIndex = 100;
            questManager.SetQuestCheckIndex(checkIndex);
        });

        notAcceptButton.onClick.AddListener(() =>
        {
            NpcNameAndChat.SetActive(false);
            ChoiceButtons.SetActive(false);
            chatIndex = 0;
        });
    }

    public void NpcQuestChapter1(int _npcIndex, int _questIndex)
    {
        if (_npcIndex == 10 && _questIndex == 100 
            && questManager.QuestClearCheck(100) == false)
        {
            if (chatIndex == 0)
            {
                chatIndex++;
                ChatWindowText.text = $"나 좀 도와줘";
                NpcNameText.text = $"김덕영";
                NpcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                checkIndex = 100;
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
                chatIndex++;
                ChatWindowText.text = $"나 좀 도와줘";
                NpcNameText.text = $"김덕일";
                NpcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                checkIndex = 101;
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
                chatIndex++;
                ChatWindowText.text = $"나 좀 도와줘";
                NpcNameText.text = $"김덕이";
                NpcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                checkIndex = 102;
                ChatWindowText.text = $"옆에 보이는 아이템을 6개만 가져다 줘";
                ChoiceButtons.SetActive(true);
            }
            else if (chatIndex == 100)
            {
                questClearIndex(102, 6, 102, $"아직 못 구해온 거야?", $"고마워! 이제 덕삼이한테 가봐!");
            }
            else if (chatIndex == 101)
            {
                chatIndex--;
                NpcNameAndChat.SetActive(false);
            }
        }
        else if (chatIndex == 0)
        {
            chatIndex = 0;
            NpcNameAndChat.SetActive(false);
        }
    }

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
}
