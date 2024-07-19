using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    Animator animator;
    public GameObject player;

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
    private float _health = 100;

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
            Debug.Log("is alive set " + value);
        }
    }

    private void Update()
    {
       if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
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
    }

    public void Hit(int damage)
    {
        if (IsAlive && !isInvincible) 
        {
            Health -= damage;
            isInvincible = true;
        }
    }
}
