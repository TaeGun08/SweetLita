using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private QuestManager questManager;

    [SerializeField] private int npcIndex;
    [SerializeField] private List<int> questIndex;

    private void Start()
    {
        questManager = QuestManager.Instance;
    }

    /// <summary>
    /// 퀘스트를 실행할 수 있게 하는 함수
    /// </summary>
    public void NpcQuestChapter1()
    {
        for (int i = 0; i < questIndex.Count; i++)
        {
            questManager.QuestAccept(npcIndex, questIndex[i]);
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
}
