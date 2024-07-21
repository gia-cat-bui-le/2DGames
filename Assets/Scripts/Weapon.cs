using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public AbilityIcon abilityIcon;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && abilityIcon.IsReady())
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        abilityIcon.StartCooldown();
    }
}
