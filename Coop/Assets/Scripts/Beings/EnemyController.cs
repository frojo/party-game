using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BeingController {

	public GameObject target;

	public Transform leftTargetPos;
	public Transform rightTargetPos;

	// TODO: Calculate/get this from the enemy's attack
	float attackRange = 1.5f;

	// Use this for initialization
	void Start () {
		FindTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		if (target) {
			facingRight = transform.position.x <= target.transform.position.x;
		}

        // If in range, attack. otherwise, get in range
		if (IsInRange())
        {
			Attack ();
        } else
        {
            MoveInRange();
        }
			
	}

	void Attack() {
		Vector2 fakeStickInput = new Vector2 (facingRight ? 1 : -1, 0);
		// TODO: Figure out what the actual attack is instead of hardcoding
		abilities [2].HandleButtonDown (fakeStickInput, transform);
	}

    void FindTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player");
		BoxCollider2D hitbox = target.transform.Find ("Hitbox").GetComponent<BoxCollider2D> ();
		SetTargetPositions (hitbox);
    }

	public virtual void SetTargetPositions (BoxCollider2D targetHitbox) {
		// Set target positions on both sides so that if the NPC is standing
		// there and they attack, they will hit the target
		leftTargetPos.transform.position = new Vector3 (targetHitbox.bounds.min.x - attackRange,
			targetHitbox.bounds.center.y, 0);
		rightTargetPos.transform.position = new Vector3 (targetHitbox.bounds.max.x + attackRange,
			targetHitbox.bounds.center.y, 0);

		// Parent to the target hitbox so that they stay in same position relative to target
		leftTargetPos.SetParent (targetHitbox.transform);
		rightTargetPos.SetParent (targetHitbox.transform);
	}

	bool IsInRange() {
		return Vector3.Distance (transform.position, GetTargetPosition ().position) < 0.01;
	}

	public Transform GetTargetPosition() {
		// Figure out if we want left or right target position (whatever is closest)
		if (transform.position.x <= target.transform.position.x) {
			return leftTargetPos;
		} else {
			return rightTargetPos;
		}

	}
		
    void MoveInRange()
    {
        if (!IsStunned())
        {
            if (!target)
            {
                FindTarget();
                if (!target)
                {
                    return;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position,
				GetTargetPosition().position, speed * Time.deltaTime * 5);
        }
    }



}
