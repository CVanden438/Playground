using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public int health;
    public float moveSpeed;
    public float waitDuration;
    public float damage;
    public float spawnRarity = 1;
    public Sprite[] idleSprites;
    public Sprite[] moveSprites;
    public EnemyAttackSO[] attacks;
    public DropTableSO dropTable;
    public GameObject prefab;
}
