using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureManager : MonoBehaviour
{
    public static PictureManager Instance;

    [Header("그림매니저")]
    [SerializeField, Tooltip("사진들")] private List<GameObject> pictures;
    [Space]
    [SerializeField, Tooltip("선택할 사진들")] private List<GameObject> choicePictures;
    [SerializeField] private int choiceNumber;

    private List<int> pictureIndex = new List<int>();

    [SerializeField] private List<bool> clearCheck = new List<bool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        int count = pictures.Count;

        for (int i = 0; i < count; i++)
        {
            pictures[i].SetActive(false);
            choicePictures[i].SetActive(false);
            clearCheck.Add(false);
        }
    }

    public GameObject GetPictureObject(int _objNumber)
    {
        return pictures[_objNumber];
    }

    public GameObject GetChoicePictureObject(int _objNumber)
    {
        return choicePictures[_objNumber];
    }

    public void SetPictureIndex(int _index)
    {
        pictureIndex.Add(_index);
    }

    public void SetChoiceNumber(bool _check)
    {
        if (_check == true)
        {
            choiceNumber++;
        }
        else
        {
            choiceNumber--;
        }
    }

    public int GetChoiceNumber()
    {
        return choiceNumber;
    }

    public void PictureCheck(int _index)
    {
        if (choiceNumber < 3)
        {
            if (pictureIndex[choiceNumber] == _index)
            {
                clearCheck[choiceNumber] = true;
            }
            else
            {
                clearCheck[choiceNumber] = false;
            }
        }
    }

    public bool GameClearCheck()
    {
        if (clearCheck[0] == true &&
            clearCheck[1] == true &&
            clearCheck[2] == true)
        {
            return true;
        }

        return false;
    }

    public bool GameOverCheck()
    {
        if (choiceNumber >= 3 && 
            (clearCheck[0] == false || 
            clearCheck[1] == false || 
            clearCheck[2] == false))
        {
            return true;
        }

        return false;
    }
}
