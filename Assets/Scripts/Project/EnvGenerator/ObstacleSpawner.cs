using System.Collections;
using UnityEngine;

/// <summary>
/// Spawns obstacles at regular intervals in random positions across a width range.
/// Used for spawning falling obstacles or environmental hazards.
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// Array of obstacle prefabs to randomly choose from when spawning.
    /// </summary>
    private GameObject[] obstaclePrefabs;
    
    [SerializeField]
    /// <summary>
    /// Time interval (in seconds) between obstacle spawns.
    /// </summary>
    private float obstacleSpawnTime = 1f;
    
    [SerializeField]
    /// <summary>
    /// Parent transform to organize spawned obstacles in the hierarchy.
    /// </summary>
    private Transform obstacleParent;
    
    [SerializeField]
    /// <summary>
    /// Half-width of the spawn area. Obstacles spawn between -spawnWidth and +spawnWidth on X axis.
    /// </summary>
    private float spawnWidth = 4f;

    /// <summary>
    /// Starts the coroutine that continuously spawns obstacles.
    /// </summary>
    void Start()
    {
        StartCoroutine(SpawnObstacleRoutine());
    }

    /// <summary>
    /// Coroutine that spawns obstacles at regular intervals.
    /// Spawns obstacles at random X positions within the spawn width.
    /// </summary>
    private IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            // Select a random obstacle prefab
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            // Calculate random spawn position within the width range
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), obstacleParent.transform.position.y, obstacleParent.transform.position.z);
            // Wait for the spawn interval
            yield return new WaitForSeconds(obstacleSpawnTime);
            // Spawn the obstacle with random rotation
            Instantiate(obstaclePrefab, spawnPosition, Random.rotation, obstacleParent);
        }
    }
}
