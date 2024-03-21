using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("퀘스트 데이터")]
    [SerializeField] private QuestData questData;

    [Header("메인 퀘스트")]
    [SerializeField] private int mainQuestIndex;
    [SerializeField] private int mainChatIndex;
    [SerializeField] private bool acceptMainQuest;
    [SerializeField] private List<bool> mainQuestClearCheck;
    [SerializeField] private GameObject chatWindow;
    private string getNpcNameString;
    private TMP_Text getNpcNameText;
    private TMP_Text getNpcChatString;
    private float nextChat;
    private bool talkPlayer = false;
    [SerializeField] private int itemIndex;
    [SerializeField] private int itemQuantity;
    [Space]
    [SerializeField] private List<Button> choiceButton;

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

        choiceButton[0].onClick.AddListener(() =>
        {
            acceptMainQuest = true;

            mainChatIndex = 0;

            for (int buttonIndex = 0; buttonIndex < choiceButton.Count; buttonIndex++)
            {
                choiceButton[buttonIndex].gameObject.SetActive(false);
            }

            talkPlayer = false;

            chatWindow.SetActive(false);
        });

        choiceButton[1].onClick.AddListener(() =>
        {
            mainChatIndex = 0;

            for (int buttonIndex = 0; buttonIndex < choiceButton.Count; buttonIndex++)
            {
                choiceButton[buttonIndex].gameObject.SetActive(false);
            }

            talkPlayer = false;

            chatWindow.SetActive(false);
        });

        talkPlayer = false;
    }

    private void Start()
    {
        getNpcNameText = chatWindow.transform.Find("NpcName/NpcNameText").GetComponent<TMP_Text>();
        getNpcChatString = chatWindow.transform.Find("ChatWindow/ChatWindowText").GetComponent<TMP_Text>();

        chatWindow.SetActive(false);

        for (int buttonIndex = 0; buttonIndex < choiceButton.Count; buttonIndex++)
        {
            choiceButton[buttonIndex].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        mainQuest();
    }

    private void mainQuest()
    {
        if (talkPlayer == true)
        {
            chatWindow.SetActive(true);
        }
        else
        {
            if (chatWindow.activeSelf == true)
            {
                chatWindow.SetActive(false);
            }

            if (mainChatIndex != 0)
            {
                mainChatIndex = 0;
            }
            return;
        }

        if ((Input.GetKeyDown(KeyCode.Z) ||
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.Mouse0)) && acceptMainQuest == false && talkPlayer == true)
        {
            if (mainQuestIndex == 0)
            {
                MainQuestChapter1();
            }
        }
        else if ((Input.GetKeyDown(KeyCode.Z) ||
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.Mouse0)) && acceptMainQuest == true && talkPlayer == true)
        {
            if (mainChatIndex == 0)
            {
                if (mainQuestIndex == 0)
                {
                    MainQuestChapter1Clear();
                }

                mainChatIndex++;
            }
            else if (mainChatIndex > 0)
            {
                chatWindow.SetActive(false);
                mainChatIndex = 0;
                talkPlayer = false;
            }
        }
    }

    #region
    private void MainQuestChapter1()
    {
        for (int i = 0; i < questData.MainQuestChapter1().Count; i++)
        {
            if (mainChatIndex == i)
            {
                getNpcChatString.text = questData.MainQuestChapter1()[i];
            }
            else if (mainChatIndex >= questData.MainQuestChapter1().Count - 2)
            {
                for (int buttonIndex = 0; buttonIndex < choiceButton.Count; buttonIndex++)
                {
                    choiceButton[buttonIndex].gameObject.SetActive(true);
                }
            }

            if (mainChatIndex > questData.MainQuestChapter1().Count - 2)
            {
                mainChatIndex = questData.MainQuestChapter1().Count - 2;
            }
        }

        mainChatIndex++;
    }
    #endregion

    private void MainQuestChapter1Clear()
    {
        if (questData.ItemIndexChapter1() == itemIndex 
            && questData.ItemQuantityChapter1() == itemQuantity)
        {
            mainQuestIndex++;
            mainQuestClearCheck[0] = true;
            acceptMainQuest = false;
            mainChatIndex = 0;
            talkPlayer = false;
        }
    }

    public void MainQuestIndex(int _mainQuestIndex)
    {
        mainQuestIndex = _mainQuestIndex;
    }

    /// <summary>
    /// Npc이름을 받아올 함수
    /// </summary>
    /// <param name="_npcName"></param>
    public void MainQuestNpc(string _npcName)
    {
        getNpcNameString = _npcName;
        getNpcNameText.text = _npcName;
    }

    public void TalkPlayer(bool _talkPlayer)
    {
        talkPlayer = _talkPlayer;
    }

    public bool TalkPlayer()
    {
        return talkPlayer;
    }

    public void ItemIndex(int _itemIndex)
    {
        itemIndex = _itemIndex;
    }

    public void QuestItemCheck(int _itemIndex, int _itemQuantity)
    {
        itemIndex = _itemIndex;
        itemQuantity = _itemQuantity;
    }
}
