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

    public void NpcQuestChapter1()
    {
        for (int i = 0; i < questIndex.Count; i++)
        {
            questManager.QuestAccept(npcIndex, questIndex[i]);
        }
    }
}
