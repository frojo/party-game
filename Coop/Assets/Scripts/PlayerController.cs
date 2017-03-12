using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float max_speed;
	public float speed = 1;
	public bool canAttack = true;

	public GameObject projectile;

	public GameObject attack;

	public GameObject gameController;

	Vector2 acceleration;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 leftStickInput = 
			new Vector2 (Input.GetAxis ("Horizontal"),
				Input.GetAxis ("Vertical"));


		if (Input.GetKeyDown(KeyCode.JoystickButton0)) {
			Attack (leftStickInput);
		}
			
		// Do movement
		Vector3 leftStickInput3d = 
			new Vector3 (leftStickInput.x, leftStickInput.y, 0);
		transform.position += leftStickInput3d * speed;
	}

	void Attack(Vector2 movementVector) {
		// TODO(frojo): Will probably want to change this to not let
		// the player move while they're attacking

		if (canAttack) {
			StartCoroutine (AttackCooldownTimer());

			attack.GetComponent<AttackController>().DoAttack(movementVector.normalized);
			//GameObject attack_obj = Instantiate (attack);
		}
	}

	IEnumerator AttackCooldownTimer() {
		canAttack = false;
		yield return new WaitForSeconds (2);
		canAttack = true;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Enemy")) {
			gameObject.SetActive (false);
			gameController.GetComponent<GameController> ().EndGame ();

		}
	}

//	void FireProjectile(Vector3 leftStickInput3d) {
//		// TODO(frojo): Move all projectile code into projectile object
//		GameObject projectile_obj = Instantiate (projectile);
//		// TODO(frojo): Instantiate this a little bit in front of player
//		projectile_obj.transform.position = transform.position;
//
//		// TODO(frojo): 
//		if (leftStickInput3d == Vector3.zero) {
//			Vector3 projectile_direction = new Vector3 (0, -1, 0);
//		} else {
//			Vector3 projectile_direction = leftStickInput3d;
//		}
//
//		projectile_obj.GetComponent<ProjectileController> ().direction =
//			leftStickInput3d;
//	}
}
