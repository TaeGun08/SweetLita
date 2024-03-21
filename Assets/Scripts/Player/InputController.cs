using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Vector3 moveVec;

    private Camera mainCam;

    [Header("플레이어의 이동설정")]
    [SerializeField] private float moveSpeed;

    [Header("플레이어 상호작용 영역")]
    [SerializeField] private CircleCollider2D interactionArea;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        playerInteraction();
        followCam();
        playerMove();
    }

    private void OnTrigger(Collider2D collider)
    {
        if (Input.GetKeyDown(KeyCode.Z) && collider.gameObject.layer == LayerMask.NameToLayer("Npc"))
        {
            Npc npcSc = collider.gameObject.GetComponent<Npc>();
            npcSc.NpcNameCheck(true);
        }
    }

    /// <summary>
    /// 플레이어가 상호작용을 할 수 있게 하는 함수
    /// </summary>
    private void playerInteraction()
    {
        Collider2D npcInteractionColl = Physics2D.OverlapCircle(interactionArea.bounds.center, interactionArea.radius, 
            LayerMask.GetMask("Npc"));

        if (npcInteractionColl != null)
        {
            OnTrigger(npcInteractionColl);
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
        moveVec = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized * moveSpeed;

        rigid.velocity = moveVec;
    }

}
