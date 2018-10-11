using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject enemyPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject SpawnEnemy(Vector2 spawnPos, CharacterConfig type) {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.GetComponent<BeingController>().Init(type);
        enemy.transform.position = spawnPos;
        return enemy;
    }

    public GameObject SpawnEnemy(Transform spawnPos, CharacterConfig type)
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.GetComponent<BeingController>().Init(type);
        enemy.transform.position = spawnPos.position;
        return enemy;
    }
}
