using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: We are almost definitely going to want to refactor this using
// a ScriptableObject
public class CharacterConfigOld {

	// Defaults
	// Player gameobject
	int healthPoints = 69;
	Color32 color = new Color32(69, 69, 69, 69);
	float size = .69f;
	float speed = .69f;

	// Abilities
//	Ability button0Ability;
//	Ability button1Ability;
//	Ability button2Ability;
//	Ability button3Ability;

	public GameObject[] tankAbilities;
	public GameObject[] clericAbilities;

	// 


	Vector3 prototype_starting_position = new Vector3(69, 69, 0);

	public CharacterConfigOld(string character) {

		//playerNumber = playerNum;
		switch (character) {
		case "Tank":
			healthPoints = 20;
			prototype_starting_position [0] = 0;
			prototype_starting_position [1] = 0;
			color = new Color32 (66, 120, 0, 255);
			size = 2f;
			speed = .5f;

			break;
		case "Cleric":
			healthPoints = 10;
			prototype_starting_position [0] = 10;
			prototype_starting_position [1] = 0;
			color = new Color32 (66, 120, 0, 255);
			size = 1f;
			speed = 1f;
			break;
		default:
			break;

		}
	}

	void AttachAbility(PlayerController playerController, Ability ability) {
//		playerController.Button0Ability = ability;

	}

	public void ApplyCharacterConfig(PlayerController playerController) {
	
		//playerController.playerNumber = playerNumber;
		playerController.healthPoints = healthPoints;
		playerController.transform.GetComponent<SpriteRenderer> ().color = color;
		playerController.transform.localScale *= size;
		playerController.speed *= speed;

		// AttachAbility(ability, 


		//TODO: DELETE LATER. THIS IS FOR PROTOTYPE
		playerController.transform.position = prototype_starting_position;

	}

	public void ApplyUICharacterConfig(PlayerUIController playerUIController) {


	}
		
}
