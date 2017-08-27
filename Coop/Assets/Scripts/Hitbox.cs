using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {

	BeingController being;

	// Use this for initialization
	void Start () {
		being = GetComponentInParent<BeingController> ();
		if (!being) {
			Debug.Log ("No Being for this hitbox!");
		}
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log("Hitbox for " + transform.parent.name + " triggered by " + other.name);
		Damageable attack = other.GetComponent<Damageable> ();
		if (attack) {
			int damage = attack.damage;
			being.TakeDamage (damage);
			if (attack.knockback) {
				//Debug.Log ("Attack is heavy, so stunning for 2 seconds");
				being.Stun (2);

				Vector2 knockbackDirection = attack.goingRight ? Vector2.right : Vector2.left;
				being.KnockBack (knockbackDirection, 2f);
			}

		}
		// if other is an attack, being should take damage (other should have a damage field)
	}

	// Update is called once per frame
	void Update () {
		
	}
}
