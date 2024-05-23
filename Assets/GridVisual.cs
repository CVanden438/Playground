using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisual : MonoBehaviour
{
    public Camera mainCam;

    void Update()
    {
        var point = mainCam.ScreenToWorldPoint(Input.mousePosition);
        var clampedPos = new Vector2(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y));
        transform.position = clampedPos;
    }
}
