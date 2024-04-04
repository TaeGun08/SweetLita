using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private NpcChatManager npcChatManager;
    private QuestManager questManager;

    private Npc npc;

    [SerializeField] private int npcIndex;
    [SerializeField] private List<int> questIndex;

    private void Awake()
    {
        npc = GetComponent<Npc>();
    }

    private void Start()
    {
        npcChatManager = NpcChatManager.Instance;

        questManager = QuestManager.Instance;
    }

    /// <summary>
    /// Npc 대화를 실행할 수 있게 하는 함수
    /// </summary>
    public void NpcTalk(bool _npcTalkCheck)
    {
        int count = questIndex.Count;
        for (int i = 0; i < count; i++)
        {
            if (npcChatManager.GetQuestCheck() == true && questManager.GetCurQuestIndex() == questIndex[i])
            {
                questManager.QuestAccept(npcIndex, questIndex[i]);
                return;
            }
            else if (npcChatManager.GetQuestCheck() == false || 
                (npcChatManager.GetQuestCheck() == true && questManager.GetCurQuestIndex() != questIndex[i]))
            {
                npcChatManager.SetNpc(npc);
                npcChatManager.SetNpcTalkCheck(_npcTalkCheck);
                npcChatManager.SetNpcIndex(npcIndex, questIndex[i]);
            }
        }
    }

    /// <summary>
    /// Npc 인덱스를 다른 스크립트에서 가져올 수 있게 하는 함수
    /// </summary>
    /// <returns></returns>
    public int GetNpcIndex()
    {
        return npcIndex;
    }

    public List<int> GetQuestIndex()
    {
        return questIndex;
    }
}
