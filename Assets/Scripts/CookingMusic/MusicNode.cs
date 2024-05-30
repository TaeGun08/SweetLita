using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicNode : MonoBehaviour
{
    [Header("노드 설정")]
    [SerializeField, Tooltip("노드 번호")] private int nodeNumber;

    private Image nodeColor;

    private void Awake()
    {
        nodeColor = GetComponent<Image>();
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
                nodeColor.color = Color.red;
                nodeNumber = _number;
                return KeyCode.UpArrow;
            case 1:
                nodeColor.color = Color.blue;
                nodeNumber = _number;
                return KeyCode.DownArrow;
            case 2:
                nodeColor.color = Color.yellow;
                nodeNumber = _number;
                return KeyCode.LeftArrow;
            case 3:
                nodeColor.color = Color.green;
                nodeNumber = _number;
                return KeyCode.RightArrow;
        }

        return KeyCode.None;
    }
}
