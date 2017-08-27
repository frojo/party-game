using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BeingController {

	//public static int NUM_ABILITIES = 4;

	//public float speed = .2f;
	public int playerNumber;
	public GameObject playerUIPrefab;

	private PlayerUIController ui;
	private InputMap inputMap;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindObjectOfType<GameController> ();
		inputMap = new InputMap (playerNumber);
	}

	// Update is called once per frame
	void Update () {
		Vector2 leftStickInput = inputMap.Get2DStickInput ();

		UpdateCurrentDirection (leftStickInput);
		UpdateAbilities (leftStickInput);

		// Do movement
		Vector3 leftStickInput3d = 
			new Vector3 (leftStickInput.x, leftStickInput.y, 0);
		transform.position += leftStickInput3d * speed;
	}

	void UpdateCurrentDirection(Vector2 stickInput) {
		if (stickInput != Vector2.zero) {
			currentDirection = stickInput.normalized;
		}
		facingRight = (currentDirection.x >= 0);
	}

	//void DoAbility(A
	void UpdateAbilities(Vector2 leftStickInput) {
		 
		if (inputMap.GetButton0KeyDown()) {
			Debug.Log("Bottom!");
			if (abilities[0]) {
				abilities[0].HandleInput (leftStickInput, transform);
			}
		}
		if (inputMap.GetButton1KeyDown()) {
			Debug.Log("Right!");

		}
		if (inputMap.GetButton2KeyDown()) {
			// This is the attack button
			if (abilities[2]) {
				abilities[2].HandleInput (leftStickInput, transform);
			}

			Debug.Log("Left!");
		}
		if (inputMap.GetButton3KeyDown()) {
			Debug.Log("Top!");
		}

	}
		
//	IEnumerator AttackCooldownTimer() {
//		canAttack = false;
//		yield return new WaitForSeconds (2);
//		canAttack = true;
//	}

	public override void TakeDamage(int damage) {
		base.TakeDamage (damage);
		Debug.Log ("Player " + playerNumber + " called base func and will now update health in ui");
		ui.UpdateHealth (healthPoints);
	}

//	void OnTriggerEnter2D(Collider2D other) {
////		if (other.CompareTag("Enemy")) {
////			//gameObject.SetActive (false);
////			healthPoints--;
////			ui.UpdateHealth(healthPoints);
////			if (IsDead()) {
////				gameObject.SetActive (false);
////				gameController.GetComponent<GameController> ().EndGame ();
////			}
////
////		}
//	}

	void ApplyPlayerConfig(PlayerConfig player) {
		playerNumber = player.number;
		transform.GetComponent<SpriteRenderer> ().color = player.color;
	}

	public void Init(PlayerConfig player, CharacterConfig character) {
		gameObject.name = "Player " + player.number+ " (" + character.name + ")";

		ApplyPlayerConfig (player);
		ApplyCharacterConfig(character);

		ui = Instantiate (playerUIPrefab).GetComponent<PlayerUIController> ();
		ui.Init (player, character);

		// PROTOTYPE TESTING
		PrototypeInfo prototypeInfo = GameObject.FindObjectOfType<PrototypeInfo> ();
		transform.position = prototypeInfo.GetPlayerPrototypePosition(player.number);
	}

}
