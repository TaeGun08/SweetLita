using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameClearCheck : MonoBehaviour
{
    public static MiniGameClearCheck Instance;

    public class MiniGameCheck
    {
        public List<int> miniGameIndex = new List<int>();
    }

    private MiniGameCheck miniGameCheck = new MiniGameCheck();
    [SerializeField] private List<int> miniGameIndex;

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

        if (PlayerPrefs.GetString("saveMiniData") != string.Empty)
        {
            string getData = PlayerPrefs.GetString("saveMiniData");
            miniGameCheck = JsonConvert.DeserializeObject<MiniGameCheck>(getData);

            int count = miniGameCheck.miniGameIndex.Count;
            for (int i = 0; i < count; i++)
            {
                miniGameIndex.Add(miniGameCheck.miniGameIndex[i]);
            }
        }
    }

    public void SetSaveMiniCheckData(int _index)
    {
        if (GetSaveMiniCheckData(_index) == true)
        {
            return;
        }

        miniGameCheck.miniGameIndex.Add(_index);
        miniGameIndex.Add(_index);

        string setData = JsonConvert.SerializeObject(miniGameCheck);
        PlayerPrefs.SetString("saveMiniData", setData);
    }

    public bool GetSaveMiniCheckData(int _index)
    {
        int count = miniGameIndex.Count;

        for (int i = 0; i < count; i++)
        {
            if (miniGameIndex[i] == _index)
            {
                return true;
            }
        }

        return false;
    }
}
