using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "EnemyAttack")]
public class EnemyAttackSO : ScriptableObject
{
    public int repeat = 0;
    public float repeatDelay = 0.3f;
    public int projCount = 1;
    public int angleBetweenProj;
    public float range = 5;
    public float duration = 5;
    public float cooldown = 1;
    public float size = 1;

    [Header("Circle")]
    public bool isCircle = false;
    public bool circleRotate = false;
    public float circleSize = 1;
    public int circleCount;
}
