using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public class SaveOption
    {
        public int widthSize;
        public int heightSize;
        public bool windowOn;
        public int dropdownValue;
        public float bgmValue;
        public float fxsValue;
    }

    public class SaveScene
    {
        public string sceneName;
    }

    private SaveOption saveOption = new SaveOption();

    private SaveScene saveScene = new SaveScene();

    [Header("게임 정지")]
    [SerializeField] private bool gamePause;

    [Header("현재 씬 이름")]
    [SerializeField] private string curSceneName;

    private string saveOptionValue = "saveOptionValue"; //스크린 사이즈 키 값을 만들 변수

    private string saveSceneName = "saveSceneName"; //씬을 저장하기 위한 변수

    [Header("게임 매니저에서 제어할 옵션 창")]
    [SerializeField, Tooltip("옵션을 껐다 키기 위한 오브젝트")] private GameObject option;
    [SerializeField, Tooltip("게임으로 돌아가기 버튼")] private Button gameBackButton;
    [SerializeField, Tooltip("메인으로 돌아가기 버튼")] private List<Button> mainBackButton;
    [SerializeField, Tooltip("메인으로 돌아가는 창")] private GameObject mainBackChoice;
    [SerializeField, Tooltip("셋팅 버튼")] private List<Button> settingButton;
    [SerializeField, Tooltip("셋팅 창")] private GameObject setting;
    [SerializeField, Tooltip("게임 종료 버튼")] private List<Button> gameExitButton;
    [SerializeField, Tooltip("게임 종료 창")] private GameObject gameExit;
    [Space]
    [SerializeField, Tooltip("해상도 변경을 위한 드롭다운")] private TMP_Dropdown dropdown;
    [SerializeField, Tooltip("창모드 변경을 위한 토글")] private Toggle toggle;
    [SerializeField, Tooltip("배경음악")] private Slider bgm;
    [SerializeField, Tooltip("효과음")] private Slider fxs;

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

        option.SetActive(false);
        setting.SetActive(false);

        string saveScreenData = PlayerPrefs.GetString(saveOptionValue);
        saveOption = JsonUtility.FromJson<SaveOption>(saveScreenData);
        setSaveOptionData(saveOption);

        gameBackButton.onClick.AddListener(() =>
        {
            option.SetActive(false);
        });

        mainBackButton[0].onClick.AddListener(() => 
        {
            mainBackChoice.SetActive(true);
        });

        mainBackButton[1].onClick.AddListener(() =>
        {
            saveScene.sceneName = curSceneName;
            string setScene = JsonUtility.ToJson(saveScene);
            PlayerPrefs.SetString(saveSceneName, setScene);
            SceneManager.LoadSceneAsync("MainScene");
        });

        mainBackButton[2].onClick.AddListener(() =>
        {
            mainBackChoice.SetActive(false);
        });

        settingButton[0].onClick.AddListener(() => 
        {
            setting.SetActive(true);
        });

        settingButton[1].onClick.AddListener(() =>
        {
            dropdownScreenSize();

            Screen.SetResolution(saveOption.widthSize, saveOption.heightSize, saveOption.windowOn);
            saveOption.dropdownValue = dropdown.value;
            saveOption.windowOn = toggle.isOn;
            saveOption.bgmValue = bgm.value;
            saveOption.fxsValue = fxs.value;

            string getScreenSize = JsonUtility.ToJson(saveOption);
            PlayerPrefs.SetString(saveOptionValue, getScreenSize);

            string saveScreenData = PlayerPrefs.GetString(saveOptionValue);
            saveOption = JsonUtility.FromJson<SaveOption>(saveScreenData);
            setSaveOptionData(saveOption);
        });

        settingButton[2].onClick.AddListener(() =>
        {
            setting.SetActive(false);
        });

        gameExitButton[0].onClick.AddListener(() => 
        {
            gameExit.SetActive(true);
        });

        gameExitButton[1].onClick.AddListener(() => 
        {
            saveScene.sceneName = curSceneName;
            string setScene = JsonUtility.ToJson(saveScene);
            PlayerPrefs.SetString(saveSceneName, setScene);

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });

        gameExitButton[2].onClick.AddListener(() =>
        {
            gameExit.SetActive(false);
        });
    }

    private void Update()
    {
        optionOnOff();
    }

    /// <summary>
    /// 게임을 정지 시키기 위한 함수
    /// </summary>
    /// <returns></returns>
    public bool GetGamePause()
    {
        return gamePause;
    }

    /// <summary>
    /// 옵션창을 끄고 켤 수 있게 도와주는 함수
    /// </summary>
    private void optionOnOff()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool optionCheck = option == option.activeSelf ? false : true;
            option.SetActive(optionCheck);
        }
    }

    /// <summary>
    /// 드롭다운을 이용하여 스크린 사이즈를 변경하는 함수
    /// </summary>
    private void dropdownScreenSize()
    {
        if (dropdown.value == 0)
        {
            saveOption.widthSize = 640;
            saveOption.heightSize = 360;
        }
        else if (dropdown.value == 1)
        {
            saveOption.widthSize = 854;
            saveOption.heightSize = 480;
        }
        else if (dropdown.value == 2)
        {
            saveOption.widthSize = 960;
            saveOption.heightSize = 540;
        }
        else if (dropdown.value == 3)
        {
            saveOption.widthSize = 1280;
            saveOption.heightSize = 720;
        }
    }

    /// <summary>
    /// 저장한 스크린 데이터를 변수에 할당
    /// </summary>
    /// <param name="_saveScreenSize"></param>
    private void setSaveOptionData(SaveOption _saveScreenSize)
    {
        Screen.SetResolution(_saveScreenSize.widthSize, _saveScreenSize.heightSize, _saveScreenSize.windowOn);
        dropdown.value = _saveScreenSize.dropdownValue;
        toggle.isOn = _saveScreenSize.windowOn;
        bgm.value = _saveScreenSize.bgmValue;
        fxs.value = _saveScreenSize.fxsValue;
    }
}
