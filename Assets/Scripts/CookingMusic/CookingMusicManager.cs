using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CookingMusicManager : MonoBehaviour
{
    public static CookingMusicManager Instance;

    [Header("ÄíÅ· ¹ÂÁ÷ ¸Å´ÏÀú")]
    [SerializeField, Tooltip("Äµ¹ö½º")] private Canvas canvas;
    [SerializeField, Tooltip("³ëµå ÇÁ¸®ÆÕ")] private List<GameObject> nodePrefab;

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

        Screen.SetResolution(1920, 1080, true);
    }

    public GameObject GetNodeObject(int _nodeIndex)
    {
        return nodePrefab[_nodeIndex];
    }

    public Canvas GetCanvas()
    {
        return canvas;
    }
}
