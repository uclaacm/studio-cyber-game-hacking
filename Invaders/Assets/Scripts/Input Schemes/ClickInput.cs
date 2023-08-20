using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInput : MonoBehaviour, IUnitInput
{
    [SerializeField] float targetReachedThreshold = 0.1f;
    [SerializeField] LocationIndicatorController locationIndicatorController;
    Vector2 targetPoint = Vector2.zero;     // This is the coordinate that we wish to move to
    public Vector2 inputVector {get; private set;}


    Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    Ray lastClick;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            lastClick = cam.ScreenPointToRay((Vector3) Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(lastClick, out hit))
            {
                targetPoint = (Vector2) hit.point;
                if(locationIndicatorController != null)
                    locationIndicatorController.SetNewLocation(targetPoint);
            }
        }



        Vector2 directionVector = targetPoint - (Vector2) transform.position;   // The vector from the current position of the unit to the target position
        if(Vector2.SqrMagnitude(directionVector) > Mathf.Pow(targetReachedThreshold, 2))       
            inputVector = directionVector;
        else
            inputVector = Vector2.zero;             // If the current position of the unit is close enough to the target position, then don't give further input
            
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(lastClick);
    }
}
