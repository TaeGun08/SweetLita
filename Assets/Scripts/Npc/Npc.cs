using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private QuestManager questManager;

    [Header("Npc ¼³Á¤")]
    [SerializeField] private string npcName;
    [SerializeField] private int intimacy;
    private bool questOn = false;

    private void Awake()
    {
        gameObject.name = npcName;
    }

    private void Start()
    {
        questManager = QuestManager.Instance;
    }

    public void NpcNameCheck(bool _questOn)
    {
        questOn = _questOn;

        if (questOn == true)
        {
            Debug.Log(gameObject.name);
            questManager.MainQuestNpc(npcName);
            questOn = false;
        }
    }
}
