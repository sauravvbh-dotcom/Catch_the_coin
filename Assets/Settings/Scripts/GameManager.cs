// ============================================================
// Project   : Catch the Coin
// Script    : GameManager.cs
// Purpose   : Singleton that tracks score and lives, updates the
//             HUD (ScoreText, LivesText), and triggers the
//             Game Over panel when lives reach zero.
// Author    : Saurabh Singh Airee
// GitHub    : https://github.com/sauravvbh-dotcom/Catch_the_coin
// Created   : 01-Nov-2026
// ============================================================

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // ---------------------------------------------------------
    // Singleton
    // ---------------------------------------------------------

    /// <summary>Global access point for the GameManager instance.</summary>
    public static GameManager Instance { get; private set; }

    // ---------------------------------------------------------
    // Inspector Fields
    // ---------------------------------------------------------

    [Header("HUD References")]
    [Tooltip("TextMeshPro component that displays the current score.")]
    public TMP_Text scoreText;

    [Tooltip("TextMeshPro component that displays remaining lives.")]
    public TMP_Text livesText;

    [Header("Game Over UI")]
    [Tooltip("Panel shown when the player runs out of lives.")]
    public GameObject gameOverPanel;

    [Header("Starting Values")]
    [Tooltip("Number of lives the player starts with.")]
    public int startingLives = 3;

    // ---------------------------------------------------------
    // Private State
    // ---------------------------------------------------------

    private int score;
    private int lives;
    private bool isGameOver;

    // ---------------------------------------------------------
    // Unity Lifecycle
    // ---------------------------------------------------------

    void Awake()
    {
        // Enforce singleton — destroy duplicate GameManagers
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        // Initialise game state
        score       = 0;
        lives       = startingLives;
        isGameOver  = false;

        // Hide the game-over panel at startup
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateHUD();
    }

    // ---------------------------------------------------------
    // Public API (called by CoinController)
    // ---------------------------------------------------------

    /// <summary>
    /// Adds <paramref name="amount"/> to the score and refreshes the HUD.
    /// Has no effect after game over.
    /// </summary>
    public void AddScore(int amount)
    {
        if (isGameOver) return;

        score += amount;
        UpdateHUD();
    }

    /// <summary>
    /// Subtracts one life.  Triggers Game Over when lives hit zero.
    /// Has no effect after game over.
    /// </summary>
    public void LoseLife()
    {
        if (isGameOver) return;

        lives--;
        UpdateHUD();

        if (lives <= 0)
        {
            TriggerGameOver();
        }
    }

    // ---------------------------------------------------------
    // Private Helpers
    // ---------------------------------------------------------

    /// <summary>Refreshes the score and lives text on the HUD.</summary>
    private void UpdateHUD()
    {
        if (scoreText != null)
            scoreText.text = "SCORE : " + score;

        if (livesText != null)
            livesText.text = "Lives: " + lives;
    }

    /// <summary>
    /// Freezes the game, shows the Game Over panel, and stops time.
    /// </summary>
    private void TriggerGameOver()
    {
        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;
    }
}
