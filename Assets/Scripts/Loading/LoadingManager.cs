using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private float timer;
    private bool check;

    private void Update()
    {
        if (check == false)
        {
            timer += Time.deltaTime;

            if (timer >= 1f)
            {
                string get = PlayerPrefs.GetString("saveScene");
                string getScene = JsonConvert.DeserializeObject<string>(get);
                SceneManager.LoadSceneAsync(getScene);

                timer = 0;
                check = true;
            }
        }   
    }
}
