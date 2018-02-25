﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSplash : Ability
{
    // public new float cooldownDuration = 3f;

	public int damage;
	public float splashDuration;
	public float aimingSpeed;

	public bool aiming;
	public Vector3 aimDirection;
	public GameObject splash;

	public GameObject splashPrefab;
	public GameObject aimingReticle;

    public override void Init()
    {
        base.icon = icon;
        base.Init(true);
    }

	public override void HandleButtonUp(Vector2 leftStickInput,
		Transform characterTransform) {
		EndAim ();
	}

    public override void HandleButtonDown(
        Vector2 leftStickInput,
        Transform characterTransform)
    {
        Debug.Log("Magic splash cooldown is " + cooldownDuration);
        if (!StartCooldown())
        {
            return;
        }

		StartAim (characterTransform.GetComponent<PlayerController>().facingRight);

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if (aiming) {
			// Move reticle to the right at a constant speed
			aimingReticle.transform.position += aimDirection * aimingSpeed * Time.deltaTime;
		}

    }

	void StartAim(bool direction) {
		if (aiming) {
			return;
		}
		aiming = true;
		aimDirection = direction ? Vector3.right : Vector3.left;
		aimingReticle.transform.position = transform.position;
		aimingReticle.SetActive (true);
	}

	void EndAim() {
		if (!aiming) {
			return;
		}
		aiming = false;
		Splash (aimingReticle.transform.position);
		aimingReticle.SetActive (false);
	}



	IEnumerator _Splash() {
		yield return new WaitForSeconds (splashDuration);
		Destroy(splash);
	}

	void Splash(Vector3 position) {
		// Make the splash/explosion happen
		// Set position of splash
		//
		splash = Instantiate(splashPrefab);
		splash.transform.position = position;
		splash.GetComponent<Damageable>().Init (damage, false, false);
		StartCoroutine (_Splash ());

	}
		
}
