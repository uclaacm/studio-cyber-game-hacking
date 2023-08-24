using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour {

    public Vector3 mousePosition;
    public float angle;
    public Vector2 direction;

    public Ray ray;
    public RaycastHit2D hit;

    bool cooldown = false;
    [SerializeField] float cooldownTime = 2f;
    float canShoot = -1f;
    bool isScaling = false;

    [SerializeField] Transform rotationPoint;

    [SerializeField] GameObject Envelope;   // prefab of envelope to be shot
    GameObject shotEnvelope;    // the envelope that was shot
    Rigidbody2D envelopeRb;
    [SerializeField] Vector3 envSpawnOffset;
    [SerializeField] float fireSpeed = 15;
    Quaternion envRotation = Quaternion.identity;

    public Vector2 target;
    public Vector2 fireDirection;

    static Object[] envAmmo;
    int randomEnv;

    // Start is called before the first frame update
    void Start() {
        if (envAmmo == null) {
            envAmmo = Resources.LoadAll("EnvelopeAmmoSprites", typeof(Sprite));
        }
    }

    // Update is called once per frame
    void Update() {
        
        // Rotate the gun around a point to face the mouse
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = mousePosition - transform.position;
        angle = Vector2.SignedAngle(transform.up, direction);
        transform.RotateAround(rotationPoint.position, new Vector3(0, 0, 1), angle);

    }
    
    // All the gun shooting functionality 
    void Shoot() {

        // cooldown implementation
        if (Time.time < canShoot) {
            return;
        }
        
        // to get game object of thing hit if smth is hit
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        
        // I'm not sure this section is needed but it might make the animations look better, I'm not sure I haven't tested it on a moving truck yet
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Person")) {
            // Debug.Log(hit.collider.gameObject.name);
            target = hit.collider.gameObject.transform.position;
            
            // personHit offset if needed to make animations look better?
        } else {
            // Debug.Log("hit something else");
            target = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        // record time of cooldown ending
        canShoot = Time.time + cooldownTime;

        randomEnv = UnityEngine.Random.Range(0, envAmmo.Length);
        
        // target acquired, shoot envelope
        fireDirection = target - new Vector2(transform.position.x, transform.position.y);
        fireDirection.Normalize();
        envRotation = UnityEngine.Random.rotation;
        envRotation.x = Quaternion.identity.x;
        envRotation.y = Quaternion.identity.y;
        shotEnvelope = Instantiate(Envelope, rotationPoint.position, envRotation);
        shotEnvelope.GetComponent<SpriteRenderer>().sprite = Instantiate(envAmmo[randomEnv]) as Sprite;
        envelopeRb = shotEnvelope.GetComponent<Rigidbody2D>();
        envelopeRb.velocity = fireDirection * fireSpeed;
        isScaling = false;
        StartCoroutine(scaleOverTime(shotEnvelope, new Vector3(0.003f, 0.003f, 0f), 2f));
    }

    IEnumerator scaleOverTime(GameObject flyingEnv, Vector3 toScale, float duration) {
        // Debug.Log("Scaling Coroutine");

        if (isScaling) {
            yield break;
        }
        
        float counter = 0;

        Vector3 startScale = flyingEnv.transform.localScale;

        while (counter < duration) {
            if (!flyingEnv) yield break;
            counter += Time.deltaTime;
            flyingEnv.transform.localScale = Vector3.Lerp(startScale, toScale, counter / duration);
            // terrible implementation of deleting too small game objects
            if (flyingEnv.transform.localScale.magnitude < new Vector3(0.0043f, 0.0043f, 0f).magnitude) {
                Destroy(flyingEnv);
                counter = duration;
            }
            yield return null;
        }

        isScaling = false;

    }

    void OnShoot(InputValue value) {
        Shoot();
    }
}
