using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    private PuzzleGameManager puzzleGameManager;

    private RectTransform rectTrs;

    [Header("퍼즐 조각 설정")]
    [SerializeField, Tooltip("퍼즐의 순서")] private int pieceIndex;
    private bool clickCheck = false;
    [SerializeField, Tooltip("퍼즐의 선택시 테두리")] private GameObject frameObj;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (puzzleGameManager.GameClear() == true)
        {
            return;
        }

        if (puzzleGameManager.PieceObjCheck() == true)
        {
            if (clickCheck == false)
            {
                clickCheck = true;
                puzzleGameManager.SetPiece(gameObject);
            }

            frameObj.SetActive(clickCheck);
        }
    }

    private void Awake()
    {
        puzzleGameManager = PuzzleGameManager.Instance;

        rectTrs = GetComponent<RectTransform>();
    }

    /// <summary>
    /// 퍼즐이 생성됐을 때 매니저에서 받아올 인덱스
    /// </summary>
    /// <param name="_pieceIndex"></param>
    public void SetPieceIndex(int _pieceIndex)
    {
        pieceIndex = _pieceIndex;
    }

    /// <summary>
    /// 퍼즐의 인덱스를 반환하는  함수
    /// </summary>
    /// <returns></returns>
    public int GetPieceIndex() 
    {
        return pieceIndex;
    }

    /// <summary>
    /// 퍼즐이 생성될 때 위치를 조정해줄 함수
    /// </summary>
    /// <param name="_rectTrs"></param>
    public void SetRectTrs(Vector3 _rectTrs)
    {
        rectTrs.localPosition = _rectTrs;
    }

    /// <summary>
    /// 퍼즐의 위치를 반환하는 함수
    /// </summary>
    /// <returns></returns>
    public RectTransform GetRectTrs()
    {
        return rectTrs;
    }

    /// <summary>
    /// 퍼즐을 바꿨는지 확인해주는 함수
    /// </summary>
    public void ChangeTrue()
    {
        clickCheck = false;
        frameObj.SetActive(clickCheck);
    }

    /// <summary>
    /// 퍼즐의 인덱스를 확인하기 위한 함수
    /// </summary>
    /// <returns></returns>
    public int PieceIndexCheck()
    {
        return pieceIndex;
    }
}
