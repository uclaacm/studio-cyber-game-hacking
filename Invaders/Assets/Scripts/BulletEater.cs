using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEater : MonoBehaviour
{
    [SerializeField] AmmoStat ammoSource;

    void Awake()
    {
        if(ammoSource == null)
        {
            Debug.LogWarning("There is no AmmoStat component associated with this Eater!");
        }
    }
    

    // The reason we don't have the bullets contacting the ammo source directly is because the bullets
    // don't really know where the ammo source is in relation to the eater it hits
    public void FeedBullets(int numBullets)
    {
        if(ammoSource != null)
            ammoSource.AddBullets(numBullets);
    }
}
