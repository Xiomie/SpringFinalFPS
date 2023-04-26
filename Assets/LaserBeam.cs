using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Transform firePoint; 
    public float maxBeamDistance = 100f; 
    public LayerMask targetMask; 
    public float maxDamage;
    public float damageRate = 1f;
    private LineRenderer lineRenderer;
    private float nextDamageTime; 
    private bool isFiring = false;
    public AudioSource shootSound;
    public AudioClip laserSound;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        shootSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (Input.GetButton("Fire1"))
            {
                if (!isFiring)
                {
                    isFiring = true;
                    shootSound.clip = laserSound;
                    shootSound.loop = true;
                    shootSound.Play();
                }
                ShootLaser();
            }
            else
            {
                if (isFiring)
                {
                    isFiring = false;
                    shootSound.Stop();
                }
                lineRenderer.enabled = false;
            }
        }
    }

    void ShootLaser()
    {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(firePoint.position, firePoint.forward, out hit, maxBeamDistance, targetMask);
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePoint.position);

        if (hasHit)
        {
            lineRenderer.SetPosition(1, hit.point);
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                float distance = Vector3.Distance(firePoint.position, hit.point);
                float damage = Mathf.Lerp(0f, maxDamage, 1f - distance / maxBeamDistance);

                if (Time.time >= nextDamageTime)
                {
                    enemyHealth.TakeDamage(damage);
                    nextDamageTime = Time.time + 1f / damageRate;
                }
            }
        }
        else
        {
            lineRenderer.SetPosition(1, firePoint.position + firePoint.forward * maxBeamDistance);
        }
    }
}