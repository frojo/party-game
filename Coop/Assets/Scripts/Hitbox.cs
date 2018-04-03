using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {

	public BeingController being;

	public GameController.Team team;

	// Use this for initialization
	void Start () {
		being = GetComponentInParent<BeingController> ();
		if (!being) {
			Debug.Log ("No Being for this hitbox!");
		}
		
	}
}
