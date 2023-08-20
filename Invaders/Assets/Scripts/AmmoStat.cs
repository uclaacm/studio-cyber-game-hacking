using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoStat : MonoBehaviour
{
    [SerializeField] int maxCapacity = 100;
    [SerializeField] int bulletsLeft = 0;

    public event System.Action<AmmoInfo> OnChangeBullets;
    
    void Awake()
    {
        if(bulletsLeft > maxCapacity)       // Prevents accidentally putting more bullets than capacity (through editor)
            bulletsLeft = maxCapacity;
    }

    void Start()
    {
        // We put this in start because other scripts subscribe to the event in Awake()
        OnChangeBullets?.Invoke(new AmmoInfo(maxCapacity, bulletsLeft));
    }
    // Try to expend a certain number of bullets from storage
    public bool TryUseBullets(int numBullets)
    {
        if(numBullets > bulletsLeft)
            return false;
        else
        {
            bulletsLeft -= numBullets;
            OnChangeBullets?.Invoke(new AmmoInfo(maxCapacity, bulletsLeft));
            return true;
        }
    }

    public void AddBullets(int numBullets)
    {
        bulletsLeft += numBullets;
        if(bulletsLeft > maxCapacity)
            bulletsLeft = maxCapacity;
        OnChangeBullets?.Invoke(new AmmoInfo(maxCapacity, bulletsLeft));
    }
}

// NOTE: There's an equivalent struct for health info (probably could be grouped together under same class)
public struct AmmoInfo
{
    public int maxBullets;
    public int currentBullets;
    public AmmoInfo(int mBullets, int cBullets)
    {
        maxBullets = mBullets;
        currentBullets = cBullets;
    }
}
