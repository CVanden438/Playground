using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellCasting : MonoBehaviour
{
    public List<SpellSO> spells;
    Camera mainCam;
    float nextCastTime;
    Player player;
    int manaDrain = 0;
    List<SpellSO> activeSpells = new();

    void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        nextCastTime = Time.time;
        player = GetComponent<Player>();
    }

    void Start()
    {
        StartCoroutine(RemoveMana());
    }

    void Update()
    {
        if (
            Time.time >= nextCastTime
            && !EventSystem.current.IsPointerOverGameObject()
            && !InventoryManager.instance.isDragging
        )
        {
            // if (Input.GetKey(KeyCode.Mouse0))
            // {
            //     CastSpell(0);
            // }
            // if (Input.GetKey(KeyCode.Mouse1))
            // {
            //     CastSpell(1);
            // }
            if (Input.GetKey(KeyCode.Alpha1))
            {
                CastSpell(2);
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                CastSpell(3);
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                CastSpell(4);
            }
            if (Input.GetKey(KeyCode.Alpha4))
            {
                CastSpell(5);
            }
        }
    }

    void CastSpell(int spellNum)
    {
        var spell = spells[spellNum];
        if (player.mana >= spell.manaCost)
        {
            var pos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            pos = SpellHelpers.ClampMaxRange(transform.position, pos, spell.range);
            var spellToCast = spell.prefab.GetComponent<ISpell>();
            if (spell.repeat == 0)
            {
                spellToCast.Cast(transform.position, pos, gameObject);
            }
            else
            {
                StartCoroutine(RepeatSpell(spellToCast, pos, spell.repeat));
            }
            nextCastTime = Time.time + spell.cooldown;
            if (spell.spellType == SpellType.single)
            {
                player.UpdateMana(-spell.manaCost);
            }
            else
            {
                if (!activeSpells.Contains(spell))
                {
                    activeSpells.Add(spell);
                    manaDrain += spell.manaCost;
                }
                else
                {
                    activeSpells.Remove(spell);
                    manaDrain -= spell.manaCost;
                }
            }
        }
    }

    IEnumerator RepeatSpell(ISpell spell, Vector3 pos, int amount)
    {
        for (var i = 0; i <= amount; i++)
        {
            spell.Cast(transform.position, pos, gameObject);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator RemoveMana()
    {
        while (true)
        {
            player.UpdateMana(-manaDrain);
            if (player.mana <= 0)
            {
                ClearActiveSpells();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void ClearActiveSpells()
    {
        foreach (var spell in activeSpells)
        {
            spell.prefab.GetComponent<ISpell>().Cast(transform.position, new Vector3(), gameObject);
        }
        activeSpells.Clear();
        manaDrain = 0;
    }
}
