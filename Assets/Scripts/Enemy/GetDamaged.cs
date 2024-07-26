using UnityEngine;

public class GetDamaged : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth = 100;
    [SerializeField]
    private float _health;
    [SerializeField]
    private bool _isAlive = true;

    public GameObject enemy;

    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;
                Die();
            }
        }
    }

    public bool IsAlive
    {
        get { return _isAlive; }
        set { _isAlive = value; }
    }

    private void Awake()
    {
        Health = MaxHealth; // Initialize health to max health
    }

    private void Update()
    {
        if (!IsAlive)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void Hit(int damage)
    {
        if (IsAlive)
        {
            Health -= damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerBullet"))
        {
            Hit(20);
            Debug.Log("Enemy - Getting hit. Current Health: " + Health);
            //Destroy(collision.gameObject); // Destroy the projectile on collision
        }
        //else
        //{
        //    Debug.Log("Enemy - Getting hit error: " + collision.gameObject.tag);
        //}
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        // Rotate the enemy to lie down horizontally
        //transform.rotation = Quaternion.Euler(0, 0, 90);

        // Optionally, destroy the enemy after some time to give a chance to see the rotation
        Destroy(enemy, 1f); // Adjust the delay as needed
    }
}
