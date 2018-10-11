using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeInfo : MonoBehaviour {
	public GameController gameController;
    public PlaceManager placeManager;
    public Spawner spawner;

	public GameObject[] playerSpawnPoints;

	public GameObject enemyPrefab;
	public GameObject[] enemySpawnPoints;
	int nextEnemySPIndex = 0;
    public CharacterConfig testEnemyCharacter;

    // Spawn points for 
    public Transform firstEnemySpawnPoint;
    public Transform[] wave2SpawnPoints;
    int wave2AliveEnemies = 0;
    public Transform[] wave3SpawnPoints;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindObjectOfType<GameController> ();
        placeManager = GameObject.FindObjectOfType<PlaceManager>();
        spawner = GameObject.FindObjectOfType<Spawner>();

		SpawnPlayers ();
        //	SpawnEnemies ();
        StartDemo();
		
	}
	
	// Update is called once per frame
    void Update () {


        if (Input.GetMouseButtonDown(0)) {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 position2d = new Vector2(worldPosition.x, worldPosition.y);
            GameObject enemy = spawner.SpawnEnemy(position2d, testEnemyCharacter);
        }
		
	}

	void SpawnPlayers() {
        // gameController.InitPlayer (1, "Cleric");
        GameObject player1 = gameController.InitPlayer(1, "Cleric");
        placeManager.RegisterPlayer(1, player1);

        GameObject player2 = gameController.InitPlayer (2, "Tank");
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


    void StartDemo() {
        // Populate wave3SpawnPoints
        GameObject parent = GameObject.Find("Wave3");
        Debug.Log("Found wave3 object: " + parent.name);
        int i = 0;
        foreach (Transform spawnPoint in parent.transform) {
            wave3SpawnPoints[i] = spawnPoint;
            i++;
        }

        SpawnWave3();


        // Spawn first enemy and register second wave to start 
        //GameObject enemy = spawner.SpawnEnemy(firstEnemySpawnPoint, 
        //                                      testEnemyCharacter);
        //enemy.GetComponent<BeingController>().OnDied += SpawnWave2;


        // TODO: Deregister from the same OnDied function later
        // When I design the actual WaveTracker thing, I should make sure to do
        // this for all waves
    }


    public void SpawnWave2() {

        foreach (Transform position in wave2SpawnPoints) {
            GameObject enemy = spawner.SpawnEnemy(position, testEnemyCharacter);
            wave2AliveEnemies++;
            enemy.GetComponent<BeingController>().OnDied += Wave2Death;
        }

    }

    void Wave2Death() {
        wave2AliveEnemies--;

        // If we've reached 0 alive enemies, wave is over
        if (wave2AliveEnemies == 0) {
            SpawnWave3();
        }
    }

    public void SpawnWave3()
    {


        foreach (Transform position in wave3SpawnPoints)
        {
            spawner.SpawnEnemy(position, testEnemyCharacter);
        }

    }
}
