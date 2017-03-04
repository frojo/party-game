using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float max_speed;
	public float speed = 1;

	public GameObject projectile;

	Vector2 acceleration;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 left_stick_input = 
			new Vector3 (Input.GetAxis ("Horizontal"),
			             Input.GetAxis ("Vertical"), 0);


		if (Input.GetKeyDown(KeyCode.JoystickButton0)) {
			GameObject projectile_obj = Instantiate (projectile);
			// TODO(frojo): Instantiate this a little bit in front of player
			projectile_obj.transform.position = transform.position;

			if (left_stick_input == Vector3.zero) {
				Vector3 projectile_direction = new Vector3 (0, -1, 0);
			} else {
				Vector3 projectile_direction = left_stick_input;;
			}

			projectile_obj.GetComponent<ProjectileController> ().direction =
				left_stick_input;
		}
			
		// Do movement
		transform.position += left_stick_input * speed;
	}
}
