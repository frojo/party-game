using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

	[HideInInspector] public int damage = 1;

	// Whether attack has knockback
	[HideInInspector] public bool knockback = false;

	[HideInInspector] public bool goingRight = true;

	[HideInInspector] public float knockbackDistance = 0f;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init(int damageArg, bool hasKnockback, float knockbackDistanceArg, bool goingRightArg) {
		damage = damageArg;
		knockback = hasKnockback;
		knockbackDistance = knockbackDistanceArg;
		goingRight = goingRightArg;
	}

	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log("Hitbox for " + transform.parent.name + " triggered by " + other.name);
		if (other.tag == "EnemyHitbox") {
			BeingController being = other.GetComponent<Hitbox> ().being;
			being.TakeDamage (damage);
			if (knockback) {
				Vector2 knockbackDirection = goingRight ? Vector2.right : Vector2.left;
				being.KnockBack (knockbackDirection, knockbackDistance);
			}
		}
	}
		
}
