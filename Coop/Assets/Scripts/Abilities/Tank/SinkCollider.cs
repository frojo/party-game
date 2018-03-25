using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkCollider : MonoBehaviour {

	private float stunDuration;

	public void Init(float duration) {

		stunDuration = duration;

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("EnemyHitbox")) {
			// other.GetComponent<SpriteRenderer> ().color = Color.black;

			// Force enemy to center of sink
			GameObject enemy = other.GetComponent<Hitbox>().being.gameObject;
			enemy.transform.position = transform.position;
			enemy.GetComponent<EnemyController> ().Stun (stunDuration);
		}
	}
}
