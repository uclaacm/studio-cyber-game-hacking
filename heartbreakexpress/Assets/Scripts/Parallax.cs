using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script adds the aesthetic effect of parallax, where the foreground moves faster than the background while in a moving vehicle. 
 */

public class Parallax : MonoBehaviour
{
    private Material _material;
    private Vector2 offset;
    [SerializeField] public float xVelocity;
    public float yVelocity;
    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    void Start()
    {
        offset = new Vector2(xVelocity, yVelocity);
    }
    
    void Update()
    {
        _material.mainTextureOffset += offset * Time.deltaTime;

    }
}
