using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityIcon : MonoBehaviour
{
    public Image iconImage;
    public Image cooldownOverlay;
    public TextMeshProUGUI cooldownText;
    public float cooldownTime = 5f;
    private float cooldownTimer = 0f;

    private void Start()
    {
        ResetCooldown();
    }

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            UpdateCooldownDisplay();
        }
        else if (cooldownTimer <= 0 && cooldownOverlay.gameObject.activeSelf)
        {
            ResetCooldown();
        }
    }

    public bool IsReady()
    {
        return cooldownTimer <= 0;
    }

    public void StartCooldown()
    {
        cooldownTimer = cooldownTime;
        cooldownOverlay.gameObject.SetActive(true);
        UpdateCooldownDisplay();
    }

    private void UpdateCooldownDisplay()
    {
        cooldownOverlay.fillAmount = cooldownTimer / cooldownTime;
        cooldownText.text = Mathf.Ceil(cooldownTimer).ToString();
    }

    private void ResetCooldown()
    {
        cooldownTimer = 0;
        cooldownOverlay.gameObject.SetActive(false);
        cooldownText.text = "";
    }
}