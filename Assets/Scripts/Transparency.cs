using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Transparency : MonoBehaviour
{
    public float transparencyAmount = 0.5f;
    public LayerMask transparentLayer;
    RaycastHit2D transparentObject;

    //COULD ALSO JUST DO ONCOLLISIONENTER/EXIT
    private void Update()
    {
        Vector2 direction = transform.position - Camera.main.transform.position;
        RaycastHit2D[] hits = Physics2D.RaycastAll(
            transform.position,
            direction,
            direction.magnitude,
            transparentLayer
        );
        if (hits.Length != 0)
        {
            if (transparentObject)
            {
                return;
            }
            SpriteRenderer spriteRenderer = hits[0].collider.GetComponentInParent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color objColor = spriteRenderer.color;
                objColor.a = transparencyAmount;
                spriteRenderer.color = objColor;
                transparentObject = hits[0];
            }
        }
        else
        {
            if (transparentObject)
            {
                var sr = transparentObject.transform.GetComponentInParent<SpriteRenderer>();
                var newColor = sr.color;
                newColor.a = 1;
                sr.color = newColor;
                transparentObject = new();
            }
        }
    }
}
