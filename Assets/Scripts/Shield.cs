using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Transform shieldPoint;
    public GameObject shieldPrefab;
    public AbilityIcon shieldIcon;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && shieldIcon.IsReady())
        {
            Shielding();
        }
    }

    void Shielding()
    {
        // shooting logic
        GameObject shieldEffect = Instantiate(shieldPrefab, shieldPoint.position, shieldPoint.rotation);
        Destroy(shieldEffect, 1.0f);
        shieldIcon.StartCooldown();
    }
}
