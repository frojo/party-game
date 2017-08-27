using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour {

	PlayerHealthController healthUI;

	static int MAX_PLAYERS = 4;
	static float MARGIN_FRACTION = .1f;
	float marginLength;
	float intervalLength;


	public void UpdateHealth(int health) {
		Debug.Log ("Updating health for " + name);
		healthUI.SetHealth (health);
	}
		
	void ApplyPlayerConfig(PlayerConfig player) {
		// Set the position using the player number
		float xPos = marginLength + (player.number-1) * intervalLength;
		transform.localPosition = new Vector3 (xPos, 0, 0);
//		transform.position = 

		// Set the color using the player color
	
	}

	void ApplyCharacterConfig(CharacterConfig character) {
		
	}

	void AttachToCanvas() {
		GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");
		transform.SetParent (canvas.transform);

		float canvasWidth = canvas.GetComponent<RectTransform> ().sizeDelta.x;
		marginLength = canvasWidth * MARGIN_FRACTION;
		intervalLength = (canvasWidth - 2 * marginLength) / MAX_PLAYERS;
	}

	public void Init(PlayerConfig player, CharacterConfig character) {
		gameObject.name = "Player " + player.number+ " UI";

		AttachToCanvas ();
		ApplyPlayerConfig (player);

		healthUI = transform.Find ("Health")
			.GetComponent<PlayerHealthController> ();
		healthUI.Init (player, character);
	}

	// Use this for initialization
	void Start () {

		// Set correct position
		// Set correct size
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
