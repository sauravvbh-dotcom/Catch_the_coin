// ============================================================
// Project   : Catch the Coin
// Script    : BasketController.cs
// Purpose   : Handles horizontal movement of the player's basket
//             using keyboard/gamepad input and clamps it within
//             the visible screen boundaries.
// Author    : Saurabh Singh Airee
// GitHub    : https://github.com/sauravvbh-dotcom/Catch_the_coin
// Created   : 01-Nov-2026
// ============================================================

using UnityEngine;

public class BasketController : MonoBehaviour
{
    // ---------------------------------------------------------
    // Inspector Fields
    // ---------------------------------------------------------

    [Header("Movement")]
    [Tooltip("How fast the basket moves left and right (units/sec).")]
    public float speed = 8f;

    [Header("Screen Bounds")]
    [Tooltip("Maximum X position the basket can reach on either side.")]
    public float horizontalLimit = 7f;

    // ---------------------------------------------------------
    // Unity Lifecycle
    // ---------------------------------------------------------

    void Update()
    {
        HandleMovement();
    }

    // ---------------------------------------------------------
    // Private Methods
    // ---------------------------------------------------------

    /// <summary>
    /// Reads horizontal input and moves the basket, clamping its
    /// X position so it never goes off-screen.
    /// </summary>
    private void HandleMovement()
    {
        // Get axis input: -1 (left), 0 (idle), +1 (right)
        float input = Input.GetAxis("Horizontal");

        // Translate the basket along the X axis
        transform.Translate(Vector2.right * input * speed * Time.deltaTime);

        // Clamp so the basket stays within the play area
        float clampedX = Mathf.Clamp(
            transform.position.x,
            -horizontalLimit,
            horizontalLimit
        );

        transform.position = new Vector2(clampedX, transform.position.y);
    }
}
