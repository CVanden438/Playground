using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    public Camera mainCam;
    public GameObject weapon;
    private Vector3 mousePosition;
    private float angle;

    void Update()
    {
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - weapon.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
