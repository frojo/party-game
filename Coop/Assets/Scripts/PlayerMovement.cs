using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float max_speed;
	public float speed = 1;

	Vector2 acceleration;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// TEST
		if (Input.GetKey(KeyCode.JoystickButton0)) {
			print ("Hello world!");
		}

		//float left_stick_x = Input.GetAxis ("Horizontal");
		//flaot left_stick_y = Input.GetAxis ("Vertical");

		Vector3 left_stick_input = 
			new Vector3 (Input.GetAxis ("Horizontal"),
			             Input.GetAxis ("Vertical"), 0);

		// Do movement
		transform.position += left_stick_input * speed;
	}
}
