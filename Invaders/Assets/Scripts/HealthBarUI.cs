using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] HealthStat healthStat;
    Slider slider;

    void Awake()
    {
        if(healthStat == null)
            Debug.LogWarning("No health stat component linked to this health bar!");
        else
        {
            healthStat.OnUnitDamaged += HandleNewHealth;
        }
        
        slider = GetComponent<Slider>();
        if(slider == null)
            Debug.LogWarning("Health bar component needs a slider!");

    }

    void OnDestroy()
    {
        healthStat.OnUnitDamaged -= HandleNewHealth;
    }

    void HandleNewHealth(HealthInfo info)
    {
        slider.value = info.currentHealth/info.maxHealth;
    }
}
