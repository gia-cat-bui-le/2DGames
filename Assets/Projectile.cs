using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle damage to ground or other objects
        // Assuming ground or other objects have a tag "Ground"
        Destroy(gameObject);
    }
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
