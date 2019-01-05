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


    // Wave Configs
    public WaveConfig wave1;
    public WaveConfig wave2;

    public WaveConfig[] waves;

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


    void StartDemo() {

        Debug.Log("Starting demo");

        //wave1.OnWaveDone += wave2.StartWave;
        //wave1.StartWave();

        for (int i = 0; i < waves.Length - 1; i++)
        {
            waves[i].OnWaveDone += waves[i + 1].StartWave;
        }
        waves[0].StartWave();
    }

}
