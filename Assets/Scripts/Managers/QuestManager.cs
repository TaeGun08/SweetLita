using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    private QuestData questData;

    [Header("퀘스트 설정")]
    [SerializeField, Tooltip("내가 클리어한 퀘스트")] private List<int> questIndex;

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

    /// <summary>
    /// 퀘스트를 클리어했는지 체크하기 위한 함수
    /// </summary>
    /// <param name="_questIndex"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 퀘스트를 수락했는지 확인하기 위한 함수
    /// </summary>
    /// <param name="_npcIndex"></param>
    /// <param name="_questIndex"></param>
    public void QuestAccept(int _npcIndex, int _questIndex)
    {
        questData.NpcQuestChapter1(_npcIndex, _questIndex);
    }

    /// <summary>
    /// 클리어했을 시 클리어한 퀘스트의 번호를 넣어 줄 함수
    /// </summary>
    /// <param name="_questIndex"></param>
    public void SetQuestIndex(int _questIndex)
    {
        questIndex.Add(_questIndex);
    }

    /// <summary>
    /// 플레이어가 대화도중 못 움직이게 만들어주는 함수
    /// </summary>
    /// <returns></returns>
    public bool PlayerMoveStop()
    {
        return questData.PlayerMoveStop();
    }
}
