using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxMana = 100;
    public int mana = 100;
    public Image manaBar;
    public LayerMask interactionLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var hits = Physics2D.RaycastAll(
                transform.position,
                Vector2.zero,
                999,
                interactionLayer
            );
            if (hits.Length != 0)
            {
                hits[0].transform.GetComponentInParent<IInteractable>().Interact();
            }
        }
    }

    public void UpdateMana(int amount)
    {
        mana += amount;
        manaBar.fillAmount = (float)mana / maxMana;
    }
}
