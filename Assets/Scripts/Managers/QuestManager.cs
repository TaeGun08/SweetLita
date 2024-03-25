using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    private QuestData questData;

    [SerializeField] private List<int> questIndex;
    [SerializeField] private List<int> questCheckIndex;

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
        questData = transform.GetChild(0).GetComponent<QuestData>();
    }

    public bool QuestClearCheck(int _questIndex)
    {
        int count = questIndex.Count;
        for (int i = 0; i < count; i++)
        {
            if (questIndex[i] == _questIndex)
            {
                return true;
            }
        }

        return false;
    }

    public void QuestAccept(int _npcIndex, int _questIndex)
    {
        questData.NpcQuestChapter1(_npcIndex, _questIndex);
    }

    public void SetQuestIndex(int _questIndex)
    {
        questIndex.Add(_questIndex);
    }

    public List<int> GetQuestIndex()
    {
        return questIndex;
    }

    public void SetQuestCheckIndex(int _questCheckIndex)
    {
        questCheckIndex.Add(_questCheckIndex);
    }

    public List<int> GetQuestCheckIndex()
    {
        return questCheckIndex;
    }
}
