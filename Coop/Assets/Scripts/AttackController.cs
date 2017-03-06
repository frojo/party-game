using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

	public float duration = 1f;
	public float range = 0.001f;

	public GameObject player;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator _DoAttack(Vector2 direction) {
		
		yield return new WaitForSeconds (duration);
		gameObject.SetActive (false);
	}

	public void DoAttack(Vector2 direction) {
		print ("ATtack!");
		transform.localPosition = direction * range;
		gameObject.SetActive (true);

		StartCoroutine(_DoAttack (direction));
	}

	void OnTriggerEnter2D(Collider2D other) {
		print ("hello I am the attack and i will destory you!");
		Destroy (other.gameObject);
	}
}
