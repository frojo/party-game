using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character", order = 1)]
public class CharacterConfig : ScriptableObject {

	// Stats
	public int healthPoints = 69;
	//public Color32 color = new Color32(69, 69, 69, 69);
	public float size = .69f;
	public float speed = .69f;

	// Abilities
	// All objects should have a script that inherits from "Ability" attached
	public GameObject[] abilities;
}
