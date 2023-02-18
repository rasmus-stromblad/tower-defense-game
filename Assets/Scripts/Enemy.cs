using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float worth = 100;

    public int valueIfDead = 50;

    public GameObject deathEffect;

    private bool isDead = false;

    void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        worth -= amount;

        if (worth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow(float amount)
    {
        // Decrease speed by some percent. Ex amount is 0.3, (1-0.3) = 0.7. Then speed = 70% of the startSpeed
        speed = startSpeed * (1f - amount);
    }

    void Die()
    {
        isDead = true;
        PlayerStats.money += valueIfDead;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

    
}
