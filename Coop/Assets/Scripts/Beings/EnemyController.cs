using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BeingController {

	public GameObject target;
    public EnemyHealthbar healthbar;

	public Transform leftTargetPos;
	public Transform rightTargetPos;

	// TODO: Calculate/get this from the enemy's attack
	float attackRange = 2f;

    public enum AIMode { Idle, Attack, Avoid };
    public AIMode aiMode = AIMode.Attack;

    public enum AttackState { FindTarget, GetInRange, DoHit };
    public AttackState attackState = AttackState.FindTarget;

	// Use this for initialization
	void Start () {
		FindTarget ();
	}
	
	// Update is called once per frame
	void Update () {
        // Face enemy in direction of the target
		if (target) {
			facingRight = transform.position.x <= target.transform.position.x;
        } else {

        }

        switch (aiMode)
        {
            case AIMode.Attack:
                Attack();
                break;
            case AIMode.Avoid:
                Debug.LogError("Avoid state not implemented");
                break;
            case AIMode.Idle:
                break;
        }

			
	}

    void Attack() {

        // First determine what attack sub-state we should be in
        if (!target || target.GetComponent<BeingController>().IsDead()) {
            attackState = AttackState.FindTarget;
        } else if (IsInRange()) {
            attackState = AttackState.DoHit;
        } else {
            attackState = AttackState.GetInRange;
        }

        // Execute attack depending on the state
        switch (attackState) {
            case AttackState.FindTarget:
                FindTarget();
                break;
            case AttackState.GetInRange:
                MoveInRange();
                break;
            case AttackState.DoHit:
                DoHit();
                break;
        }


    }

    void FindTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (!target) {
            return;
        }

        Debug.Log("Found target: " + target.name);
		BoxCollider2D hitbox = target.transform.Find ("Hitbox").GetComponent<BoxCollider2D> ();
		SetTargetPositions (hitbox);
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

    void DoHit()
    {
        Vector2 fakeStickInput = new Vector2(facingRight ? 1 : -1, 0);
        // TODO: Figure out what the actual attack is instead of hardcoding
        abilities[2].HandleButtonDown(fakeStickInput, transform);
    }

    public virtual void SetTargetPositions (BoxCollider2D targetHitbox) {
        // Create new target positions if 
        if (!leftTargetPos) {
            leftTargetPos = new GameObject().transform;
        }
        if (!rightTargetPos)
        {
            rightTargetPos = new GameObject().transform;
        }


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

    public override int TakeDamage(int damage) {
        int damageTaken = base.TakeDamage(damage);
        healthbar.Resize((float)healthPoints / maxHealth);
        return damageTaken;
    }

    public override int Heal(int amount)
    {
        int healed = base.Heal(amount);
        healthbar.Resize((float)healthPoints / maxHealth);
        return healed;
    }







}
