using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public int damage = 50;

    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;
    
    public void Seek (Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        // If bullet loses its target
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // If we hit the target
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Move the bullet
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        // Point at the bullet
        transform.LookAt(target);

    }

    void HitTarget()
    {
        // Create and then destroy particle effect after 2 seconds
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);

        // Basically, if it is a missile we want to damage enemies in a radius. Otherwise, just the enemy that is hit
        if(explosionRadius > 0f)
        {
            Explode();
        } 
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        // Check colliders inside a radius from a position. Put every object in range in a list
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if(e != null)
        {
            e.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
