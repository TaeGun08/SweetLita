using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeTextDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rect;

    [SerializeField] private int textIndex;
    private TMP_Text recipeText;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        recipeText.fontSize -= 5;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        recipeText.fontSize += 5;
    }

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        recipeText = GetComponent<TMP_Text>();
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
}
