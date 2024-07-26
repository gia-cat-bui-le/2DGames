using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1;

    private void Start()
    {
        // Destroy the projectile after a certain time to prevent it from existing indefinitely
        Destroy(gameObject, 5f); // Adjust the time as needed
    }

    private void FixedUpdate()
    {
        // Move the projectile downwards
        transform.Translate(Vector2.down * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Handle collision with ground or other objects
        //    Apply damage if needed, e.g., to the player or enemy
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        //Destroy the projectile on collision
  
    }
}

