using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeInfo : MonoBehaviour {
	public GameController gameController;

	public GameObject[] playerSpawnPoints;

	public GameObject enemyPrefab;
	public GameObject[] enemySpawnPoints;
	int nextEnemySPIndex = 0;
	public CharacterConfig mook;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindObjectOfType<GameController> ();

		SpawnPlayers ();
		SpawnEnemies ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnPlayers() {
		// gameController.InitPlayer (1, "Cleric");
		gameController.InitPlayer (1, "Cleric");
		gameController.InitPlayer (2, "Tank");

	}

	void SpawnEnemies() {
		foreach (GameObject enemySP in enemySpawnPoints) {
			GameObject enemy = Instantiate (enemyPrefab);
			enemy.GetComponent<BeingController>().Init (mook);
			enemy.transform.parent = transform;
			enemy.transform.position = enemySP.transform.position;
		}
	}

	public Vector3 GetEnemyPrototypeStartingPosition() {
		int i = nextEnemySPIndex % enemySpawnPoints.Length;
		return enemySpawnPoints [i].transform.position;
	}


	public Vector3 GetPlayerPrototypePosition(int playerNum) {
		return playerSpawnPoints [playerNum-1].transform.position;
	}
}
