using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool instance {get; private set;}


    [SerializeField] GameObject projectilePrefab;
    Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        // NOTE: This will not work if we have multiple scenes because this will get destroyed
        instance = this;
    }


    public GameObject Get()
    {
        // The structure below ensures that the number of objects in our pool meets the demand of the game exactly

        // The reason we queue (through AddObject) and then dequeue (which seems sorta redundant) is because it fits in nicely
        // with the case where we don't need to enqueue first (when there are still objects in the pool) -- we will dequeue
        // regardless of whether there are pooled objects left

        if(pool.Count == 0)         // If at the current moment there's nothing left in the queue, then we must add one to the queue for us to take
            AddObject();

        return pool.Dequeue();      
    }

    public void ReturnToPool(GameObject proj)
    {
        proj.SetActive(false);
        pool.Enqueue(proj);
    }

    void AddObject()
    {
        var newObject = GameObject.Instantiate(projectilePrefab,Vector3.zero,Quaternion.identity,this.transform);
        newObject.SetActive(false);
        pool.Enqueue(newObject);

        newObject.GetComponent<IPooledGameObject>().pool = this;        // Initialize the pool instance that the projectile will use
    }

}
