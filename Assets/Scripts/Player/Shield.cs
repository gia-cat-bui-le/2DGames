using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Transform shieldPoint;
    public GameObject shieldPrefab;
    public AbilityIcon shieldIcon;
    public float shieldDuration = 5f;

    public GameObject activeShield;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {    
        if (Input.GetButtonDown("Fire2") && shieldIcon.IsReady())
        {
            audioManager.PlaySFX(audioManager.shield);
            Shielding();
        }
    }

    void Shielding()
    {
        if (activeShield == null)
        {
            activeShield = Instantiate(shieldPrefab, shieldPoint.position, shieldPoint.rotation, shieldPoint);
            StartCoroutine(ShieldDuration());
            shieldIcon.StartCooldown();
        }
    }

    private IEnumerator ShieldDuration()
    {
        yield return new WaitForSeconds(shieldDuration);
        if (activeShield != null)
        {
            Destroy(activeShield);
            activeShield = null;
        }
    }
   
}
