using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

	protected float cooldownDuration = 2f;
	protected bool onCooldown = false;

	public abstract void Init ();

	public abstract void HandleInput (
		Vector2 stickInput,
	Transform characterTransform);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void StartCooldown() {
		onCooldown = true;
		StartCoroutine (CooldownTimer (cooldownDuration));

	}
		
	IEnumerator CooldownTimer(float duration) {
		yield return new WaitForSeconds (duration);
		Debug.Log ("Cooldown timer ending for " + name);
		onCooldown = false;
	}
}
