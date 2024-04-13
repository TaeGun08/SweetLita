using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundManager : MonoBehaviour
{
    private Camera mainCam;

    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private Transform trsPlayer;
    private Bounds bounds;

    private void Start()
    {
        mainCam = Camera.main;

        checkBound();
    }

    private void checkBound()
    {
        float height = mainCam.orthographicSize;
        //aspect = width / height
        float width = height * mainCam.aspect;

        bounds = boxCollider2D.bounds;

        float minX = bounds.min.x + width;
        float maxX = bounds.extents.x - width;

        float minY = bounds.min.y + height;
        float maxY = bounds.extents.y - height;

        bounds.SetMinMax(new Vector3(minX, minY), new Vector3(maxX, maxY));
    }

    private void Update()
    {
        if (trsPlayer == null)
        {
            return;
        }

        Vector3 curPos = mainCam.transform.position;
        curPos.x = trsPlayer.position.x;
        curPos.y = trsPlayer.position.y;

        mainCam.transform.position = new Vector3(Mathf.Clamp(curPos.x, bounds.min.x, bounds.max.x),
            Mathf.Clamp(curPos.y, bounds.min.y, bounds.max.y), curPos.z);
    }
}
