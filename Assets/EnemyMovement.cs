using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public float flySpeed = 3f;
    public float directionChangeInterval = 2f; // Interval in seconds for changing direction
    private Rigidbody2D rb;
    private Vector2 flyDirection; // Current flying direction
    private float screenWidth;
    private float screenHeight;
    private float nextDirectionChangeTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        screenWidth = Camera.main.aspect * Camera.main.orthographicSize;
        screenHeight = Camera.main.orthographicSize;
        nextDirectionChangeTime = Time.time + directionChangeInterval;
        flyDirection = GetRandomDirection();
    }

    private void FixedUpdate()
    {
        MoveEnemy();
        CheckScreenBounds();

        if (Time.time >= nextDirectionChangeTime)
        {
            ChangeFlyDirection();
            nextDirectionChangeTime = Time.time + directionChangeInterval;
        }
    }

    private void MoveEnemy()
    {
        // Calculate the velocity based on the fly direction and speed
        Vector2 velocity = flyDirection * flySpeed;
        rb.velocity = velocity;
    }

    // Method to change the flying direction randomly within the upper half of the screen
    private void ChangeFlyDirection()
    {
        flyDirection = GetRandomDirection();
    }

    private Vector2 GetRandomDirection()
    {
        Vector2 randomDirection;
        do
        {
            randomDirection = Random.insideUnitCircle.normalized;
        } while (randomDirection.y < 0); // Ensure it moves in the upper half of the screen

        // Flip the enemy sprite horizontally if needed
        Vector3 scale = transform.localScale;
        if ((randomDirection.x > 0 && scale.x < 0) || (randomDirection.x < 0 && scale.x > 0))
        {
            scale.x *= -1;
            transform.localScale = scale;
        }

        return randomDirection;
    }

    private void CheckScreenBounds()
    {
        // Get the position of the enemy
        Vector2 position = transform.position;

        // Ensure the enemy doesn't go out of the screen bounds
        if (position.x < -screenWidth)
        {
            position.x = -screenWidth;
            flyDirection.x = Mathf.Abs(flyDirection.x); // Move right
        }
        else if (position.x > screenWidth)
        {
            position.x = screenWidth;
            flyDirection.x = -Mathf.Abs(flyDirection.x); // Move left
        }

        float upperHalfHeight = screenHeight; // Restrict to upper half of the screen
        if (position.y < 0)
        {
            position.y = 0;
            flyDirection.y = Mathf.Abs(flyDirection.y); // Move up
        }
        else if (position.y > upperHalfHeight)
        {
            position.y = upperHalfHeight;
            flyDirection.y = -Mathf.Abs(flyDirection.y); // Move down
        }

        transform.position = position;
    }
}

