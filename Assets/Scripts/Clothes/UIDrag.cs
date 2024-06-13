using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ClothesManager clothesManager;

    [SerializeField] private int type;
    [SerializeField] private int index;
    [Space]
    [SerializeField] private RectTransform parentRectTrs; //부모 렉트트랜스폼
    [SerializeField] private List<float> posCheck;
    private Vector2 mousePos; //마우스 포지션

    private float screenWidth; //스크린의 가로 길이를 계산하기 위한 변수
    private float screenHeight; //스크린의 세로 길이를 계산하기 위한 변수
    private Vector2 trsPos;
    [SerializeField] private Vector2 proportion;

    private CanvasGroup canvasGroup; //캔버스그룹

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        mousePos.x = transform.position.x - eventData.position.x;
        mousePos.y = transform.position.y - eventData.position.y;

        parentRectTrs.SetParent(clothesManager.GetCanvas().transform);
        parentRectTrs.SetAsLastSibling();

        canvasGroup.blocksRaycasts = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        parentRectTrs.position = eventData.position + new Vector2(0f, posCheck[2]);
        //if (type == 0)
        //{
        //    if (screenWidth == 3840 && screenHeight == 2160)
        //    {
        //        parentRectTrs.position = eventData.position + new Vector2(0f, posCheck[0]);
        //    }
        //    else if (screenWidth == 2560 && screenHeight == 1440)
        //    {
        //        parentRectTrs.position = eventData.position + new Vector2(0f, posCheck[1]);
        //    }
        //    else if (screenWidth == 1920 && screenHeight == 1080)
        //    {
        //        parentRectTrs.position = eventData.position + new Vector2(0f, posCheck[2]);
        //    }
        //    else
        //    {
        //        parentRectTrs.position = eventData.position;
        //    }
        //}
        //else if (type == 1)
        //{
        //    if (screenWidth == 3840 && screenHeight == 2160)
        //    {
        //        parentRectTrs.position = eventData.position + new Vector2(0f, posCheck[0]);
        //    }
        //    else if (screenWidth == 2560 && screenHeight == 1440)
        //    {
        //        parentRectTrs.position = eventData.position + new Vector2(0f, posCheck[1]);
        //    }
        //    else if (screenWidth == 1920 && screenHeight == 1080)
        //    {
        //        parentRectTrs.position = eventData.position + new Vector2(0f, posCheck[2]);
        //    }
        //    else
        //    {
        //        parentRectTrs.position = eventData.position;
        //    }
        //}
        //else if (type == 2)
        //{
        //    if (screenWidth == 3840 && screenHeight == 2160)
        //    {
        //        parentRectTrs.position = eventData.position + new Vector2(0f, posCheck[0]);
        //    }
        //    else if (screenWidth == 2560 && screenHeight == 1440)
        //    {
        //        parentRectTrs.position = eventData.position + new Vector2(0f, posCheck[1]);
        //    }
        //    else if (screenWidth == 1920 && screenHeight == 1080)
        //    {
        //        parentRectTrs.position = eventData.position + new Vector2(0f, posCheck[2]);
        //    }
        //    else
        //    {
        //        parentRectTrs.position = eventData.position;
        //    }
        //}
        //else if (type == 3)
        //{
        //    if (screenWidth == 3840 && screenHeight == 2160)
        //    {
        //        parentRectTrs.position = eventData.position + new Vector2(0f, posCheck[0]);
        //    }
        //    else if (screenWidth == 2560 && screenHeight == 1440)
        //    {
        //        parentRectTrs.position = eventData.position + new Vector2(0f, posCheck[1]);
        //    }
        //    else if (screenWidth == 1920 && screenHeight == 1080)
        //    {
        //        parentRectTrs.position = eventData.position + new Vector2(0f, posCheck[2]);
        //    }
        //    else
        //    {
        //        parentRectTrs.position = eventData.position;
        //    }
        //}
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (transform.position.x >= screenWidth ||
            transform.position.x <= 0 ||
            transform.position.y >= screenHeight ||
            transform.position.y <= 0)
        {
            parentRectTrs.position = new Vector3((screenWidth * proportion.x), (screenHeight * proportion.y));
        }

        canvasGroup.blocksRaycasts = true;
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();      
    }

    private void Start()
    {
        clothesManager = ClothesManager.Instance;

        screenWidth = Screen.width;
        screenHeight = Screen.height;

        trsPos = new Vector2((screenWidth * proportion.x), (screenHeight * proportion.y));
        parentRectTrs.position = trsPos;
    }

    public RectTransform GetParentTrs()
    {
        return parentRectTrs;
    }

    public int GetTypeCheck()
    {
        return type;
    }

    public int GetIndexCheck()
    {
        return index;
    }

    public Vector2 GetTrs()
    {
        return trsPos;
    }
}
