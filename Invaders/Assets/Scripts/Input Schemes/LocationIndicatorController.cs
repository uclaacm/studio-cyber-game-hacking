using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationIndicatorController : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetNewLocation(Vector3 pos)
    {
        transform.position = pos;
        animator.SetTrigger("newLocation");
    }
}
