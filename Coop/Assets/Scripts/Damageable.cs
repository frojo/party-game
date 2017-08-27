using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

	[HideInInspector] public int damage = 1;

	// Whether attack has knockback
	[HideInInspector] public bool knockback = false;

	[HideInInspector] public bool goingRight = true;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init(int damageArg, bool knockbackArg, bool goingRightArg) {
		damage = damageArg;
		knockback = knockbackArg;
		goingRight = goingRightArg;
	}
}
