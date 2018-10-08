using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeInfo : MonoBehaviour {
	public GameController gameController;
    public PlaceManager placeManager;

	public GameObject[] playerSpawnPoints;

	public GameObject enemyPrefab;
	public GameObject[] enemySpawnPoints;
	int nextEnemySPIndex = 0;
    public CharacterConfig testEnemyCharacter;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindObjectOfType<GameController> ();
        placeManager = GameObject.FindObjectOfType<PlaceManager>();

		SpawnPlayers ();
		SpawnEnemies ();
		
	}
	
	// Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.F)) {
            Debug.Log("Got input!");
            placeManager.GoToNextPlace();
        }
		
	}

	void SpawnPlayers() {
        // gameController.InitPlayer (1, "Cleric");
        GameObject player1 = gameController.InitPlayer(1, "Tank");
        placeManager.RegisterPlayer(1, player1);

        GameObject player2 = gameController.InitPlayer (2, "Cleric");
        placeManager.RegisterPlayer(2, player2);
    }

	void SpawnEnemies() {
		foreach (GameObject enemySP in enemySpawnPoints) {
			GameObject enemy = Instantiate (enemyPrefab);
            enemy.GetComponent<BeingController>().Init (testEnemyCharacter);
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
