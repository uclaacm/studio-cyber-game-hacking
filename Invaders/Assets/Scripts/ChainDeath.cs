using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainDeath : MonoBehaviour
{
    [SerializeField] HealthStat triggerHealthStat;
    [SerializeField] HealthStat[] deathChain;

    void Awake()
    {
        if(triggerHealthStat != null)
            triggerHealthStat.OnUnitKilled += HandleMainDeath;
    }

    public void HandleMainDeath()
    {
        StartCoroutine(DelayedChainDeath());
    }

    IEnumerator DelayedChainDeath()
    {
        foreach(HealthStat health in deathChain)
        {
            yield return new WaitForSeconds(0.5f);
            health.TakeDamage(99999);       // Instakill every item in chain
        }
    }
}
