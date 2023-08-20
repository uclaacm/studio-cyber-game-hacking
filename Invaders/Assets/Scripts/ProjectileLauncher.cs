using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] float shotsPerSecond = 1;

    [SerializeField] PoolType poolType = PoolType.basic;
    ProjectilePool pool;


    [SerializeField] bool burst = false;
    [SerializeField] float burstOnTime = 0.5f;
    [SerializeField] float burstOffTime = 0.5f;

    public enum PoolType
    {
        basic,
        fast,
        cool,
        chonk
    }

    [SerializeField] AmmoStat optionalAmmoSource;       // This is optional so that we can have a unit have infinite ammo

    private bool _isShooting = false;
    public bool isShooting => _isShooting;


    bool isLocked = false;

    float lastShot = 0;

    void Awake()
    {
        if(pool == null)
        {
            switch(poolType)
            {
                case PoolType.basic:
                    pool = GameObject.Find("Basic Bullet Pool").GetComponent<ProjectilePool>();
                    break;
                case PoolType.fast:
                    pool = GameObject.Find("Fast Bullet Pool").GetComponent<ProjectilePool>();
                    break;
                case PoolType.cool:
                    pool = GameObject.Find("Cool Bullet Pool").GetComponent<ProjectilePool>();
                    break;
                case PoolType.chonk:
                    pool = GameObject.Find("Chonk Bullet Pool").GetComponent<ProjectilePool>();
                    break;
            }
        }
        if(firePoint == null)
            Debug.LogWarning("No fire point specified");

        if(burst)
            StartCoroutine(BurstToggle());
    }

    // Update is called once per frame
    void Update()
    {
        if(firePoint == null || pool == null) {return;}

        if(isLocked)
        {
            _isShooting = false;
            return;
        }

        if(Time.time - lastShot > 1/shotsPerSecond)
        {
            lastShot = Time.time;       // We want to update the last shot time regardless of whether any bullets are actually shot to prevent constant checking of this loop in the case nothing is shot

            if(optionalAmmoSource != null)                  // If we do have an ammo source, then expend bullets
            {
                if(!optionalAmmoSource.TryUseBullets(1))    // If we are all out of bullets to use, then don't shoot any more
                {
                    _isShooting = false;
                    return;
                }
            }
            _isShooting = true;

            var newProj = pool.Get();       // This is where we try to get another projectile from the queue (note that the projectiles will automatically enqueue themselves back in after expiring)

            

            newProj.transform.position = firePoint.position;
            newProj.transform.rotation = firePoint.rotation;

            
            // This is a messy way of setting the "no damage tag" for the projectile (messy bc this is supposed to be generic code)
            Projectile projectile = newProj.GetComponent<Projectile>();
            if(projectile != null)
                projectile.noDamageTag = gameObject.tag;
            


            // It's important that we enable the bullet AFTER setting the position and rotation so that the force is applied in
            // the proper direction on enable

            newProj.SetActive(true);        // We NEED this line because the projectiles are disabled when we grab them from the queue
    
            


            
        }
    }


    public float GetShotsPerSecond() => shotsPerSecond;     // This is used by the laser sound effect manager to determine how fast to play the loop

    void OnDisable()
    {
        _isShooting = false;    // Don't want player to "keep shooting" after death
    }


    public void SetLockState(bool lockState)
    {
        isLocked = lockState;
    }


    IEnumerator BurstToggle()
    {
        while(true)
        {
            SetLockState(true);
            yield return new WaitForSeconds(burstOffTime);
            SetLockState(false);
            yield return new WaitForSeconds(burstOnTime);
        }
    }

}
