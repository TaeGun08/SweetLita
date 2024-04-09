using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestManager;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public class QuestIndexData
    {
        public List<int> questIndex = new List<int>();
        public int curQuestIndex;
    }

    private QuestIndexData questIndexData = new QuestIndexData();

    private NpcChatManager npcChatManager;
    private QuestData questData;

    [Header("퀘스트 설정")]
    [SerializeField, Tooltip("내가 클리어한 퀘스트")] private List<int> questIndex;
    [SerializeField] private int curQuestIndex;

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
        npcChatManager = NpcChatManager.Instance;

        questData = transform.GetChild(0).GetComponent<QuestData>();

        if (PlayerPrefs.GetString("saveQuestIndex") != string.Empty)
        {
            string getQeustIndex = PlayerPrefs.GetString("saveQuestIndex");
            //questIndexData = JsonUtility.FromJson<QuestIndexData>(getQeustIndex);
            questIndexData = JsonConvert.DeserializeObject<QuestIndexData>(getQeustIndex);
            if (questIndexData.questIndex != null && questIndexData.questIndex.Count != 0)
            {
                for (int i = 0; i < questIndexData.questIndex.Count; i++)
                {
                    questIndex.Add(questIndexData.questIndex[i]);
                }
            }

            curQuestIndex = questIndexData.curQuestIndex;

            if (curQuestIndex != 0)
            {
                npcChatManager.SetQuestCheck(true);
                questData.SetChatIndex(100);
            }
        }
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

    public List<int> GetQuestClearIndex()
    {
        return questIndex;
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
        questIndexData.questIndex.Add(_questIndex);

        //string setQuestIndex = JsonUtility.ToJson(new JosnConvert<int>(questIndexData.questIndex));
        string setQuestIndex = JsonConvert.SerializeObject(questIndexData); 
        PlayerPrefs.SetString("saveQuestIndex", setQuestIndex);
    }

    public void SetCurQuestIndex(int _curQuestIndex)
    {
        questIndexData.curQuestIndex = _curQuestIndex;
        curQuestIndex = _curQuestIndex;

        string setQuestIndex = JsonConvert.SerializeObject(questIndexData);
        PlayerPrefs.SetString("saveQuestIndex", setQuestIndex);
    }

    public int GetCurQuestIndex()
    {
        return curQuestIndex;
    }

    /// <summary>
    /// 플레이어가 대화도중 못 움직이게 만들어주는 함수
    /// </summary>
    /// <returns></returns>
    public bool PlayerMoveStop()
    {
        return questData.PlayerMoveStop();
    }

    //[System.Serializable]
    //public class JosnConvert<T>
    //{
    //    public List<T> questIndex;
    //    public JosnConvert(List<T> list) => this.questIndex = list;
    //}
}
