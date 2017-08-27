using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDamageable : Damageable {

	public float speed = 1f;
	public Vector3 direction;

	float duration = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + direction * speed;
	}

	public void Init(int damage, bool knockback, bool goingRight, 
		float speedArg) {
		base.Init (damage, knockback, goingRight);
		speed = speedArg;
		direction = goingRight ? Vector2.right : Vector2.left;

		StartCoroutine (Die (duration));
	}

	IEnumerator Die(float duration) {
		yield return new WaitForSeconds (duration);
		Destroy (gameObject);
	}
}
