using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] float smoothTime = 0.2f;

    Vector2 currentVelocity = Vector2.zero;
    Vector2 targetVelocity = Vector2.zero;
    Vector2 currentVel;


    IUnitInput input;

    void Awake()
    {
        input = GetComponent<IUnitInput>();
        if(input == null)
            Debug.LogError("This unit needs an input component!");
    }

    // Update is called once per frame
    void Update()
    {
        if(input == null) {return;}

        targetVelocity = input.inputVector.normalized;
        currentVelocity = Vector2.SmoothDamp(currentVelocity, targetVelocity, ref currentVel, smoothTime);
        transform.Translate((currentVelocity) * speed * Time.deltaTime);
    }
}
