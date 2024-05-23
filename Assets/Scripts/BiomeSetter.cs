using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Biomes
{
    forest,
    tundra,
    swamp,
    taiga,
    desert,
    ash,
    castle,
    mushroom,
    elfheim
}

public class BiomeSetter : MonoBehaviour
{
    public Tilemap forest;
    public Tilemap tundra;
    public Tilemap swamp;
    public Tilemap taiga;
    public Tilemap desert;
    public Tilemap ash;
    public Tilemap castle;
    public BiomeSO forestBiome;
    public BiomeSO tundraBiome;
    public BiomeSO swampBiome;
    public BiomeSO taigaBiome;
    public BiomeSO desertBiome;
    public BiomeSO ashBiome;
    public BiomeSO castleBiome;
    public BiomeSO currentBiome;

    void Update()
    {
        var pos = Vector3Int.FloorToInt(transform.position);
        if (forest.HasTile(pos))
        {
            currentBiome = forestBiome;
            return;
        }
        if (tundra.HasTile(pos))
        {
            currentBiome = tundraBiome;
            return;
        }
        if (swamp.HasTile(pos))
        {
            currentBiome = swampBiome;
            return;
        }
        if (taiga.HasTile(pos))
        {
            currentBiome = taigaBiome;
            return;
        }
        if (desert.HasTile(pos))
        {
            currentBiome = desertBiome;
            return;
        }
        if (ash.HasTile(pos))
        {
            currentBiome = ashBiome;
            return;
        }
        EnemyManager.instance.currentBiome = currentBiome;
    }
}
