using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BondController : MonoBehaviour
{
    LineRenderer lineRenderer;
    EdgeCollider2D edgeCollider;
    HealthStat healthStat;
    [SerializeField] Transform player;
    [SerializeField] Transform altPlayer;
    [SerializeField] float thickness = 0.1f;

    Vector2 firstPos = Vector2.zero;
    Vector2 secondPos = Vector2.zero;

    void Awake()
    {
        //TODO: Initialize line renderer and edge collider
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        if(lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.endWidth = thickness;
            lineRenderer.startWidth = thickness;
        }

        if(edgeCollider != null)
        {
            edgeCollider.edgeRadius = thickness/2;
        }


        // Set up the edge collider to be disabled when it dies
        healthStat = GetComponent<HealthStat>();
        if(healthStat != null)
            healthStat.OnUnitKilled += HandleDeath;
            

    }

    // Update is called once per frame
    void Update()
    {

        firstPos = new Vector2(player.position.x, player.position.y);
        secondPos = new Vector2(altPlayer.position.x, altPlayer.position.y);

        if(lineRenderer != null)
        {
            lineRenderer.SetPosition(0, firstPos);
            lineRenderer.SetPosition(1, secondPos);
        }

        if(edgeCollider != null)
        {
            Vector2[] newPoints = {firstPos, secondPos};
            edgeCollider.points = newPoints;
        }
    }


    void HandleDeath()
    {
        if(edgeCollider != null)
            edgeCollider.enabled = false;

        // Do some other stuff (like play proper animation or change color, etc.)
        healthStat.OnUnitKilled -= HandleDeath;
    }
}
