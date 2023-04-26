using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public List<GameObject> projectiles;
    public Transform fireballSpawnPoint;
    public float projectileForce = 1000f;
    public float shootCooldown = 0.5f; 
    private float lastShootTime = 0f;  
    private Camera playerCamera;
    private PlayerWandController wandController;

    void Start()
    {
        playerCamera = Camera.main;
        wandController = GetComponent<PlayerWandController>();
    }

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time > lastShootTime + shootCooldown)
                {
                    ShootProjectile();
                    lastShootTime = Time.time;
                }
            }
        }
    }

    void ShootProjectile()
    {
        int activeWandIndex = wandController.currentWandIndex;
        if (activeWandIndex < 0 || activeWandIndex >= projectiles.Count) return;

        
        Quaternion projectileRotation = playerCamera.transform.rotation;

       
        GameObject projectile = Instantiate(projectiles[activeWandIndex], fireballSpawnPoint.position, projectileRotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        
        projectileRb.useGravity = false;

        Vector3 shootDirection = playerCamera.transform.forward;
        projectileRb.AddForce(shootDirection * projectileForce);
    }
}