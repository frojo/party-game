using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    // DEBUG
    public DebugInfo debugInfo;

	public GameObject gameOverText;

	public GameObject enemiesTest;
	public GameObject playerPrefab;
	public GameObject playerUIPrefab;
	public GameObject abilityUIPrefab;

	public GameObject canvas;

	public CharacterConfig[] characters;
	public PlayerConfig[] players;

	public enum Team {Player, Enemy};

    //public void SetDebugText(string s) {
    //    debugInfo.SetText(s);
    //}

	void Awake() {
		canvas = GameObject.Find ("Canvas");
	}

	// Use this for initialization
	void Start () {
		//canvas = GameObject.Find ("Canvas");
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.JoystickButton13)) {;
			RestartGame ();
		}
		
	}

	CharacterConfig GetCharacterConfig(string character) {
		switch (character) {
		case "Tank":
			return characters[0];
		case "Cleric":
			return characters[1];
		default:
			Debug.Log("Error!! Not a specified character: " + character);
			return characters[0];
		}
	}

	PlayerConfig GetPlayerConfig(int playerNum) {
		return players [playerNum - 1];
	}

	// TODO(frojo): This should be moved into the PlayerController script as "Init" or "Initialize"
    public GameObject InitPlayer(int playerNum, string character) {
		// Debug.Log ("Initing player " + playerNum);
		// Instantiate and init player prefab
		if (!playerPrefab) {
			Debug.Log ("playerPrefab is null");
		}
		GameObject player = Instantiate(playerPrefab);

		player.GetComponent<PlayerController> ().Init (
			GetPlayerConfig(playerNum),
			GetCharacterConfig (character));

        return player;
	}

	public void EndGame() {
		gameOverText.SetActive (true);
	}

	void StartGame() {
		if (!playerPrefab) {
			Debug.Log ("playerPrefab is null");
		}
		GameObject playerObj = Instantiate (playerPrefab);
		if (!enemiesTest) {
			Debug.Log ("enemiesTest is null");
		}
		GameObject enemiesTestObj = Instantiate (enemiesTest);

		foreach (Transform enemy in enemiesTestObj.transform) {
			enemy.GetComponent<EnemyController> ().target = playerObj;
		}

	}

	void ClearGame() {
		// We assume the player is dead
		GameObject alivePlayer = 
			GameObject.FindGameObjectWithTag ("Player");
		if (alivePlayer) {
			Destroy (alivePlayer);
		}

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

	void PrototypeSettings() {
	}
}
