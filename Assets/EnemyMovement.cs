using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public float flySpeed = 3f;
    public float directionChangeInterval = 2f; // Interval in seconds for changing direction
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float shootCooldown = 1f; // Cooldown in seconds between each shot
    public float verticalOffset = 2f; // Vertical offset above the player's head
    public float minHorizontalDistance = 1f; // Minimum horizontal distance from the player
    public float verticalMoveAmplitude = 2f; // Amplitude for vertical movement
    public float verticalMoveInterval = 1f; // Interval in seconds for vertical movement change

    private Rigidbody2D rb;
    private float screenWidth;
    private float screenHeight;
    private float nextDirectionChangeTime;
    private float nextShootTime;
    private float nextVerticalMoveTime;
    private Transform player; // Reference to the player's transform
    private Vector2 targetPosition;
    private bool movingUp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        screenWidth = Camera.main.aspect * Camera.main.orthographicSize;
        screenHeight = Camera.main.orthographicSize;
        nextDirectionChangeTime = Time.time + directionChangeInterval;
        nextShootTime = Time.time; // Start shooting immediately
        nextVerticalMoveTime = Time.time + verticalMoveInterval;
        movingUp = true; // Start moving up
    }

    private void Start()
    {
        // Find the player object by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found!");
        }
    }

    private void FixedUpdate()
    {
        MoveEnemy();
        CheckScreenBounds();

        if (Time.time >= nextDirectionChangeTime)
        {
            nextDirectionChangeTime = Time.time + directionChangeInterval;
        }

        if (Time.time >= nextShootTime)
        {
            ShootProjectile();
            nextShootTime = Time.time + shootCooldown;
        }

        if (Time.time >= nextVerticalMoveTime)
        {
            nextVerticalMoveTime = Time.time + verticalMoveInterval;
            movingUp = !movingUp; // Toggle vertical movement direction
        }
    }

    private void MoveEnemy()
    {
        // If the player is not assigned, move randomly
        if (player == null)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // Calculate the target position above the player's head
        targetPosition = new Vector2(player.position.x, player.position.y + verticalOffset);

        // Ensure the enemy maintains a minimum horizontal distance from the player
        if (Mathf.Abs(targetPosition.x - player.position.x) < minHorizontalDistance)
        {
            if (targetPosition.x < player.position.x)
            {
                targetPosition.x = player.position.x - minHorizontalDistance;
            }
            else
            {
                targetPosition.x = player.position.x + minHorizontalDistance;
            }
        }

        // Add vertical fluctuation to the target position
        float fluctuation = movingUp ? verticalMoveAmplitude : -verticalMoveAmplitude;
        targetPosition.y += fluctuation;

        // Move towards the target position
        Vector2 directionToTarget = (targetPosition - rb.position).normalized;
        rb.velocity = directionToTarget * flySpeed;

        // Flip the enemy sprite horizontally if needed
        Vector3 scale = transform.localScale;
        if ((directionToTarget.x > 0 && scale.x < 0) || (directionToTarget.x < 0 && scale.x > 0))
        {
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void CheckScreenBounds()
    {
        // Get the position of the enemy
        Vector2 position = transform.position;

        // Ensure the enemy doesn't go out of the screen bounds
        if (position.x < -screenWidth)
        {
            position.x = -screenWidth;
        }
        else if (position.x > screenWidth)
        {
            position.x = screenWidth;
        }

        float upperHalfHeight = screenHeight; // Restrict to upper half of the screen
        if (position.y < 0)
        {
            position.y = 0;
        }
        else if (position.y > upperHalfHeight)
        {
            position.y = upperHalfHeight;
        }

        transform.position = position;
    }

    private void ShootProjectile()
    {
        // Ensure the projectilePrefab is not null
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab is not assigned!");
            return;
        }

        // Calculate the shoot position and instantiate the projectile
        Vector2 shootPosition = new Vector2(transform.position.x, transform.position.y - 1); // Offset position downwards
        Instantiate(projectilePrefab, shootPosition, Quaternion.identity);

        // Debug.Log("Projectile instantiated at position: " + shootPosition);
    }
}
