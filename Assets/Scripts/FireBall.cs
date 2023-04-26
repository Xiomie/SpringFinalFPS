using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject explosionEffectPrefab;
    public float explosionRadius = 5f;
    public float explosionForce = 700f;
    public float damage = 50f;
    void OnCollisionEnter(Collision collision)
    {
     
        GameObject explosionEffect = Instantiate(explosionEffectPrefab, collision.contacts[0].point, Quaternion.identity);

      
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            EnemyHealth enemyHealth = collider.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Rigidbody enemyRigidbody = collider.gameObject.GetComponent<Rigidbody>();
                if (enemyRigidbody != null)
                {
                    enemyRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }

  
        Destroy(explosionEffect, 3f);
        Destroy(gameObject);
    }
}