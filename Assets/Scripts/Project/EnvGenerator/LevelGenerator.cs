using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject chunkPrefab;
    [SerializeField]
    private int startingChunksAmount = 12;
    [SerializeField]
    private Transform chunkParent;
    [SerializeField]
    private float chunkLength = 1f;
    [SerializeField]
    private float chunkMoveSpeed = 8f;

    private List<GameObject> chunks = new();

    void Start()
    {
        SpawnStartingChunks();
    }

    void Update()
    {
        MoveChunk();
    }

    private void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        float spawnPositionZ = CalculateSpawnPosition();

        Vector3 chunkSpawnPoint = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);

        GameObject newChunk = Instantiate(chunkPrefab, chunkSpawnPoint, Quaternion.identity, chunkParent);
        chunks.Add(newChunk);
    }

    private float CalculateSpawnPosition()
    {
        float spawnPositionZ;

        if (chunks.Count == 0)
        {
            spawnPositionZ = transform.position.z;
        }
        else
        {
            spawnPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        }

        return spawnPositionZ;
    }

    private void MoveChunk()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunks[i].transform.Translate(-transform.forward * (chunkMoveSpeed * Time.deltaTime));

            if(chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }
}
