using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float speed = 0.001f;

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
		if (!target) {
			FindTarget ();
		}
		transform.position = Vector2.MoveTowards (transform.position,
			target.transform.position, speed * Time.deltaTime);
	}

}
