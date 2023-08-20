using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BondTransformMaintainer : MonoBehaviour
{
    LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (lineRenderer.GetPosition(0) + lineRenderer.GetPosition(1))/2;
    }
}
