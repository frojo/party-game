using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : Ability {

	public float speed = 1f;
	public float duration = 5f;
	// public new float cooldownDuration = .5f;

	public GameObject projectile;


	// TODO: We want this to cause knockback on enemies

	public override void Init() {
        base.Init(false);
		projectile = transform.Find ("ProjectileObj").gameObject;
		projectile.SetActive (false);
		base.cooldownDuration = cooldownDuration;
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

//	private int GetDirectionMultiplier(Transform characterTransform) {
//		bool facingRight = characterTransform.GetComponent<PlayerController>().facingRight;
//		return facingRight ? 1 : -1;
//	}
		
	public void FireProjectile(Transform characterTransform) {

		// Instantiate copy of projectile and set it to move in a certain direction

		GameObject projectileCopy = Instantiate (projectile);
		//attack.transform.parent = characterTransform;

		projectileCopy.transform.position = transform.position;
		projectileCopy.SetActive (true);
		projectileCopy.GetComponent<MovingDamageable>().Init(5, false, 
			characterTransform.GetComponent<BeingController>().facingRight,
			speed);
	}
}
