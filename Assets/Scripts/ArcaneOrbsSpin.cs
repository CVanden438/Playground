using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneOrbsSpin : MonoBehaviour
{
    public float spinSpeed = 100f;
    public SpellSO data;

    private void Update()
    {
        // Rotate the GameObject around the Z-axis
        transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
    }
}
