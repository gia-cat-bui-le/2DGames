using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownUI : MonoBehaviour
{
    [SerializeField]
    private Image imageCooldown;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        imageCooldown.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ApplyCooldown();
    }

    void ApplyCooldown()
    {
        if (gameManager.cooldownTimer < 0)
        {
            imageCooldown.fillAmount = 0;
        }
        else
        {
            imageCooldown.fillAmount = gameManager.cooldownTimer / gameManager.cooldownTime;
        }
    }
}
