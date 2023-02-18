using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public TMP_Text waveCountdownText;

    public GameManager gameManager;

    private int waveIndex = 0;

    void Start()
    {
        EnemiesAlive = 0;
    }

    void Update()
    {
        // IF there are enemies alive, return from the function
        if(EnemiesAlive > 0)
        {
            return;
        }

        // IF we reach the end of the level
        if(waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            //Disable this script
            this.enabled = false;
        }

        if(countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    //IEnumerator eneblas us to pause things inside
    IEnumerator SpawnWave()
    {
        PlayerStats.rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            // Spawn enemies with the distance of 1/the spawn rate of the wave
            yield return new WaitForSeconds(1f / wave.rate);
        }
        // Number of enemies increses
        waveIndex++;

        

    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
