using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunCircle : MonoBehaviour {

	private float stunDuration;

	public void Init(float duration) {
		stunDuration = duration;

	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("EnemyHitbox")) {
			BeingController being = other.GetComponent<Hitbox>().being;
			being.Stun (stunDuration);
		}
	}
}
