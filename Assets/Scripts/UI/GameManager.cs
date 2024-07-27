using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //public GameObject player;   // replace later
    public Damageable playerInfo;
    public GetDamaged enemyInfo;
    public HealthBar healthBar;
    public GameObject WinUI;
    public GameObject LoseUI;
    public GameObject enemyHealthUI;
    public GameObject settingMenu;
    public GameObject volumeMenu;

    private TextMeshProUGUI enemyHealthText;

    public float cooldownTime = 3;  // replace by player
    public float cooldownTimer = 0; // replace by player

    // Start is called before the first frame update
    void Start()
    {
        LoseUI.SetActive(false);
        WinUI.SetActive(false);

        if (enemyHealthUI != null)
        {
            enemyHealthText = enemyHealthUI.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("Enemy Health UI is not assigned!");
        }
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
        if (!playerInfo.IsAlive && enemyInfo.IsAlive)
        {
            LoseUI.SetActive(true);
            WinUI.SetActive(false);
            settingMenu.SetActive(false);
            volumeMenu.SetActive(false);
        }
        else if (playerInfo.IsAlive && !enemyInfo.IsAlive)
        {
            LoseUI.SetActive(false);
            WinUI.SetActive(true);
            settingMenu.SetActive(false);
            volumeMenu.SetActive(false);
        }

        // Update the enemy health text
        if (enemyHealthText != null && enemyInfo != null)
        {
            enemyHealthText.text = "Enemy Health: " + enemyInfo.Health.ToString();
        }
    }

    // remove later
    public void UseSpell()
    {
        cooldownTimer = cooldownTime;
    }
}
