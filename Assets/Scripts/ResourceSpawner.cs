using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject resourcePrefab; // The prefab to spawn
    public Tilemap tilemap; // The tilemap to spawn on
    public int numberOfResourcesToSpawn = 10; // Number of resources to spawn

    void Start()
    {
        SpawnResources();
    }

    void SpawnResources()
    {
        BoundsInt bounds = tilemap.cellBounds;
        for (int i = 0; i < numberOfResourcesToSpawn; i++)
        {
            Vector3Int cellPosition = GetRandomCellPosition(bounds);
            Vector3 spawnPosition = tilemap.CellToWorld(cellPosition);

            if (IsValidSpawnPosition(cellPosition))
            {
                Instantiate(resourcePrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                i--; // Try again if the position was invalid
            }
        }
    }

    Vector3Int GetRandomCellPosition(BoundsInt bounds)
    {
        int x = Random.Range(bounds.xMin, bounds.xMax);
        int y = Random.Range(bounds.yMin, bounds.yMax);
        return new Vector3Int(x, y, 0);
    }

    bool IsValidSpawnPosition(Vector3Int cellPosition)
    {
        // Check if the cell contains a tile
        if (!tilemap.HasTile(cellPosition))
        {
            return false;
        }

        // Check if there is a collider at the position
        Vector3 worldPosition = tilemap.CellToWorld(cellPosition);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPosition, 0.1f);
        if (colliders.Length > 0)
        {
            return false;
        }

        return true;
    }
}
