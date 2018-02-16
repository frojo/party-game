using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BeingController {

	//public static int NUM_ABILITIES = 4;

	//public float speed = .2f;
	// public int playerNumber;
	public GameObject playerUIPrefab;

	private PlayerUIController ui;
	private InputMap inputMap;
    private PlayerConfig playerConfig;


	// Use this for initialization
	void Start () {
		
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
		 
		if (inputMap.GetButton0Key()) {
			Debug.Log("Bottom!");
			if (abilities[0]) {
				abilities[0].HandleInput (leftStickInput, transform);
			}
		}
		if (inputMap.GetButton1Key()) {
			Debug.Log("Right!");

		}
		if (inputMap.GetButton2Key()) {
			// This is the attack button
			if (abilities[2]) {
				abilities[2].HandleInput (leftStickInput, transform);
			}

			Debug.Log("Left!");
		}
		if (inputMap.GetButton3Key()) {
			Debug.Log("Top!");
		}

	}

	public override void TakeDamage(int damage) {
		base.TakeDamage (damage);
		Debug.Log ("Player " + playerConfig.number + " called base func and will now update health in ui");
		ui.UpdateHealth (healthPoints);
	}


	void ApplyPlayerConfig(PlayerConfig player) {
        playerConfig = player;
		transform.GetComponent<SpriteRenderer> ().color = player.color;
	}

	void LinkAbilitiesWithUI() {
		foreach (Ability ability in abilities) {
			// Instantiate 
			AbilityUI abilityUI = Instantiate(gameController.abilityUIPrefab).GetComponent<AbilityUI>();
			abilityUI.Init (ui);
		}
	}

	public void Init(PlayerConfig player, CharacterConfig character) {
		gameObject.name = "Player " + player.number+ " (" + character.name + ")";

		gameController = GameObject.FindObjectOfType<GameController> ();
		inputMap = new InputMap (player.number);

		ApplyPlayerConfig (player);
		ApplyCharacterConfig(character);

		ui = Instantiate (playerUIPrefab).GetComponent<PlayerUIController> ();
		ui.Init (player, character);

		LinkAbilitiesWithUI ();

		// PROTOTYPE TESTING
		PrototypeInfo prototypeInfo = GameObject.FindObjectOfType<PrototypeInfo> ();
		transform.position = prototypeInfo.GetPlayerPrototypePosition(player.number);
	}

}
