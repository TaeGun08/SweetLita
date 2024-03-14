using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Vector3 moveVec;

    [Header("플레이어의 이동설정")]
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveVec = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized * moveSpeed;

        rigid.velocity = moveVec;
    }
}
