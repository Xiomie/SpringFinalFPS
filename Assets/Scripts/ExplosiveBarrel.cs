using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExplosiveBarrel : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public float explosionRadius = 10f;
    public float explosionDamage = 50f;
    public GameObject explosionEffect;

    public GameObject healthBarUI;
    public Slider slider;

    void Start()
    {
        currentHealth = maxHealth;
        slider.value = CalculateHealth();
    }

    private void Update()
    {
        slider.value = CalculateHealth();
        if (currentHealth < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        if (currentHealth <= 0)
        {
            Explode();
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(50);
        }
    }

    float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        // Instantiate the explosion effect
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // Damage enemies in the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            EnemyHealth enemy = collider.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                float damage = (1f - (distance / explosionRadius)) * explosionDamage;
                enemy.TakeDamage(damage);
            }
        }

        // Destroy the barrel
        Destroy(gameObject);
    }
}




