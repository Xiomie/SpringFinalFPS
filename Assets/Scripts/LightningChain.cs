using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningChain : MonoBehaviour
{
    public Transform firePoint;
    public float maxChainDistance = 100f;
    public LayerMask targetMask;
    public float maxDamage;
    public float damageRate = 1f;
    private LineRenderer lineRenderer;
    private float nextDamageTime;
    public float maxChainCount;
    public AudioSource shootSound;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        shootSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (Input.GetMouseButton(0))
            {
                ShootLightning();
                if (!shootSound.isPlaying)
                {
                    shootSound.Play();
                }
            }
            else
            {
                if (shootSound.isPlaying)
                {
                    shootSound.Stop();
                }
                lineRenderer.enabled = false;
            }
        }
    }

    Vector3 GetRandomDirection(Vector3 currentDirection)
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = Mathf.Abs(randomDirection.y);
        randomDirection = randomDirection.normalized;

        if (Vector3.Dot(currentDirection, randomDirection) > 0.8f)
        {
            randomDirection = Vector3.Reflect(randomDirection, currentDirection);
        }

        return randomDirection;
    }

    void ShootLightning()
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, firePoint.position);

        int chainCount = 0;
        Vector3 targetPosition = firePoint.position;
        EnemyHealth currentEnemyHealth = null;

        while (chainCount < maxChainCount)
        {
            RaycastHit hit;
            if (Physics.Raycast(targetPosition, GetRandomDirection(lineRenderer.GetPosition(lineRenderer.positionCount - 2) - targetPosition), out hit, maxChainDistance, targetMask))
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                currentEnemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (currentEnemyHealth != null)
                {
                    float distance = Vector3.Distance(targetPosition, currentEnemyHealth.transform.position);
                    float damage = Mathf.Lerp(0f, maxDamage, 1f - distance / maxChainDistance);

        
                    if (Time.time >= nextDamageTime)
                    {
                        currentEnemyHealth.TakeDamage(damage);
                        nextDamageTime = Time.time + 1f / damageRate;
                    }

                    targetPosition = currentEnemyHealth.transform.position;
                    chainCount++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        if (lineRenderer.positionCount == 1)
        {
            lineRenderer.SetPosition(1, firePoint.position + firePoint.forward * maxChainDistance);
        }

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, targetPosition);
    }
}
