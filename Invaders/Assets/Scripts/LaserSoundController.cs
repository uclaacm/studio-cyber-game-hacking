using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSoundController : MonoBehaviour
{
    AudioSource laserSound;
    [SerializeField] ProjectileLauncher playerLauncher;

    bool isPlayingSound = false;

    void Awake()
    {
        laserSound = GetComponent<AudioSource>();

        if(playerLauncher != null)
        {
            laserSound.pitch = playerLauncher.GetShotsPerSecond()/15;       // At pitch = 1, we hear 15 shots per second
        }
    }

    void Update()
    {
        if(playerLauncher.isShooting && !isPlayingSound)
        {
            laserSound.Play();
            isPlayingSound = true;
        }
        else if(!playerLauncher.isShooting && isPlayingSound)
        {
            laserSound.Stop();
            isPlayingSound = false;
        }

        if(Input.GetKeyDown(KeyCode.Space))
            AudioManager.instance.PlaySound("enemy_death_medium");
    }
}
