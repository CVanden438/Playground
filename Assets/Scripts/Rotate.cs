using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Tween tween;

    void Start()
    {
        StartCoroutine(RotateAround(3));
        // tween = transform
        //     .DORotate(new Vector3(0, 0, 360), 3, RotateMode.FastBeyond360)
        //     .SetLoops(-1);
    }

    IEnumerator RotateAround(float duration)
    {
        Quaternion startRot = transform.rotation;
        float t = 0.0f;
        while (t < 100)
        {
            t += Time.deltaTime;
            transform.rotation =
                startRot * Quaternion.AngleAxis(t / duration * 360f, Vector3.forward); //or transform.right if you want it to be locally based
            yield return null;
        }
        transform.rotation = startRot;
    }
}
