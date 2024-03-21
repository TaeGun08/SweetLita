using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("메인 퀘스트")]
    [SerializeField] private int mainQuestIndex;
    [SerializeField] private List<string> npcNameSequence;
    [SerializeField] private GameObject chatWindow;
    private string getNpcNameString;
    private TMP_Text getNpcNameText;
    private TMP_Text getNpcChatString;

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
    }

    private void Start()
    {
        getNpcNameText = chatWindow.transform.Find("NpcName/NpcNameText").GetComponent<TMP_Text>();
        getNpcChatString = chatWindow.transform.Find("ChatWindow/ChatWindowText").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        mainQuest();
    }

    private void mainQuest()
    {
        for (int i = 0; i < npcNameSequence.Count; i++)
        {
            if (getNpcNameString == npcNameSequence[i])
            {
                mainQuestIndex++;
            }
        }
    }

    /// <summary>
    /// Npc이름을 받아올 함수
    /// </summary>
    /// <param name="_npcName"></param>
    public void MainQuestNpc(string _npcName)
    {
        getNpcNameString = _npcName;
    }

    /// <summary>
    /// 메인 퀘스트 순서
    /// </summary>
    /// <returns></returns>
    public int MainQuestIndex()
    {
        return mainQuestIndex;
    }
}
