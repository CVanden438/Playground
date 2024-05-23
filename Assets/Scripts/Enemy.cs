using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public interface IInteractable
{
    public void Interact();
    public void InteractionEnter(GameObject player);
    public void InteractionExit();
}

public class Enemy : MonoBehaviour, IInteractable
{
    public EnemySO data;
    public GameObject proj;
    public GameObject projPivot;
    float nextAttackTime;
    public GameObject target;
    public bool canAttack = false;

    // public EnemyAttackSO[] attacks;
    EnemyAttackSO currentAttack;
    int index = 0;
    EnemyAni ani;

    void Awake()
    {
        nextAttackTime = Time.time;
        target = GameObject.FindGameObjectWithTag("Player");
        ani = GetComponent<EnemyAni>();
    }

    void Start()
    {
        currentAttack = data.attacks[index];
    }

    void Update()
    {
        if (canAttack && Time.time >= nextAttackTime)
        {
            // if (isCircle)
            // {
            //     CreateNormalProj();
            // }
            // else
            // {
            //     CreateCircleProj();
            // }
            StartCoroutine(RepeatAttack());
            if (index == data.attacks.Length - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            currentAttack = data.attacks[index];
            nextAttackTime = Time.time + currentAttack.cooldown;
        }
    }

    void CreateCircleProj(Vector2 rot)
    {
        var go = new GameObject();
        go.transform.position = transform.position;
        go.AddComponent<EnemyProj>();
        if (currentAttack.circleRotate)
        {
            go.AddComponent<Rotate>();
        }
        var angle = 360 / currentAttack.circleCount;
        for (int i = 0; i < currentAttack.circleCount; i++)
        {
            var newProj = Instantiate(projPivot, go.transform);
            newProj.transform.GetChild(0).Translate(0, currentAttack.circleSize, 0);
            newProj.transform.Rotate(new Vector3(0, 0, angle * i));
        }
        var enemyProj = go.GetComponent<EnemyProj>();
        enemyProj.direction = rot;
        enemyProj.range = currentAttack.range;
        enemyProj.duration = currentAttack.duration;
        enemyProj.size = currentAttack.size;
        enemyProj.data = data;
    }

    void CreateNormalProj(Vector2 rot)
    {
        var newProj = Instantiate(proj, transform.position, Quaternion.identity);
        var enemyProj = newProj.GetComponent<EnemyProj>();
        enemyProj.direction = rot;
        enemyProj.range = currentAttack.range;
        enemyProj.duration = currentAttack.duration;
        enemyProj.size = currentAttack.size;
        enemyProj.data = data;
    }

    void SetupProj()
    {
        int angleOffset = 0;
        var baseDirection = target.transform.position - transform.position;
        for (int i = 0; i < currentAttack.projCount; i++)
        {
            angleOffset *= -1;
            if ((i + 1) % 2 == 0 && i != 0)
            {
                angleOffset += currentAttack.angleBetweenProj;
            }
            Quaternion rotation = Quaternion.Euler(0f, 0f, angleOffset);
            Vector2 rotatedDirection = rotation * baseDirection;
            if (currentAttack.isCircle)
            {
                CreateCircleProj(rotatedDirection);
            }
            else
            {
                CreateNormalProj(rotatedDirection);
            }
        }
    }

    IEnumerator RepeatAttack()
    {
        for (var i = 0; i <= currentAttack.repeat; i++)
        {
            SetupProj();
            yield return new WaitForSeconds(currentAttack.repeatDelay);
        }
    }

    public void Interact()
    {
        Debug.Log("interaction");
    }

    public void InteractionEnter(GameObject player) { }

    public void InteractionExit() { }

    void OnDestroy()
    {
        EnemyManager.instance.currentEnemies.Remove(gameObject);
    }
}



// Curve Type
