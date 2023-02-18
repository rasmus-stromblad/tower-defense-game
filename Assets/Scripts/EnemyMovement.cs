using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavePointIndex = 0;

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.wayPoints[0];
    }

    void Update()
    {
        // Direction to the waypoint
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        // Reset the speed in case it has been decreased by laser
        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        // If the enemy reaches the end
        if (wavePointIndex >= Waypoints.wayPoints.Length - 1)
        {
            EndPath();
            return;
        }

        wavePointIndex++;
        target = Waypoints.wayPoints[wavePointIndex];
    }

    void EndPath()
    {
        PlayerStats.lives --;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
