using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
