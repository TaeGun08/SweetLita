using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private GameManager gameManager;
    private NpcChatManager npcChatManager;
    private QuestManager questManager;
    private TIuBookManager tIuBookManager;
    private PlayerPosManager playerPosManager;

    private Rigidbody2D rigid;
    private Vector3 moveVec;

    private Camera mainCam;

    [Header("플레이어의 이동설정")]
    [SerializeField] private float moveSpeed;

    [Header("플레이어 상호작용 영역")]
    [SerializeField] private CircleCollider2D interactionArea;

    [SerializeField] private Inventory inventory;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        mainCam = Camera.main;

        gameManager = GameManager.Instance;

        npcChatManager = NpcChatManager.Instance;

        questManager = QuestManager.Instance;

        tIuBookManager = TIuBookManager.Instance;

        playerPosManager = PlayerPosManager.Instance;

        transform.position = playerPosManager.GetPlayerPos();
    }

    private void Update()
    {
        playerInteraction();
        followCam();
        playerMove();
    }

    /// <summary>
    /// 상호작용을 위한 콜라이더를 통해 작동시켜주는 함수
    /// </summary>
    /// <param name="collider"></param>
    private void OnTrigger(Collider2D collider)
    {
        if (Input.GetKeyDown(KeyCode.Space) && collider.gameObject.layer == LayerMask.NameToLayer("Npc"))
        {
            Npc npcSc = collider.gameObject.GetComponent<Npc>();
            npcSc.NpcTalk(true);
            tIuBookManager.SetNpcIdCheck(npcSc.GetNpcIndex());
        }

        if (Input.GetKeyDown(KeyCode.Space) && collider.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Item itemSc = collider.gameObject.GetComponent<Item>();
            inventory.SetItem(itemSc.GetItemIndex(), itemSc.GetItemType(), itemSc.gameObject);
            tIuBookManager.SetItemIdCheck(itemSc.GetItemIndex());
        }
    }

    /// <summary>
    /// 플레이어가 상호작용을 할 수 있게 하는 함수
    /// </summary>
    private void playerInteraction()
    {
        Collider2D npcInteractionColl = Physics2D.OverlapCircle(interactionArea.bounds.center, interactionArea.radius,
            LayerMask.GetMask("Npc"));

        Collider2D itemInteractionColl = Physics2D.OverlapCircle(interactionArea.bounds.center, interactionArea.radius,
           LayerMask.GetMask("Item"));

        if (npcInteractionColl != null)
        {
            OnTrigger(npcInteractionColl);
        }

        if (itemInteractionColl != null)
        {
            OnTrigger(itemInteractionColl);
        }
    }

    /// <summary>
    /// 카메라가 플레이어를 따라다니게 만드는 함수
    /// </summary>
    private void followCam()
    {
        mainCam.transform.position = transform.position + new Vector3(0f, 0f, -10f);
    }

    /// <summary>
    /// 플레이어를 움직이게 동작시켜주는 함수
    /// </summary>
    private void playerMove()
    {
        if (questManager.PlayerMoveStop() == true || npcChatManager.GetPlayerMoveStop() == true)
        {
            rigid.velocity = Vector3.zero;
            return;
        }

        moveVec = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized * moveSpeed;

        rigid.velocity = moveVec;
    }
}
