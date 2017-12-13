using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

	protected float cooldownDuration = 2f;
	protected bool onCooldown = false;

	protected AbilityUI ui;

	//public abstract void Init ();
	public abstract void Init ();

	// TODO: Initialize the UI
	public void Init(bool hasUI) {
		// If this is an ability with a UI component, 
		// create one and set abilityUI equal to it
//		if (hasUI) {
//			AbilityUI abilityUIPrefab = FindObjectOfType<GameController> ().abilityUIPrefab;
//			ui = Instantiate (abilityUIPrefab).GetComponent<AbilityUIController> ();
//			ui.Init (player);
//		}
	}

	public abstract void HandleInput (
		Vector2 stickInput,
		Transform characterTransform);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public bool StartCooldown() {
		if (onCooldown) {
			return false;
		}
		onCooldown = true;
		StartCoroutine (CooldownTimer (cooldownDuration));
		return true;
	}
		
	IEnumerator CooldownTimer(float duration) {
		yield return new WaitForSeconds (duration);
		Debug.Log ("Cooldown timer ending for " + name);
		onCooldown = false;
	}
}
