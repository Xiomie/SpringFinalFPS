using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirewandSound : MonoBehaviour
{
    public AudioClip fireSound;
    public AudioSource audioSource;
    public float fireCooldown = 0.5f;
    private bool canFire = true;

    void Update()
    {
        if (!PauseMenu.isPaused && canFire)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                // Play the fire sound effect using the AudioSource component
                audioSource.PlayOneShot(fireSound);

                // Set canFire to false to start the cooldown
                canFire = false;

                // Call the ResetFire method after the fireCooldown time
                Invoke("ResetFire", fireCooldown);
            }
        }
    }

    void ResetFire()
    {
        canFire = true;
    }
}