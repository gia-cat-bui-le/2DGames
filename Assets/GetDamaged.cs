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
            Debug.Log("Current Health: " + Health);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Getting hit. Current Health: " + Health);
        Hit(20);
        //Destroy(collision.gameObject); // Destroy the projectile on collision
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        //Destroy(enemy); // Destroy the enemy game object
    }
}
