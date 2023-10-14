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

    bool cooldown = false;
    [SerializeField] float cooldownTime = 2f;
    public float CooldownTime => cooldownTime;

    float canShoot = -1f;
    bool isScaling = false;

    [SerializeField] Transform rotationPoint;

    [SerializeField] GameObject Envelope;   // prefab of envelope to be shot
    GameObject shotEnvelope;    // the envelope that was shot
    Rigidbody2D envelopeRb;
    [SerializeField] Vector3 envSpawnOffset;
    [SerializeField] float fireSpeed = 15;
    public float FireSpeed => fireSpeed;
    Quaternion envRotation = Quaternion.identity;

    public Vector2 target;
    public Vector2 fireDirection;

    static Object[] envAmmo;
    int randomEnv;

    // Start is called before the first frame update
    // Fill up the envAmmo array with all the possible envelope sprites
    void Start() {
        if (envAmmo == null) {
            envAmmo = Resources.LoadAll("EnvelopeAmmoSprites", typeof(Sprite));
        }
    }

    // Update is called once per frame
    void Update() {
        
        // Rotates the gun around a point to face the mouse
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

        // record time of cooldown ending
        canShoot = Time.time + cooldownTime;

        GetComponent<AudioSource>().Play();

        // get a random envelope sprite, set the target of the envelope to mouse cursor location, normalize vector towards firing direction
        randomEnv = UnityEngine.Random.Range(0, envAmmo.Length);
        target = mousePosition;
        fireDirection = direction.normalized;

        // Instantiate an envelope to be shot with a random rotation and random sprite
        envRotation = new Quaternion(Quaternion.identity.x, Quaternion.identity.y, Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        shotEnvelope = Instantiate(Envelope, rotationPoint.position, envRotation);
        shotEnvelope.GetComponent<SpriteRenderer>().sprite = Instantiate(envAmmo[randomEnv]) as Sprite;

        // Shoot the envelope, and start the scaleOverTime coroutine
        // HACKABLE: Envelope shrinks the longer it's on screen. Can you make it larger or prevent it from shrinking for a wider range?
        envelopeRb = shotEnvelope.GetComponent<Rigidbody2D>();
        envelopeRb.velocity = fireDirection * fireSpeed;
        isScaling = false;
        StartCoroutine(scaleOverTime(shotEnvelope, new Vector3(0.003f, 0.003f, 0f), 2f));
    }

    // This coroutine makes the envelope get smaller the longer it's on the screen. When the envelope is too small, the gameobject will be destroyed.
    // I do not know how it works.
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

    // When left click, shoot
    void OnShoot(InputValue value) {
        Shoot();
    }
}
