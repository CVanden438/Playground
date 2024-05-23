using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biome", menuName = "Biome")]
public class BiomeSO : ScriptableObject
{
    public Biomes biomeName;
    public List<EnemySO> enemies;
    public float spawnRate;
    public int maxSpawns;
}
