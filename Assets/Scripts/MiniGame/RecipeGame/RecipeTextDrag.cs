using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeTextDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RecipeGameManager recipeGameManager;

    private RectTransform rectTrs;
    private Vector3 trsPos;
    private CanvasGroup canvasGroup;

    [SerializeField] private int textIndex;
    private TMP_Text recipeText;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        recipeText.fontSize -= 5;
        trsPos = eventData.position;
        transform.SetAsLastSibling();
        transform.SetParent(recipeGameManager.BackGroundTrs());
        recipeGameManager.SetTextIndex(textIndex);
        recipeGameManager.SetTextObj(gameObject);
        canvasGroup.blocksRaycasts = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rectTrs.position = eventData.position;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        recipeText.fontSize += 5;

        if (textIndex != 0)
        {
            rectTrs.position = trsPos;
            canvasGroup.blocksRaycasts = true;
        }
    }

    private void Awake()
    {
        rectTrs = GetComponent<RectTransform>();
        recipeText = GetComponent<TMP_Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        recipeGameManager = RecipeGameManager.Instance;
    }

    /// <summary>
    /// 레시피 텍스트 체크 스크립트에서 인덱스를 넣어 주기위한 함수
    /// </summary>
    /// <param name="_textIndex"></param>
    /// <param name="_recipeText"></param>
    public void SetTextValue(int _textIndex)
    {
        textIndex = _textIndex;
    }

    /// <summary>
    /// 레시피게임 매니저에서 값을 넣어주기 위한 함수
    /// </summary>
    /// <param name="_textIndex"></param>
    /// <param name="_recipeText"></param>
    public void SetTextValue(int _textIndex, string _recipeText)
    {
        textIndex = _textIndex;
        recipeText.text = _recipeText;
    }

    public int GetTextIndex() 
    { 
        return textIndex; 
    }
}
