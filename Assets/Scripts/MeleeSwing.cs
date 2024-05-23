using System.Collections;
using System.Collections.Generic;

using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class MeleeSwing : MonoBehaviour
{
    public Camera mainCam;
    public GameObject meleeWeaponPivot;
    public GameObject meleeWeapon;
    public GameObject rangedWeapon;
    public GameObject meleeProj;
    public GameObject rangeProj;
    private Vector3 mousePosition;
    private float angle;
    public float rotationDuration = 1;
    int swap = 1;
    public WeaponSO wepData;
    float nextAttackTime;
    SpriteRenderer meleeSprite;
    SpriteRenderer rangedSprite;

    // Vector2 direction;

    void Awake()
    {
        nextAttackTime = Time.time;
        meleeSprite = meleeWeapon.GetComponent<SpriteRenderer>();
        rangedSprite = rangedWeapon.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (wepData && wepData.weaponType == WeaponType.melee)
        {
            meleeSprite.sprite = wepData.sprite;
            meleeWeaponPivot.SetActive(true);
            mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - meleeWeaponPivot.transform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            meleeWeaponPivot.transform.rotation = Quaternion.AngleAxis(
                angle - 180,
                Vector3.forward
            );
            if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextAttackTime)
            {
                meleeWeaponPivot.transform
                    .DORotate(new Vector3(0, 0, 360 * swap), 0.5f, RotateMode.LocalAxisAdd)
                    .SetEase(Ease.InOutCubic);
                swap *= -1;
                if (swap == -1)
                {
                    meleeWeapon.GetComponent<SpriteRenderer>().flipY = true;
                }
                else
                {
                    meleeWeapon.GetComponent<SpriteRenderer>().flipY = false;
                }
                if (wepData.repeat == 0)
                {
                    SetupRangeProj(direction, angle, transform.position);
                }
                else
                {
                    StartCoroutine(
                        RepeatAttack(wepData.repeat, direction, angle, transform.position)
                    );
                }
            }
        }
        if (wepData && wepData.weaponType == WeaponType.range)
        {
            rangedSprite.sprite = wepData.sprite;
            rangedWeapon.SetActive(true);
            mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - rangedWeapon.transform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rangedWeapon.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextAttackTime)
            {
                if (wepData.repeat == 0)
                {
                    SetupRangeProj(direction, angle, transform.position);
                }
                else
                {
                    StartCoroutine(
                        RepeatAttack(wepData.repeat, direction, angle, transform.position)
                    );
                }
            }
        }
    }

    IEnumerator RepeatAttack(int amount, Vector2 dir, float angle, Vector3 pos)
    {
        for (var i = 0; i <= amount; i++)
        {
            SetupRangeProj(dir, angle, pos);
            // if (wepData && wepData.weaponType == WeaponType.melee)
            // {
            //     SetupMeleeProj(dir, angle, pos);
            // }
            // else
            // {
            //     SetupRangeProj(dir, angle, pos);
            // }
            yield return new WaitForSeconds(0.1f);
        }
    }

    void SetupRangeProj(Vector2 dir, float angle, Vector3 pos)
    {
        //
        int angleOffset = 0;
        for (int i = 0; i < wepData.proj; i++)
        {
            angleOffset *= -1;
            if ((i + 1) % 2 == 0 && i != 0)
            {
                angleOffset += 20;
            }
            Quaternion rotation = Quaternion.Euler(0f, 0f, angleOffset);
            Vector2 rotatedDirection = rotation * dir;
            float rotatedAngle =
                Mathf.Atan2(rotatedDirection.y, rotatedDirection.x) * Mathf.Rad2Deg;

            //
            var proj = Instantiate(
                wepData.weaponType == WeaponType.melee ? meleeProj : rangeProj,
                pos + new Vector3(0, 0.5f),
                Quaternion.AngleAxis(
                    wepData.weaponType == WeaponType.melee ? rotatedAngle : rotatedAngle - 90,
                    Vector3.forward
                )
            );
            proj.GetComponent<MoveSlash>().direction = rotatedDirection;
            proj.GetComponent<MoveSlash>().data = wepData;
        }
        nextAttackTime = Time.time + wepData.cooldown;
    }

    void SetupMeleeProj(Vector2 dir, float angle, Vector3 pos)
    {
        var proj = Instantiate(
            meleeProj,
            pos + new Vector3(0, 0.5f),
            Quaternion.AngleAxis(angle, Vector3.forward)
        );
        proj.GetComponent<MoveSlash>().direction = dir;
        proj.GetComponent<MoveSlash>().data = wepData;
        nextAttackTime = Time.time + wepData.cooldown;
    }
}
