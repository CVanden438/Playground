using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoronoiBiomes : MonoBehaviour
{
    public int gridSizeX = 20;
    public int gridSizeY = 20;
    public int numBiomes = 5;
    public GameObject[] biomeTiles;

    private Vector2[] points;

    void Start()
    {
        GenerateBiomes();
    }

    void GenerateBiomes()
    {
        points = new Vector2[numBiomes];

        for (int i = 0; i < numBiomes; i++)
        {
            points[i] = new Vector2(Random.Range(0, gridSizeX), Random.Range(0, gridSizeY));
        }

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2Int cell = new Vector2Int(x, y);
                int closestPointIndex = GetClosestPointIndex(cell);

                if (biomeTiles.Length > closestPointIndex)
                {
                    Instantiate(
                        biomeTiles[closestPointIndex],
                        new Vector3(x, y, 0),
                        Quaternion.identity
                    );
                }
            }
        }
    }

    int GetClosestPointIndex(Vector2Int cell)
    {
        float minDistance = float.MaxValue;
        int closestPointIndex = 0;

        for (int i = 0; i < numBiomes; i++)
        {
            float distance = Vector2.Distance(points[i], cell);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestPointIndex = i;
            }
        }

        return closestPointIndex;
    }
}
