using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosManager : MonoBehaviour
{
    public static PlayerPosManager Instance;

    public class SavePos
    {
        public float xPos;
        public float yPos;
    }

    private SavePos savePos = new SavePos();

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

        if (PlayerPrefs.GetString("savePlayerPos") != string.Empty)
        {
            string savePosData = PlayerPrefs.GetString("savePlayerPos");
            savePos = JsonConvert.DeserializeObject<SavePos>(savePosData);
        }
    }

    public Vector3 GetPlayerPos()
    {
        return new Vector3(savePos.xPos, savePos.yPos, 0f);
    }
}
