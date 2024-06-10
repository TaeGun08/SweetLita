using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrop : MonoBehaviour, IDropHandler
{
    private ClothesManager clothesManager;

    private float screenWidth; //스크린의 가로 길이를 계산하기 위한 변수
    private float screenHeight; //스크린의 세로 길이를 계산하기 위한 변수

    [SerializeField] private int typeCheck;

    [SerializeField] private bool check = false;

    [SerializeField] private GameObject pos;

    private int type;
    private int index;

    private UIDrag dragSc;

    private int score;

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            screenWidth = Screen.width;
            screenHeight = Screen.height;

            dragSc = eventData.pointerDrag.gameObject.GetComponent<UIDrag>();

            if (typeCheck == dragSc.GetTypeCheck() && check == false)
            {
                index = dragSc.GetIndexCheck();

                if (clothesManager.GameStart() == dragSc.GetIndexCheck())
                {
                    if (typeCheck == 0 || typeCheck == 3)
                    {
                        clothesManager.SetScore(20);
                        score = 20;
                    }
                    else
                    {
                        if (typeCheck == 1 && index == 0)
                        {
                            clothesManager.SetScore(60);
                            score = 60;
                        }
                        else
                        {
                            clothesManager.SetScore(30);
                            score = 30;
                        }
                    }
                }
                else
                {
                    if (typeCheck == 0 && typeCheck == 3)
                    {
                        clothesManager.SetScore(5);
                        score = 5;
                    }
                    else
                    {
                        if (typeCheck == 1 && index == 0)
                        {
                            clothesManager.SetScore(20);
                            score = 20;
                        }
                        else
                        {
                            clothesManager.SetScore(10);
                            score = 10;
                        }
                    }
                }
                dragSc.GetParentTrs().transform.SetParent(transform);

                dragSc.GetParentTrs().transform.position = new Vector2(pos.transform.position.x, pos.transform.position.y);

                clothesManager.SetIndexCheck(dragSc.GetTypeCheck(), dragSc.GetIndexCheck());
                clothesManager.SetTypeCheck(dragSc.GetTypeCheck(), dragSc.GetTypeCheck());
                check = true;
            }
            else
            {
                dragSc.GetParentTrs().transform.position = new Vector2(dragSc.GetTrs().x, dragSc.GetTrs().y);
            }
        }
    }

    private void Start()
    {
        clothesManager = ClothesManager.Instance;
    }

    private void Update()
    {
        if (dragSc != null && transform.GetComponentInChildren<UIDrag>() == null && check == true)
        {
            clothesManager.SetScore(-score);
            clothesManager.SetIndexCheck(dragSc.GetTypeCheck(), -1);
            clothesManager.SetTypeCheck(dragSc.GetTypeCheck(), -1);
            score = 0;
            index = 0;
            dragSc = null;
            check = false;
        }
    }

    public void SetCheck(bool _check)
    {
        check = _check;
    }

    public bool GetCheck()
    {
        return check;
    }
}
