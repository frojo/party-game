using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour {

	PlayerHealthController healthUI;

	public void UpdateHealth(int health) {
		// healthUI.SetHealth (health);
	}

	// Use this for initialization
	void Start () {
		healthUI = transform.Find ("Health")
			.GetComponent<PlayerHealthController> ();

		// Set correct position
		// Set correct size
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
