using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneOrbsSpell : MonoBehaviour, ISpell
{
    public GameObject center;
    GameObject activeOrbs;

    public void Cast(Vector3 spawnPos, Vector3 targetPos, GameObject caster)
    {
        if (!activeOrbs)
        {
            activeOrbs = Instantiate(center, caster.transform);
        }
        else
        {
            Destroy(activeOrbs);
        }
    }
}
