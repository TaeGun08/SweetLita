using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData : MonoBehaviour
{
    [Header("√©≈Õ¿« ƒ˘Ω∫∆Æ ¥Î»≠ º≥¡§")]
    [SerializeField] private List<string> mainQuestChapter1;
    [SerializeField] private int itemIndexChapter1;
    [SerializeField] private int itemQuantityChapter1;
    [Space]
    [SerializeField] private List<string> mainQuestChapter2;
    [Space]
    [SerializeField] private List<string> mainQuestChapter3;
    [Space]
    [SerializeField] private List<string> mainQuestChapter4;
    [Space]
    [SerializeField] private List<string> mainQuestChapter5;
    [Space]
    [SerializeField] private List<string> mainQuestChapter6;
    [Space]
    [SerializeField] private List<string> mainQuestChapter7;
    [Space]
    [SerializeField] private List<string> mainQuestChapter8;

    #region
    public List<string> MainQuestChapter1()
    {
        return mainQuestChapter1;
    }

    public int ItemIndexChapter1()
    {
        return itemIndexChapter1;
    }
    
    public int ItemQuantityChapter1()
    {
        return itemQuantityChapter1;
    }
    #endregion

    #region
    public List<string> MainQuestChapter2()
    {
        return mainQuestChapter2;
    }
    #endregion

    #region
    public List<string> MainQuestChapter3()
    {
        return mainQuestChapter3;
    }
    #endregion

    #region
    public List<string> MainQuestChapter4()
    {
        return mainQuestChapter4;
    }
    #endregion

    #region
    public List<string> MainQuestChapter5()
    {
        return mainQuestChapter5;
    }
    #endregion
}
