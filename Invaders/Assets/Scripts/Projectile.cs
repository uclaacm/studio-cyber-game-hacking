using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPooledGameObject
{
    [SerializeField] float force = 1;
    [SerializeField] float expireTime = 5;
    [SerializeField] int bulletValue = 1;     // How many player bullets this projectile corresponds to
    [SerializeField] int damage = 5;
    float timeOfEnable = 0;



    // We make this setter so that the launchers can set themselves as the ones that their projectiles should not damage 
    private string _noDamageTag = "";      // We don't want the projectiles to deal damage to anything before we initialize it
    public string noDamageTag
    {
        set
        {
            _noDamageTag = value;
        }
    }  




    // NOTE:  We use the below structure in order to catch mistakenly assigning a pool more than once to a given projectile
    private ProjectilePool _pool;       // Note that we need this private version of the pool instance in order to make the check whether a pool has been assigned to it
    public ProjectilePool pool
    {
        get{ return _pool;}
        set
        {
            if(_pool == null)
                _pool = value;
            else
                throw new System.Exception("Attempted to assign multiple pools to the same projectile!");   // A pool should only be assigned once after the instantiation of the projectile by the pooling system
        }
    }


    // Update is called once per frame

    void OnEnable()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * force);
        timeOfEnable = Time.time;

    }


    void Update()
    {
        if(Time.time - timeOfEnable > expireTime)
        {
            Vanish();
        }
    }


    void Vanish()
    {
        pool.ReturnToPool(gameObject);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Is there a better way for checking what type of thing we collided with? I thought Smallberg said something about
        // how attempting to a cast an object to a certain type and then checking to see if it returned a null value was costly
        BulletEater bulletEater = other.GetComponent<BulletEater>();
        HealthStat healthStat = other.GetComponent<HealthStat>();
        if(bulletEater != null)
        {
            bulletEater.FeedBullets(bulletValue);
            Vanish();
        }
        else if(healthStat != null)
        {
            if(other.CompareTag(_noDamageTag)) {return;}        // Don't do anything if the object is tagged with an ignore damage tag

            healthStat.TakeDamage(damage);
            Vanish();

        }

    }


}

// This interface allows us to give any game object the "credentials" it needs to be accepted/used as a pooled object
public interface IPooledGameObject
{
    ProjectilePool pool {get; set;}
}
