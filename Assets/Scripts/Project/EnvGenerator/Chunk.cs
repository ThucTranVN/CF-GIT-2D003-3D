using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Level chunk component that procedurally spawns obstacles and collectibles.
/// Manages lane-based spawning of fences, apples, and coins with probability-based placement.
/// </summary>
public class Chunk : MonoBehaviour
{
    [Header("Prefab References")]
    [SerializeField]
    /// <summary>
    /// Prefab for fence obstacles.
    /// </summary>
    private GameObject fencePrefab;
    
    [SerializeField]
    /// <summary>
    /// Prefab for apple collectibles (time extension items).
    /// </summary>
    private GameObject applePrefab;
    
    [SerializeField]
    /// <summary>
    /// Prefab for coin collectibles (score items).
    /// </summary>
    private GameObject coinPrefab;
    
    [Header("Spawn Settings")]
    [SerializeField]
    /// <summary>
    /// Probability chance (0-1) for spawning an apple in this chunk.
    /// </summary>
    private float appleSpawnChance = 0.3f;
    
    [SerializeField]
    /// <summary>
    /// Probability chance (0-1) for spawning coins in this chunk.
    /// </summary>
    private float coinSpawnChance = 0.8f;
    
    [SerializeField]
    /// <summary>
    /// Distance between consecutive coins when spawning a coin trail.
    /// </summary>
    private float coinSeperationLength = 2f;
    
    [SerializeField]
    /// <summary>
    /// Array of X positions representing the three lanes (-2, 0, 2).
    /// </summary>
    private float[] lanes = { -2f, 0, 2f };

    /// <summary>
    /// List of available lane indices that haven't been used yet.
    /// Used to prevent overlapping spawns in the same lane.
    /// </summary>
    private List<int> availableLanes = new List<int> { 0, 1, 2 };

    /// <summary>
    /// Spawns all chunk elements (fences, apples, coins) when the chunk is created.
    /// </summary>
    void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }

    /// <summary>
    /// Spawns a random number of fences in random lanes.
    /// Fences block lanes and must be avoided by the player.
    /// </summary>
    private void SpawnFences()
    {
        // Random number of fences (0 to number of lanes)
        int fencesToSpawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < fencesToSpawn; i++)
        {
            // Stop if no lanes are available
            if (availableLanes.Count <= 0) break;

            // Select and remove a lane from available lanes
            int selectedLane = SelectLanes();
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }

    /// <summary>
    /// Spawns an apple collectible based on spawn chance if lanes are available.
    /// Apples extend game time when collected.
    /// </summary>
    private void SpawnApple()
    {
        // Check spawn chance and lane availability
        if (Random.value > appleSpawnChance || availableLanes.Count <= 0) return;

        // Select a lane and spawn apple
        int selectedLane = SelectLanes();
        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        Instantiate(applePrefab, spawnPosition, Quaternion.identity, this.transform);
    }

    /// <summary>
    /// Spawns a trail of coins in a single lane based on spawn chance.
    /// Coins are spaced evenly along the chunk for collection.
    /// </summary>
    private void SpawnCoins()
    {
        // Check spawn chance and lane availability
        if (Random.value > coinSpawnChance || availableLanes.Count <= 0) return;

        // Select a lane for the coin trail
        int selectedLane = SelectLanes();

        // Determine how many coins to spawn
        int maxCoinToSpawn = 20;
        int coinsToSpawn = Random.Range(1, maxCoinToSpawn);
        
        // Calculate starting Z position (top of chunk)
        float topOfChunkZPos = transform.position.z + (coinSeperationLength * 2f);

        // Spawn coins in a line along the selected lane
        for (int i = 0; i < coinsToSpawn; i++)
        {
            float spawnPositionZ = topOfChunkZPos - (i * coinSeperationLength);
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPositionZ);
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }

    /// <summary>
    /// Selects a random lane from available lanes and removes it from the list.
    /// Ensures each lane is only used once per chunk.
    /// </summary>
    /// <returns>The index of the selected lane.</returns>
    private int SelectLanes()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        // Remove selected lane to prevent reuse
        availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }
}
