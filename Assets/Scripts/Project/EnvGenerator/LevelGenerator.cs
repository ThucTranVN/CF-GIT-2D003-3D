using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager for procedurally generating an endless runner level.
/// Spawns, moves, and recycles level chunks to create a continuous gameplay experience.
/// </summary>
public class LevelGenerator : BaseManager<LevelGenerator>
{
    [Header("Chunk Prefabs")]
    [SerializeField]
    /// <summary>
    /// Special chunk prefab that contains a checkpoint.
    /// </summary>
    private GameObject checkpointChunkPrefab;
    
    [SerializeField]
    /// <summary>
    /// Array of regular chunk prefabs to randomly spawn.
    /// </summary>
    private GameObject[] chunkPrefabs;
    
    [Header("Spawn Settings")]
    [SerializeField]
    /// <summary>
    /// Number of chunks to spawn at the start of the game.
    /// </summary>
    private int startingChunksAmount = 12;
    
    [SerializeField]
    /// <summary>
    /// Interval at which checkpoint chunks are spawned (every N chunks).
    /// </summary>
    private int checkpointChunkInterval = 8;
    
    [SerializeField]
    /// <summary>
    /// Parent transform to organize spawned chunks in the hierarchy.
    /// </summary>
    private Transform chunkParent;
    
    [SerializeField]
    /// <summary>
    /// Length of each chunk in world units (used for spacing).
    /// </summary>
    private float chunkLength = 1f;
    
    [Header("Movement Settings")]
    [SerializeField]
    /// <summary>
    /// Base speed at which chunks move towards the camera.
    /// </summary>
    private float chunkMoveSpeed = 8f;
    
    [SerializeField]
    /// <summary>
    /// Minimum allowed movement speed (prevents chunks from moving too slowly).
    /// </summary>
    private float minMoveSpeed = 2f;

    /// <summary>
    /// List of all currently active chunk GameObjects.
    /// </summary>
    private List<GameObject> chunks = new();
    
    /// <summary>
    /// Total number of chunks spawned since game start (used for checkpoint calculation).
    /// </summary>
    private int chunksSpawned;

    /// <summary>
    /// Spawns the initial set of chunks to start the level.
    /// </summary>
    void Start()
    {
        SpawnStartingChunks();
    }

    /// <summary>
    /// Moves all chunks and handles recycling of chunks that have passed the camera.
    /// </summary>
    void Update()
    {
        MoveChunk();
    }

    /// <summary>
    /// Changes the chunk movement speed and adjusts related systems (gravity, camera FOV).
    /// Called when player reaches a checkpoint to increase difficulty.
    /// </summary>
    /// <param name="speedAmount">Amount to add to the current chunk move speed.</param>
    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        // Increase movement speed
        chunkMoveSpeed += speedAmount;

        // Ensure speed doesn't go below minimum
        if(chunkMoveSpeed < minMoveSpeed)
        {
            chunkMoveSpeed = minMoveSpeed;
        }

        // Adjust physics gravity in Z direction to match increased speed
        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z - speedAmount);

        // Adjust camera FOV for speed effect
        if (CameraController.HasInstance)
        {
            CameraController.Instance.ChangeCameraFOV(speedAmount);
        }
    }

    /// <summary>
    /// Spawns the initial set of chunks to populate the starting area.
    /// </summary>
    private void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunk();
        }
    }

    /// <summary>
    /// Spawns a single chunk at the appropriate position.
    /// Chooses between regular and checkpoint chunks based on spawn count.
    /// </summary>
    private void SpawnChunk()
    {
        float spawnPositionZ = CalculateSpawnPosition();
        Vector3 chunkSpawnPoint = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
        GameObject chunkToSpawn = ChooseChunkToSpawn();
        GameObject newChunkGo = Instantiate(chunkToSpawn, chunkSpawnPoint, Quaternion.identity, chunkParent);
        chunks.Add(newChunkGo);
        chunksSpawned++;
    }

    /// <summary>
    /// Chooses which chunk prefab to spawn based on checkpoint interval.
    /// </summary>
    /// <returns>The GameObject prefab to instantiate.</returns>
    private GameObject ChooseChunkToSpawn()
    {
        GameObject chunkToSpawn;

        // Spawn checkpoint chunk at regular intervals
        if (chunksSpawned % checkpointChunkInterval == 0 && chunksSpawned != 0)
        {
            chunkToSpawn = checkpointChunkPrefab;
        }
        else
        {
            // Spawn a random regular chunk
            chunkToSpawn = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        }

        return chunkToSpawn;
    }

    /// <summary>
    /// Calculates the Z position where the next chunk should spawn.
    /// </summary>
    /// <returns>The Z position for the new chunk.</returns>
    private float CalculateSpawnPosition()
    {
        float spawnPositionZ;

        // First chunk spawns at generator position
        if (chunks.Count == 0)
        {
            spawnPositionZ = transform.position.z;
        }
        else
        {
            // Subsequent chunks spawn after the last chunk
            spawnPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        }

        return spawnPositionZ;
    }

    /// <summary>
    /// Moves all chunks towards the camera and recycles chunks that have passed behind the camera.
    /// When a chunk is destroyed, a new one is spawned to maintain continuous gameplay.
    /// </summary>
    private void MoveChunk()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            // Move chunk backwards (towards camera)
            chunks[i].transform.Translate(-transform.forward * (chunkMoveSpeed * Time.deltaTime));

            // Check if chunk has passed behind the camera
            if(chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                // Remove and destroy the old chunk
                chunks.Remove(chunk);
                Destroy(chunk);
                // Spawn a new chunk to replace it
                SpawnChunk();
            }
        }
    }
}
