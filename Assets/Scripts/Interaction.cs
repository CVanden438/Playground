using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject interactionPrompt;
    public bool showPrompt = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInParent<IInteractable>().InteractionEnter(other.gameObject);
            if(showPrompt){
            interactionPrompt.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInParent<IInteractable>().InteractionExit();
            interactionPrompt.SetActive(false);
        }
    }
}
