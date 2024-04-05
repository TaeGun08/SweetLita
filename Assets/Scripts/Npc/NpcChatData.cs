using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcChatData : MonoBehaviour
{
    private NpcChatManager npcChatManager;
    private QuestManager questManager;

    [Header("퀘스트 데이터 설정")]
    [SerializeField, Tooltip("Npc이름과 말풍선 오브젝트")] private GameObject NpcNameAndChat;
    [SerializeField, Tooltip("선택 버튼 오브젝트")] private GameObject ChoiceButtons;
    [SerializeField, Tooltip("대화 버튼")] private Button talkButton;
    [SerializeField, Tooltip("퀘스트 버튼")] private Button qeustButton;
    [SerializeField, Tooltip("대화 종료 버튼")] private Button backButton;
    private TMP_Text ChatWindowText; //Npc의 말을 표시할 텍스트
    private TMP_Text NpcNameText; //Npc의 이름을 표시할 텍스트
    private int chatIndex;  //다음 대화를 진행시켜주기 위한 변수
    [SerializeField] private bool playerMoveStop = false; //플레이어의 움직임을 멈추게 하는 변수
    [SerializeField] private Image npcImage;
    [SerializeField] private List<Sprite> npcSprites;

    private void Awake()
    {
        talkButton.onClick.AddListener(() => 
        {
            chatIndex = 100;
            if (npcChatManager.GetNpcIndex() == 10)
            {
                ChatWindowText.text = "저는 영식이라고 해요..";
            }
            else if (npcChatManager.GetNpcIndex() == 11)
            {
                ChatWindowText.text = "오늘도 마을 분위기가 좋네";
            }
            ChoiceButtons.SetActive(false);
        });

        qeustButton.onClick.AddListener(() =>
        {
            npcChatManager.SetPlayerMoveStop(false);
            chatIndex = 0;
            npcChatManager.SetQuestCheck(true);
            if (npcChatManager.GetQuestCheck() == true)
            {
                for (int i = 0; i < npcChatManager.GetNpc().GetQuestIndex().Count; i++)
                {
                    questManager.QuestAccept(npcChatManager.GetNpc().GetNpcIndex(), npcChatManager.GetNpc().GetQuestIndex()[i]);
                }
            }
        });

        backButton.onClick.AddListener(() =>
        {
            npcChatManager.SetPlayerMoveStop(false);
            chatIndex = 0;
            NpcNameAndChat.SetActive(false);
            ChoiceButtons.SetActive(false);
        });

        NpcNameAndChat.SetActive(false);
        ChoiceButtons.SetActive(false);
    }

    private void Start()
    {
        npcChatManager = NpcChatManager.Instance;

        questManager = QuestManager.Instance;

        ChatWindowText = NpcNameAndChat.transform.Find("ChatWindow/ChatWindowText").GetComponent<TMP_Text>();
        NpcNameText = NpcNameAndChat.transform.Find("NpcName/NpcNameText").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        NpcChat();
    }

    /// <summary>
    /// Npc의 대화 데이터를 담을 함수
    /// </summary>
    /// <param name="_npcIndex"></param>
    /// <param name="_questIndex"></param>
    private void NpcChat()
    {
        if (npcChatManager.GetNpcTalkCheck() == true)
        {
            if (npcChatManager.GetNpcIndex() == 10)
            {
                NpcNameText.text = "영식이";
                npcImage.sprite = npcSprites[0];
                
                if (chatIndex == 0)
                {
                    npcChatManager.SetPlayerMoveStop(true);
                    NpcNameAndChat.SetActive(true);
                    chatIndex++;
                    ChatWindowText.text = "안녕하세요...";
                }
                else if (chatIndex == 1)
                {
                    ChoiceButtons.SetActive(true);
                    ChatWindowText.text = "저한테 무슨 볼일이라도...";
                }
                else if (chatIndex == 100)
                {
                    chatIndex++;
                    ChatWindowText.text = "딱히 할 말이 없네요..";
                }
                else if (chatIndex == 101)
                {
                    npcChatManager.SetPlayerMoveStop(false);
                    chatIndex = 0;
                    NpcNameAndChat.SetActive(false);
                    ChoiceButtons.SetActive(false);
                }
            }
            else if (npcChatManager.GetNpcIndex() == 11)
            {
                NpcNameText.text = "꽃분이";
                npcImage.sprite = npcSprites[1];

                if (chatIndex == 0)
                {
                    npcChatManager.SetPlayerMoveStop(true);
                    NpcNameAndChat.SetActive(true);
                    chatIndex++;
                    ChatWindowText.text = "안녕!";
                }
                else if (chatIndex == 1)
                {
                    ChoiceButtons.SetActive(true);
                    ChatWindowText.text = "너가 새로왔다던 그 친구구나?";
                }
                else if (chatIndex == 100)
                {
                    chatIndex++;
                    ChatWindowText.text = "다음에 보자!";
                }
                else if (chatIndex == 101)
                {
                    npcChatManager.SetPlayerMoveStop(false);
                    chatIndex = 0;
                    NpcNameAndChat.SetActive(false);
                    ChoiceButtons.SetActive(false);
                }
            }

            npcChatManager.SetNpcTalkCheck(false);
        }
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
