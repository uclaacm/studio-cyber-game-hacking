using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public float Speed => _speed;

    [SerializeField] float _speed = 2;
    [SerializeField] float smoothTime = 0.2f;

    [SerializeField] Transform lowerLeftBounds;
    [SerializeField] Transform upperRightBounds;

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
        transform.Translate((currentVelocity) * _speed * Time.deltaTime);


        // Ensure unit stays within bounds
        // NOTE: This is a very hasty implementation for implementing walls in the game. In a real game we'd
        // probably like to keep this in a separate component away from the movement provider. Perhaps in the
        // main player control module or something like that.
        if (lowerLeftBounds == null || upperRightBounds == null)
            return;

        Vector3 finalPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if(transform.position.x > upperRightBounds.position.x)
            finalPos.x = upperRightBounds.position.x;
        if(transform.position.x < lowerLeftBounds.position.x)
            finalPos.x = lowerLeftBounds.position.x;
        if(transform.position.y > upperRightBounds.position.y)
            finalPos.y = upperRightBounds.position.y;
        if(transform.position.y < lowerLeftBounds.position.y)
            finalPos.y = lowerLeftBounds.position.y;

        transform.position = finalPos;
    }
}
