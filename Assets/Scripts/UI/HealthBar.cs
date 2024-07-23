using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameManager gameManager;
    public List<HealthUnit> healthUnits = new List<HealthUnit>();

    public void UpdateHealthBar()
    {

        for (int i = 0; i < healthUnits.Count; i++)
        {
            healthUnits[i].SetHealthImage(i < gameManager.playerInfo.Health);
        }
    }
}
