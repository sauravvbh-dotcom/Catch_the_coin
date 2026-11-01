// ============================================================
// Project   : Catch the Coin
// Script    : CoinSpawner.cs
// Purpose   : Spawns coin prefabs at a configurable interval at
//             random horizontal positions above the screen.
//             Stops spawning after Game Over.
// Author    : Saurabh Singh Airee
// GitHub    : https://github.com/sauravvbh-dotcom/Catch_the_coin
// Created   : 01-Nov-2026
// ============================================================

using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // ---------------------------------------------------------
    // Inspector Fields
    // ---------------------------------------------------------

    [Header("Coin Prefab")]
    [Tooltip("Drag the Coin prefab (with CoinController attached) here.")]
    public GameObject coinPrefab;

    [Header("Spawn Settings")]
    [Tooltip("Seconds between each coin spawn.")]
    public float spawnInterval = 1.2f;

    [Tooltip("Y position where coins appear (above the visible screen).")]
    public float spawnY = 6f;

    [Tooltip("Leftmost X boundary for random spawn positions.")]
    public float minX = -7f;

    [Tooltip("Rightmost X boundary for random spawn positions.")]
    public float maxX = 7f;

    // ---------------------------------------------------------
    // Private State
    // ---------------------------------------------------------

    private float timer;

    // ---------------------------------------------------------
    // Unity Lifecycle
    // ---------------------------------------------------------

    void Start()
    {
        // Start the timer offset slightly so the first coin
        // doesn't appear the very instant the game loads
        timer = spawnInterval * 0.5f;
    }

    void Update()
    {
        // Don't spawn while the game is paused (i.e. after game over)
        if (Time.timeScale == 0f) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnCoin();
        }
    }

    // ---------------------------------------------------------
    // Private Methods
    // ---------------------------------------------------------

    /// <summary>
    /// Instantiates a coin at a random X position just above the screen.
    /// </summary>
    private void SpawnCoin()
    {
        if (coinPrefab == null)
        {
            Debug.LogWarning("[CoinSpawner] No coin prefab assigned!");
            return;
        }

        // Pick a random horizontal position within the defined range
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(randomX, spawnY);

        Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
    }
}
