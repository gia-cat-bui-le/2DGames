using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    Animator animator;
    public GameObject player;
    Shield shield;

    [SerializeField]
    private float _maxHealth;

    public float maxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private float _health = 5;

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public bool IsAlive
    {
        get { 
            return _isAlive; 
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
        }
    }

    private void Update()
    {
        //if (!shield.activeShield)
        //{
        //    Debug.Log("Not Shielding");
        //}
        //else
        //{
        //    Debug.Log("Shielding");
        //}
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
       }

        if (IsStunned)
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= stunDuration)
            {
                IsStunned = false;
                spriteRenderer.color = originalColor;
                if (playerController != null)
                {
                    playerController.enabled = true; // Re-enable player movement
                }
            }
        }

        //Hit(10);

        if (!IsAlive)
        {
            player.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        playerController = GetComponent<PlayerController>();
        shield = GameObject.FindGameObjectWithTag("Player").GetComponent<Shield>();
    }

    public void Hit(int damage)
    {
        if (IsAlive && !isInvincible && !shield.activeShield) 
        {
            Health -= damage;
            isInvincible = true;
            Stun();
        }
    }

    public float stunDuration = 0.5f;
    private bool _isStunned = false;
    private float stunTimer = 0f;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    public bool IsStunned
    {
        get
        {
            return _isStunned;
        }
        set
        {
            _isStunned = value;
            animator.SetBool(AnimationStrings.isStunned, value);
        }
    }

    private void Stun()
    {
        IsStunned = true;
        stunTimer = 0f;
        spriteRenderer.color = Color.red;
        if (playerController != null)
        {
            playerController.enabled = false; // Disable player movement
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            Hit(1);
            Debug.Log("Player - Getting hit. Current Health: " + Health);
            //Destroy(collision.gameObject); // Destroy the projectile on collision
        }
        else
        {
            Debug.Log("Player - Getting hit error: " + collision.gameObject.tag);
        }
    }
}
