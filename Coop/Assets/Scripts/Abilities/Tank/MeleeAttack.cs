using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Ability {

	public float duration = 1f;
	public float range = 0.1f;
	public int damage;

    // Distance that this attack knocks back enemies
    public float knockbackDistance = 3f;

    // Whether attacker can move while attacking
    public bool canMoveDuringAttack = true;

	public GameObject attack;


	// TODO: We want this to cause knockback on enemies

	public override void Init(BeingController attacker) {
        base.Init(attacker, false);
		attack = transform.Find ("AttackObj").gameObject;
		attack.SetActive (false);
	}


	public override void HandleButtonDown (
		Vector2 leftStickInput, 
		Transform attackerTransform)
	{
		if (!StartCooldown()) {
			return;
		}
        DoAttack (attackerTransform);
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

        Debug.Log("Enable attacker's movement");
    }

	private int GetDirectionMultiplier(Transform characterTransform) {
		bool facingRight = characterTransform.GetComponent<BeingController>().facingRight;
		return facingRight ? 1 : -1;
	}

	public void DoAttack(Transform attackerTransform) {
        attack.transform.parent = attackerTransform;

        attack.transform.localPosition = Vector3.right * GetDirectionMultiplier(attackerTransform) * range;
		attack.GetComponent<Damageable>().Init (damage, true, knockbackDistance,
                                                attackerTransform.GetComponent<BeingController>().facingRight, owner);
		attack.SetActive (true);

        if (!canMoveDuringAttack) {
            Debug.Log("Disable attacker's movement");
            // Disable movement 
        }

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
