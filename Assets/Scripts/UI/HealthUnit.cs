using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUnit : MonoBehaviour
{
    public Sprite fullHealth, emptyHealth;
    Image healthImage;
    bool isFull = true;

    private void Awake()
    {
        healthImage = GetComponent<Image>();
    }

    public void SetHealthImage(bool isFull)
    {
        this.isFull = isFull;
        healthImage.sprite = this.isFull ? fullHealth : emptyHealth;
    }
}
