using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureManager : MonoBehaviour
{
    public static PictureManager Instance;

    [Header("그림매니저")]
    [SerializeField, Tooltip("사진 프리팹")] private List<GameObject> picturePrefab;

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
    }

    public GameObject GetPictureObject(int _objNumber)
    {
        return picturePrefab[_objNumber];
    }
}
