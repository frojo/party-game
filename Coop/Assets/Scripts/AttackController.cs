using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : Ability {

	public float duration = 1f;
	public float range = 0.1f;

	public GameObject attack;


	// TODO: We want this to cause knockback on enemies

	public override void Init() {
		attack = transform.Find ("AttackObj").gameObject;
		attack.SetActive (false);
	}

	public override void HandleInput (
		Vector2 leftStickInput, 
		Transform characterTransform)
	{
		DoAttack (characterTransform);

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator _DoAttack() {
		yield return new WaitForSeconds (duration);
		attack.SetActive(false);
		attack.transform.parent = transform;
	}

	private int GetDirectionMultiplier(Transform characterTransform) {
		bool facingRight = characterTransform.GetComponent<PlayerController>().facingRight;
		return facingRight ? 1 : -1;
	}

	public void DoAttack(Transform characterTransform) {
		attack.transform.parent = characterTransform;

		attack.transform.localPosition = Vector3.right * GetDirectionMultiplier(characterTransform) * range;
		attack.SetActive (true);

		StartCoroutine(_DoAttack());
	}

	void OnTriggerEnter2D(Collider2D other) {
		// TODO: We want this to cause knockback on enemies
		// And also damage them
		//Destroy (other.gameObject);
		// If the other thing is an enemy (or player, or any character), cause a stun effect
		CharacterController characterController = other.transform.GetComponent<CharacterController>();
		if (characterController) {
			// DO stun
		};
	}
}
