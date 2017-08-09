using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingController : MonoBehaviour {

	public static int NUM_ABILITIES = 4;

	public float speed = .2f;
	//public int playerNumber;

	public int healthPoints;

	// The direction that the player is currently "looking"
	public Vector2 currentDirection;
	public bool facingRight = true;

	//public PlayerHealthController playerHealthUI;
	//public PlayerUIController playerUI;
	public GameObject gameController;
	private InputMap inputMap;

	public Ability[] abilities;

	Vector2 acceleration;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindObjectOfType<GameController> ().gameObject;
		inputMap = new InputMap (0);
	}

	// Update is called once per frame
	void Update () {
		Vector2 leftStickInput = inputMap.Get2DStickInput ();
//			new Vector2 (Input.GetAxis (inputMap.leftStickXAxis),
//				Input.GetAxis (inputMap.leftStickYAxis));

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

	bool IsDead() {
		return (healthPoints <= 0);
	}

	void AttachAbility(int ability_num, CharacterConfig character) {
		if (!character.abilities[ability_num]) {
			Debug.Log ("null Ability prefab!");
			return;
		}
		GameObject abilityObj = Instantiate (character.abilities[ability_num]);
		abilityObj.transform.parent = transform;
		Ability ability = abilityObj.GetComponent<Ability> ();
		ability.Init ();
		abilities [ability_num] = ability;
	}

	public void ApplyCharacterConfig(CharacterConfig character) {
		healthPoints = character.healthPoints;
		//transform.GetComponent<SpriteRenderer> ().color = character.color;
		transform.localScale *= character.size;
		speed *= character.speed;

		// Attach abilities
		for (int i = 0; i < NUM_ABILITIES; i++) {
			AttachAbility (i, character);
		}

		// AttachAbility(ability, 

	}

	//	void PrototypeStartingPosition(int playerNum) {
	//		switch
	//	}

	public void OnTriggerEnter2D(Collider2D other) {
		//
	}

	public void Init(CharacterConfig character) {
		//Debug.Log ("Initing player " + playerNum + " as a " + character.name);
		//gameObject.name = "Player " + playerNum + " (" + character.name + ")";
		ApplyCharacterConfig (character);

		// PROTOTYPE TESTING
		PrototypeInfo prototypeInfo = GameObject.FindObjectOfType<PrototypeInfo> ();
		//transform.position = prototypeInfo.GetPrototypePosition(playerNum);
	}

}
