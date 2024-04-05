using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TIuBookManager : MonoBehaviour
{
    public static TIuBookManager Instance;

    public class BookData
    {
        public List<int> itemsIndex = new List<int>();
        public List<int> RecipeIndex = new List<int>();
        public List<int> npcIndex = new List<int>();
    }

    private BookData bookData = new BookData();

    private GameManager gameManager;
    private QuestManager questManager;
    private Inventory inventory;
    private NpcChatManager npcChatManager;

    [Header("도감 설정")]
    [SerializeField] private List<GameObject> bookObj;
    [SerializeField] private List<GameObject> pageObj;
    [SerializeField] private Button bookOpen;
    [SerializeField] private Button itemPageOpen;
    [SerializeField] private Button RecipePageOpen;
    [SerializeField] private Button ResidentPageOpen;
    [SerializeField] private Button bookClose;
    [Space]
    [SerializeField] private List<int> itemsIndex;
    [SerializeField] private List<Image> itemImages;
    private bool itemCheck = false;
    [Space]
    [SerializeField] private List<int> RecipeIndex;
    [SerializeField] private List<Image> RecipeImages;
    private bool npcCheck = false;
    [Space]
    [SerializeField] private List<int> npcIndex;
    [SerializeField] private List<Image> npcImages;
    private bool tiuBookOnCheck = false;

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

        #region
        bookOpen.onClick.AddListener(() => 
        {
            bookObj[1].SetActive(false);
            bookObj[2].SetActive(true);
        });

        itemPageOpen.onClick.AddListener(() => 
        {
            pageObj[0].SetActive(false);
            pageObj[1].SetActive(true);
            pageObj[2].SetActive(false);
            pageObj[3].SetActive(false);
        });

        RecipePageOpen.onClick.AddListener(() =>
        {
            pageObj[0].SetActive(false);
            pageObj[1].SetActive(false);
            pageObj[2].SetActive(true);
            pageObj[3].SetActive(false);
        });

        ResidentPageOpen.onClick.AddListener(() =>
        {
            pageObj[0].SetActive(false);
            pageObj[1].SetActive(false);
            pageObj[2].SetActive(false);
            pageObj[3].SetActive(true);
        });

        bookClose.onClick.AddListener(() =>
        {
            pageObj[0].SetActive(true);
            pageObj[1].SetActive(false);
            pageObj[2].SetActive(false);
            pageObj[3].SetActive(false);
            bookObj[0].SetActive(false);
            bookObj[1].SetActive(true);
            bookObj[2].SetActive(false);
        });
        #endregion
    }

    private void Start()
    {
        gameManager = GameManager.Instance;

        questManager = QuestManager.Instance;

        inventory = Inventory.Instance;

        npcChatManager = NpcChatManager.Instance;

        bookObj[0].SetActive(false);
        bookObj[2].SetActive(false);

        if (PlayerPrefs.GetString("bookSaveData") != string.Empty)
        {
            string getBookData = PlayerPrefs.GetString("bookSaveData");
            bookData = JsonConvert.DeserializeObject<BookData>(getBookData);

            int itemCount = bookData.itemsIndex.Count;
            for (int i = 0; i < itemCount; i++)
            {
                itemsIndex.Add(bookData.itemsIndex[i]);
            }

            int npcCount = bookData.npcIndex.Count;
            for (int i = 0; i < npcCount; i++)
            {
                npcIndex.Add(bookData.npcIndex[i]);
            }

            itemCheck = true;
            npcCheck = true;
        }
    }

    private void Update()
    {
        bookOnOff();
        openItem();
        openNpc();
    }

    /// <summary>
    /// 도감을 껐다 켤 수 있는 함수
    /// </summary>
    private void bookOnOff()
    {
        if (gameManager.GetOptionOnCheck() == true || inventory.GetInventoryOnCheck() == true ||
            npcChatManager.GetPlayerMoveStop() == true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.B) && questManager.PlayerMoveStop() == false)
        {
            bool onOff = bookObj[0] == bookObj[0].activeSelf ? false : true;
            tiuBookOnCheck = onOff;
            bookObj[0].SetActive(onOff);

            if (onOff == false)
            {
                pageObj[0].SetActive(true);
                pageObj[1].SetActive(false);
                pageObj[2].SetActive(false);
                pageObj[3].SetActive(false);
                bookObj[0].SetActive(false);
                bookObj[1].SetActive(true);
                bookObj[2].SetActive(false);
            }
        }
    }

    /// <summary>
    /// 아이템 인덱스를 확인하고 블라인드처리를 해제하는 함수
    /// </summary>
    private void openItem()
    {
        if (itemCheck == true)
        {
            int count = itemsIndex.Count;
            for (int i = 0; i < count; i++)
            {
                switch (itemsIndex[i])
                {
                    case 100:
                        itemImages[0].color = Color.white;
                        break;
                    case 101:
                        itemImages[1].color = Color.white;
                        break;
                    case 102:
                        itemImages[2].color = Color.white;
                        break;
                }
            }
            itemCheck = false;
        }
    }

    /// <summary>
    /// Npc 인덱스를 확인하고 블라인드처리를 해제하는 함수
    /// </summary>
    private void openNpc()
    {
        if (npcCheck == true)
        {
            int count = npcIndex.Count;
            for (int i = 0; i < count; i++)
            {
                switch (npcIndex[i])
                {
                    case 10:
                        npcImages[0].color = Color.white;
                        break;
                }
            }
            npcCheck = false;
        }
    }

    /// <summary>
    /// 아이템 인덱스를 확인하여 있으면 true 없으면 false 값을 반환하는 함수
    /// </summary>
    /// <param name="_itemId"></param>
    /// <returns></returns>
    private bool SetItemsIndex(int _itemId)
    {
        int count = itemsIndex.Count;

        for (int i = 0; i < count; i++)
        {
            if (itemsIndex[i] == _itemId)
            {         
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 아이템 인덱스를 받아오는 함수
    /// </summary>
    /// <param name="_itemId"></param>
    public void SetItemIdCheck(int _itemId)
    {
        if (SetItemsIndex(_itemId) == false)
        {
            itemsIndex.Add(_itemId);
            bookData.itemsIndex.Add(_itemId);
            string setNpcId = JsonConvert.SerializeObject(bookData);
            PlayerPrefs.SetString("bookSaveData", setNpcId);
            itemCheck = true;
        }
    }

    /// <summary>
    /// Npc 인덱스를 확인하여 있으면 true 없으면 false 값을 반환하는 함수
    /// </summary>
    /// <param name="_itemId"></param>
    /// <returns></returns>
    private bool SetNpcIndex(int _itemId)
    {
        int count = npcIndex.Count;

        for (int i = 0; i < count; i++)
        {
            if (npcIndex[i] == _itemId)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Npc 인덱스를 받아오는 함수
    /// </summary>
    /// <param name="_itemId"></param>
    public void SetNpcIdCheck(int _itemId)
    {
        if (SetNpcIndex(_itemId) == false)
        {
            npcIndex.Add(_itemId);
            bookData.npcIndex.Add(_itemId);
            string setNpcId = JsonConvert.SerializeObject(bookData);
            PlayerPrefs.SetString("bookSaveData", setNpcId);
            npcCheck = true;
        }
    }

    public bool GetTiuBookOnCheck()
    {
        return tiuBookOnCheck;
    }
}
