using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRefiller : MonoBehaviour
{
    [SerializeField] private AmmoStat ammo;

    [SerializeField] private float ammoRefillPerSecond = 5.0f;
    [SerializeField] private float cooldownInSeconds = 3.0f;

    public float AmmoRefillPerSecond => ammoRefillPerSecond;
    public float CooldownInSeconds => cooldownInSeconds;

    // Helper function
    private float TimeToRefillOneAmmo => 1 / ammoRefillPerSecond;

    private float timeOfNextRefill = 0;
    private float timeOfEnable = 0;

    private void Update()
    {
        // Refiller has not been enabled yet
        if(timeOfEnable >= Time.time)
        {
            return;
        }

        if(timeOfNextRefill <= Time.time)
        {
            ammo.AddBullets(1);
            timeOfNextRefill = Time.time + TimeToRefillOneAmmo;
        }
    }

    public void TemporarilyDisable()
    {
        timeOfEnable = Time.time + cooldownInSeconds;
    }
}
