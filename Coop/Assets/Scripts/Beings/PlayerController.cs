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
				abilities[0].HandleButtonDown (leftStickInput, transform);
			}
		} else if (inputMap.GetButton0KeyUp()) {
			Debug.Log("Bottom Up!");
			if (abilities[0]) {
				abilities[0].HandleButtonUp(leftStickInput, transform);
			}
		}

		if (inputMap.GetButton1Key()) {
			Debug.Log("Right!");
            if (abilities[1])
            {
                abilities[1].HandleButtonDown(leftStickInput, transform);
            }

		} else if (inputMap.GetButton1KeyUp()) {
			Debug.Log("Right up!");
			if (abilities[1]) {
				abilities[1].HandleButtonUp(leftStickInput, transform);
			}
		}
		if (inputMap.GetButton2Key()) {
			// This is the attack button
			if (abilities[2]) {
				abilities[2].HandleButtonDown (leftStickInput, transform);
			}

			Debug.Log("Left!");
		} else if (inputMap.GetButton2KeyUp()) {
			Debug.Log("Left up!");
			if (abilities[2]) {
				abilities[2].HandleButtonUp(leftStickInput, transform);
			}
		}
		if (inputMap.GetButton3Key()) {
			Debug.Log("Top!");
            if (abilities[3])
            {
                abilities[3].HandleButtonDown(leftStickInput, transform);
            }
		} else if (inputMap.GetButton3KeyUp()) {
			Debug.Log("Top up!");
			if (abilities[3]) {
				abilities[3].HandleButtonUp(leftStickInput, transform);
			}
		}

	}

	public override int TakeDamage(int damage) {
		int damageTaken = base.TakeDamage (damage);
		ui.UpdateHealth (healthPoints);
        return damageTaken;
	}

    public override int Heal(int amount)
    {
        int healed = base.Heal(amount);
        ui.UpdateHealth(healthPoints);
        return healed;
    }

    public override void AddUltCharge(float charge)
    {
        base.AddUltCharge(charge);
        ui.UpdateUltCharge(this.ultCharge);
    }

    public override void ResetUltCharge()
    {
        base.ResetUltCharge();
        ui.UpdateUltCharge(0);
    }


    void ApplyPlayerConfig(PlayerConfig player) {
        playerConfig = player;
		transform.GetComponent<SpriteRenderer> ().color = player.color;
	}

	public void Init(PlayerConfig player, CharacterConfig character) {
		gameObject.name = "Player " + player.number+ " (" + character.name + ")";

		gameController = GameObject.FindObjectOfType<GameController> ();
		inputMap = new InputMap (player.number);

		ApplyPlayerConfig (player);
		ApplyCharacterConfig(character);

		ui = Instantiate (playerUIPrefab).GetComponent<PlayerUIController> ();
		ui.Init (player, character, abilities, ult);

		// PROTOTYPE TESTING
		PrototypeInfo prototypeInfo = GameObject.FindObjectOfType<PrototypeInfo> ();
		transform.position = prototypeInfo.GetPlayerPrototypePosition(player.number);
	}

}
