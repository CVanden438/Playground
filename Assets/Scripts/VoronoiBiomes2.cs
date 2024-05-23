using UnityEngine;
using System.Collections.Generic;

public class VoronoiTerrain : MonoBehaviour
{
    public int width = 50;
    public int height = 50;
    public int numCells = 20;
    public float cellSize = 5f;

    private float[,] noiseMap;
    private int[,] biomeMap;
    private List<Vector2> cellCenters;

    void Start()
    {
        GenerateVoronoiTerrain();
        CreateTiles();
    }

    void GenerateVoronoiTerrain()
    {
        noiseMap = new float[width, height];
        biomeMap = new int[width, height];
        cellCenters = new List<Vector2>();

        // Generate Voronoi cell centers
        for (int i = 0; i < numCells; i++)
        {
            float x = Random.Range(0f, width);
            float y = Random.Range(0f, height);
            cellCenters.Add(new Vector2(x, y));
        }

        // Compute Voronoi diagram
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float minDistance = float.MaxValue;
                int closestCellIndex = -1;

                for (int i = 0; i < cellCenters.Count; i++)
                {
                    float distance = Vector2.Distance(new Vector2(x, y), cellCenters[i]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestCellIndex = i;
                    }
                }

                noiseMap[x, y] = minDistance;
                biomeMap[x, y] = closestCellIndex;
            }
        }
    }

    void CreateTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int biomeIndex = biomeMap[x, y];
                CreateTile(x, y, biomeIndex);
            }
        }
    }

    void CreateTile(int x, int y, int biomeIndex)
    {
        GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Quad);
        tile.transform.position = new Vector3(x * cellSize, y * cellSize, 0f);
        tile.transform.localScale = new Vector3(cellSize, cellSize, 1f);
        tile.GetComponent<Renderer>().material.color = GetBiomeColor(biomeIndex);
    }

    Color GetBiomeColor(int biomeIndex)
    {
        // Assign different colors to different biomes
        switch (biomeIndex)
        {
            case 0:
                return Color.green;
            case 1:
                return Color.yellow;
            case 2:
                return Color.blue;
            case 3:
                return Color.red;
            default:
                return Color.grey;
        }
    }
}
