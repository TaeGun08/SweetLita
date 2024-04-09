using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeTextCheck : MonoBehaviour, IDropHandler
{
    private RecipeGameManager recipeGameManager;

    private RectTransform rect;

    [SerializeField] private int textIndex;

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (recipeGameManager.GetTextIndex() == textIndex)
            {
                eventData.pointerDrag.transform.SetParent(transform);

                eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;

                RecipeTextDrag textSc = recipeGameManager.GetTextObj().GetComponent<RecipeTextDrag>();
                textSc.SetTextValue(0);

                recipeGameManager.GameClearCheck();
            }
            else
            {
                recipeGameManager.SetTextIndex(0);
            }
        }
    }

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        recipeGameManager = RecipeGameManager.Instance;
    }
}
