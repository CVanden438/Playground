using UnityEngine;
using System.Collections.Generic;

public class VoronoiBiomeGenerator : MonoBehaviour
{
    public int mapWidth = 100;
    public int mapHeight = 100;
    public int numBiomes = 5;
    public float noiseScale = 0.1f;
    public GameObject tilePrefab;

    private Dictionary<Vector2Int, GameObject> tiles = new Dictionary<Vector2Int, GameObject>();
    private List<Color> biomeColors = new List<Color>();

    private void Start()
    {
        GenerateBiomeColors();
        GenerateVoronoiDiagram();
    }

    private void GenerateBiomeColors()
    {
        for (int i = 0; i < numBiomes; i++)
        {
            biomeColors.Add(new Color(Random.value, Random.value, Random.value));
        }
    }

    public void GenerateVoronoiDiagram()
    {
        List<Vector2Int> biomeSeeds = new List<Vector2Int>();

        for (int i = 0; i < numBiomes; i++)
        {
            int x = Random.Range(0, mapWidth);
            int y = Random.Range(0, mapHeight);
            biomeSeeds.Add(new Vector2Int(x, y));
        }

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Vector2Int tilePos = new Vector2Int(x, y);
                float minDistance = float.MaxValue;
                int closestBiomeIndex = 0;

                for (int i = 0; i < numBiomes; i++)
                {
                    float distance = Vector2Int.Distance(tilePos, biomeSeeds[i]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestBiomeIndex = i;
                    }
                }

                GameObject tile = Instantiate(
                    tilePrefab,
                    new Vector3(x, y, 0),
                    Quaternion.identity
                );
                tile.GetComponent<SpriteRenderer>().color = biomeColors[closestBiomeIndex];
                tiles[tilePos] = tile;
            }
        }

        // ApplyNoise();
    }

    private void ApplyNoise()
    {
        foreach (var tile in tiles)
        {
            Vector2Int tilePos = tile.Key;
            float noiseValue = Mathf.PerlinNoise(tilePos.x * noiseScale, tilePos.y * noiseScale);
            Color tileColor = tile.Value.GetComponent<SpriteRenderer>().color;
            tileColor *= noiseValue;
            tile.Value.GetComponent<SpriteRenderer>().color = tileColor;
        }
    }
}
