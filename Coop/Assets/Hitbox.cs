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
		// if other is an attack, being should take damage (other should have a damage field)
	}

	// Update is called once per frame
	void Update () {
		
	}
}
