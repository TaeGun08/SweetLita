using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicNode : MonoBehaviour
{
    [Header("노드 설정")]
    [SerializeField, Tooltip("노드 번호")] private int nodeNumber;
    [SerializeField, Tooltip("노드 이미지")] private List<Sprite> nodeImages;
    private Image nodeImage;

    private void Awake()
    {
        nodeImage = GetComponent<Image>();
    }

    /// <summary>
    /// 키 코드를 반환하는 함수
    /// </summary>
    /// <param name="_number"></param>
    /// <returns></returns>
    public KeyCode GetKeyCode(int _number)
    {
        switch (_number)
        {
            case 0:
                nodeImage.sprite = nodeImages[0];
                nodeNumber = _number;
                return KeyCode.UpArrow;
            case 1:
                nodeImage.sprite = nodeImages[1];
                nodeNumber = _number;
                return KeyCode.DownArrow;
            case 2:
                nodeImage.sprite = nodeImages[2];
                nodeNumber = _number;
                return KeyCode.LeftArrow;
            case 3:
                nodeImage.sprite = nodeImages[3];
                nodeNumber = _number;
                return KeyCode.RightArrow;
        }

        return KeyCode.None;
    }
}
