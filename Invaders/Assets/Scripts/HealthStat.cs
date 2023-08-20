using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStat : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth = 100;

    public event System.Action OnUnitKilled;

    public event System.Action<HealthInfo> OnUnitDamaged;   // Broadcast the current health state of the unit

    void Awake()
    {

        if(currentHealth > maxHealth)       // If current health should not be greater than max health
            currentHealth = maxHealth;
        if(currentHealth <= 0)
            currentHealth = 1;              // Unit cannot start off with less than 1 hp
    }

    public void TakeDamage(float dmg)
    {
        if(currentHealth <= 0) {return;}    // We don't want to constantly be invoking the damaged/killed events even after the unit has died

        currentHealth -= dmg;

        OnUnitDamaged?.Invoke(new HealthInfo(maxHealth, currentHealth));

        if(currentHealth <= 0)
        {
            OnUnitKilled?.Invoke();     // If the unit has died, then give broadcast

            gameObject.SetActive(false);
        }
            
    }
}

public struct HealthInfo
{
    public float maxHealth;
    public float currentHealth;

    public HealthInfo(float mHealth, float cHealth)     // Have a constructor to make it easier to initialize a new HealthInfo struct
    {
        maxHealth = mHealth;
        currentHealth = cHealth;
    }
}
