using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericOrb : Ability {

	public float speed = 1f;
	public float duration = 5f;
    public int damage = 10;
    public int heal = 5;

	public GameObject projectile;


	// TODO: We want this to cause knockback on enemies

	public override void Init(BeingController being) {
        base.Init(being, false);
		projectile = transform.Find ("ClericProjectile").gameObject;
		projectile.SetActive (false);
	}

	public override void HandleButtonDown (
		Vector2 leftStickInput, 
		Transform characterTransform)
	{
		if (!StartCooldown ()) {
			return;
		}
		FireProjectile (characterTransform);

	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
		
	public void FireProjectile(Transform characterTransform) {
		// Instantiate copy of projectile and set it to move in a certain direction

		GameObject projectileCopy = Instantiate (projectile);
		projectileCopy.transform.position = transform.position;
		projectileCopy.SetActive (true);
		projectileCopy.GetComponent<ClericProjectile>().Init(damage, false, 
			characterTransform.GetComponent<BeingController>().facingRight,
			owner, speed, heal);
	}
}
