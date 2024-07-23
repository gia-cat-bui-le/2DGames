using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject player;   // replace later
    public Damageable playerInfo;
    public HealthBar healthBar;

    public float cooldownTime = 3;  // replace by player
    public float cooldownTimer = 0; // replace by player
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.UpdateHealthBar();
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        if (Input.GetMouseButton(0) && cooldownTimer <= 0)
        {
            UseSpell();
        }
    }
    
    // remove later
    public void UseSpell()
    {
        cooldownTimer = cooldownTime;
    }
}
