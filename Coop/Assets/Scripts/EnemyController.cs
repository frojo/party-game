﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BeingController {

	public GameObject target;

	void FindTarget() {
		target = GameObject.FindGameObjectWithTag ("Player");
	}

	// Use this for initialization
	void Start () {
		FindTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!IsStunned ()) {
			if (!target) {
				FindTarget ();
			}
			transform.position = Vector2.MoveTowards (transform.position,
				target.transform.position, speed * Time.deltaTime*4);
		}
	}

}
