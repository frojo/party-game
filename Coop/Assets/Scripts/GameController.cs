using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject gameOverText;

	public GameObject enemiesTest;
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.JoystickButton13)) {;
			RestartGame ();
		}
		
	}

	public void EndGame() {
		gameOverText.SetActive (true);
	}

	void StartGame() {
		// Deactivate GameOverText
		GameObject playerObj = Instantiate (player);
		GameObject enemiesTestObj = Instantiate (enemiesTest);

		//Transform[] newEnemies = enemiesTestObj.GetComponentsInChildren<Transform>();
		foreach (Transform enemy in enemiesTestObj.transform) {
			enemy.GetComponent<EnemyController> ().target = playerObj;
		}
		// Instantiate Enemies
		// Instantiate Player
	}

	void ClearGame() {
		// We assume the player is dead
		GameObject alivePlayer = 
			GameObject.FindGameObjectWithTag ("Player");
		if (alivePlayer) {
			Destroy (alivePlayer);
		}

		// TODO(frojo): Clean this up because it still doesn't
		// clear the EnemiesTest container Gameobject
		// Destroy all enemies
		GameObject[] aliveEnemies =
			GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in aliveEnemies) {
			Destroy (enemy);
		}

		gameOverText.SetActive(false);
	}

	void RestartGame() {
		print ("Restart!!!");
		ClearGame ();
		StartGame();
	}
}
