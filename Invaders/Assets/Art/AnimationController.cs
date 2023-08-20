using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    HealthStat healthStat;
    Animator animator;

    void Awake()
    {
        
        animator = GetComponent<Animator>();

        healthStat = GetComponent<HealthStat>();

        if(healthStat != null)
            healthStat.OnUnitDamaged += HandleDamaged;
    }

    void HandleDamaged(HealthInfo info)
    {
        animator.SetTrigger("wasHit");
    }
}
