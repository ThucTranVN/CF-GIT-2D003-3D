using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] obstaclePrefabs;
    [SerializeField]
    private float obstacleSpawnTime = 1f;
    [SerializeField]
    private Transform obstacleParent;
    private int obstaclesSpawned = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnObstacleRoutine());
    }

    private IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            yield return new WaitForSeconds(obstacleSpawnTime);
            Instantiate(obstaclePrefab, obstacleParent.position, Random.rotation, obstacleParent);
            obstaclesSpawned++;
        }
    }
}
