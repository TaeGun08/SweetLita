using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public class InventorySlotData
    {
        public List<int> itemIndex = new List<int>();
        public List<int> itemQuantity = new List<int>();
    }

    private InventorySlotData inventorySlotData = new InventorySlotData();

    private QuestManager questManager;

    [Header("인벤토리")]
    [SerializeField, Tooltip("인벤토리 UI오브젝트")] private GameObject inventory;
    [SerializeField, Tooltip("인벤토리의 슬롯당 최대 개수")] private int maxQuantiry;
    [SerializeField, Tooltip("슬롯 리스트")] private List<Slot> slot;
    private int questItems; //퀘스트 아이템들
    private int qeustItemIndex; //퀘스트 아이템의 번호

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

        for (int i = 0; i < 20; i++)
        {
            inventorySlotData.itemIndex.Add(0);
            inventorySlotData.itemQuantity.Add(0);
        }
    }

    private void Start()
    {
        questManager = QuestManager.Instance;

        setInventoryData();
    }

    private void Update()
    {
        inventoryOnOff();
    }

    /// <summary>
    /// 인벤토리를 껐다 킬 수 있게 하는 함수
    /// </summary>
    private void inventoryOnOff()
    {
        if (Input.GetKeyDown(KeyCode.I) && questManager.PlayerMoveStop() == false)
        {
            bool invenOnOff = inventory == inventory.activeSelf ? false : true;
            inventory.SetActive(invenOnOff);
        }
    }


    /// <summary>
    /// 인벤토리에 저장된 값을 불러오는 함수
    /// </summary>
    private void setInventoryData()
    {
        if (PlayerPrefs.GetString("inventoryData") != string.Empty)
        {
            string getInventoryItem = PlayerPrefs.GetString("inventoryData");
            inventorySlotData = JsonConvert.DeserializeObject<InventorySlotData>(getInventoryItem);

            if (inventorySlotData != null && inventorySlotData.itemIndex.Count != 0
                && inventorySlotData.itemQuantity.Count != 0)
            {
                for (int i = 0; i < slot.Count; i++)
                {
                    Slot slotSc = slot[i];
                    slotSc.SetSlotData(inventorySlotData.itemIndex[i], inventorySlotData.itemQuantity[i]);
                }
            }
        }
    }

    /// <summary>
    /// 슬롯에 있는 데이터를 저장하기 위한 함수
    /// </summary>
    private void saveInventory()
    {
        string setInventoryItem = JsonConvert.SerializeObject(inventorySlotData);
        PlayerPrefs.SetString("inventoryData", setInventoryItem);
    }

    /// <summary>
    /// 아이템을 넣어줄 함수
    /// </summary>
    /// <param name="_itemIndex"></param>
    /// <param name="_itemType"></param>
    /// <param name="_itemObj"></param>
    public void SetItem(int _itemIndex, Item.ItemType _itemType, GameObject _itemObj)
    {
        for (int i = 0; i < slot.Count; i++)
        {
            Slot slotSc = slot[i];

            if (slotSc.GetItemIndex() == _itemIndex && slotSc.GetSlotQuantity() < maxQuantiry)
            {
                slotSc.SetSlot(_itemIndex, _itemObj);
                inventorySlotData.itemIndex[i] = slotSc.GetItemIndex();
                inventorySlotData.itemQuantity[i] = slotSc.GetSlotQuantity();
                saveInventory();
                return;
            }
            else if (slotSc.GetItemIndex() == 0 && slotSc.GetSlotQuantity() < maxQuantiry)
            {
                slotSc.SetSlot(_itemIndex, _itemObj);
                inventorySlotData.itemIndex[i] = slotSc.GetItemIndex();
                inventorySlotData.itemQuantity[i] = slotSc.GetSlotQuantity();
                saveInventory();
                return;
            }
        }
    }

    /// <summary>
    /// 퀘스트 아이템을 확인하는 함수
    /// </summary>
    /// <param name="_itemIndex"></param>
    /// <param name="_itemQuantity"></param>
    /// <returns></returns>
    public bool QuestItemCheck(int _itemIndex, int _itemQuantity)
    {
        qeustItemIndex = _itemIndex;

        int itmeQuantity = 0;

        for (int i = 0; i < slot.Count; i++)
        {
            Slot slotSc = slot[i];

            if (slotSc.GetItemIndex() == qeustItemIndex)
            {
                itmeQuantity += slotSc.GetSlotQuantity();
            }

            inventorySlotData.itemIndex[i] = slotSc.GetItemIndex();
            inventorySlotData.itemQuantity[i] = slotSc.GetSlotQuantity();
            saveInventory();
        }

        return itmeQuantity >= _itemQuantity;
    }

    /// <summary>
    /// 퀘스트 아이템을 지우기 위한 함수
    /// </summary>
    /// <param name="_itemIndex"></param>
    /// <param name="_questItem"></param>
    public void QuestItem(int _itemIndex, int _questItem)
    {
        questItems = _questItem;

        for (int i = 0; i < slot.Count; i++)
        {
            Slot slotSc = slot[i];

            if (slotSc.GetItemIndex() == _itemIndex && slotSc.GetSlotQuantity() != 0)
            {
                if (questItems == 0)
                {
                    return;
                }

                questItems = slotSc.QuestItem(questItems);
            }

            inventorySlotData.itemIndex[i] = slotSc.GetItemIndex();
            inventorySlotData.itemQuantity[i] = slotSc.GetSlotQuantity();
            saveInventory();
        }
    }

    /// <summary>
    /// 다른 스크립트에서 오브젝트를 확인하기 위한 함수
    /// </summary>
    /// <returns></returns>
    public GameObject InventoryObj()
    {
        return inventory;
    }
}
