using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelpoController : MonoBehaviour
{
    public class SaveScene
    {
        public string sceneName;
    }

    public class SavePos
    {
        public float xPos;
        public float yPos;
    }

    private SaveScene saveScene = new SaveScene();

    private SavePos savePos = new SavePos();

    [SerializeField] private string nextScene;
    [SerializeField] private Vector2 playerNextScenePos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            setSavePos();
            SceneManager.LoadSceneAsync(nextScene);
        }
    }

    private void setSavePos()
    {
        saveScene.sceneName = nextScene;
        string setScene = JsonConvert.SerializeObject(saveScene);
        PlayerPrefs.SetString("saveSceneName", setScene);

        savePos.xPos = playerNextScenePos.x;
        savePos.yPos = playerNextScenePos.y;
        string setPos = JsonConvert.SerializeObject(savePos);
        PlayerPrefs.SetString("savePlayerPos", setPos);
    }
}
