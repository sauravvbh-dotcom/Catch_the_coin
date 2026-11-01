// ============================================================
// Project   : Catch the Coin
// Script    : CoinController.cs
// Purpose   : Controls the falling behaviour of each coin.
//             If a coin exits the bottom of the screen without
//             being caught, the player loses one life.
// Author    : Saurabh Singh Airee
// GitHub    : https://github.com/sauravvbh-dotcom/Catch_the_coin
// Created   : 01-Nov-2026
// ============================================================

using UnityEngine;

public class CoinController : MonoBehaviour
{
    // ---------------------------------------------------------
    // Inspector Fields
    // ---------------------------------------------------------

    [Header("Falling Speed")]
    [Tooltip("How fast the coin falls downward (units/sec).")]
    public float fallSpeed = 4f;

    [Header("Despawn Boundary")]
    [Tooltip("Y position below which the coin is considered missed.")]
    public float despawnY = -6f;

    // ---------------------------------------------------------
    // Unity Lifecycle
    // ---------------------------------------------------------

    void Update()
    {
        Fall();
        CheckMissed();
    }

    // ---------------------------------------------------------
    // Private Methods
    // ---------------------------------------------------------

    /// <summary>
    /// Moves the coin straight downward every frame.
    /// </summary>
    private void Fall()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Checks whether the coin has fallen below the screen.
    /// If so, notifies the GameManager and destroys the coin.
    /// </summary>
    private void CheckMissed()
    {
        if (transform.position.y < despawnY)
        {
            // Tell the game manager the player missed this coin
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoseLife();
            }

            Destroy(gameObject);
        }
    }

    // ---------------------------------------------------------
    // Trigger Detection (called when basket hits this coin)
    // ---------------------------------------------------------

    /// <summary>
    /// Called by Unity when another collider overlaps this trigger.
    /// The basket catching the coin is detected here.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only react when the basket catches the coin
        if (other.CompareTag("Player"))
        {
            // Award score via the GameManager singleton
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(1);
            }

            Destroy(gameObject);
        }
    }
}
