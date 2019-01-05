using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Wave", order = 1)]
public class WaveConfig : ScriptableObject {

    // This is a prefab whose children's Transforms will be used as spawn points
    public GameObject spawnPoints;
    public CharacterConfig enemyType;

    Spawner spawner;

    int aliveEnemies = 0;
    public delegate void WaveDoneAction();
    public event WaveDoneAction OnWaveDone;


    public void StartWave()
    {
        aliveEnemies = 0;
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        Debug.Log("Spawning enemies from wave config " + name);
        foreach (Transform spawnPoint in spawnPoints.transform)
        {
            Debug.Log("Spawning enemy from wave config " + name);
            GameObject enemy = spawner.SpawnEnemy(spawnPoint, enemyType);
            aliveEnemies++;
            enemy.GetComponent<BeingController>().OnDied += EnemyDied;
        }
        Debug.Log("Spawned enemies for " + name + ". Alive enemies = " + aliveEnemies);
    }

    void EnemyDied()
    {
        aliveEnemies--;
        Debug.Log("Enemy dead for " + name + ". Alive enemies = " + aliveEnemies);
        if (aliveEnemies == 0)
        {
            Debug.Log("All enemies dead for " + name);
            OnWaveDone();
        }
    }
}
