using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemytoggle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            Debug.Log("test");
        {
            other.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
