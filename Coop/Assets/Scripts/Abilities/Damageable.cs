using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

	public int damage = 1;

	// Whether attack has knockback
	[HideInInspector] public bool knockback = false;
    [HideInInspector] public float knockbackDistance = 0f;

    [HideInInspector] public bool goingRight = true;

    // Whether attack stuns
    [HideInInspector] public bool stun = false;
    [HideInInspector] public float stunDuration = 0f;

    protected string enemyHitboxTag = "EnemyHitbox";

    protected string friendlyHitboxTag = "PlayerHitbox";

    // The being that created this damageable. Might be null
    public BeingController owner;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected bool SameTeam(Collider2D other) {
        return other.GetComponent<Hitbox>().team == owner.GetHitbox().team;
    }

    public void Init(int damage, BeingController owner)
    {
        this.damage = damage;
        this.owner = owner;
    }

    public void Init(int damage, bool knockback, float knockbackDistance, 
                     bool goingRight, BeingController owner) {
		this.damage = damage;
		this.knockback = knockback;
		this.knockbackDistance = knockbackDistance;
		this.goingRight = goingRight;
		this.owner = owner;
	}

    public void Init(int damage, bool knockback, float knockbackDistance,
                     bool stun, float stunDuration, bool goingRight, BeingController owner)
    {
        this.damage = damage;
        this.knockback = knockback;
        this.knockbackDistance = knockbackDistance;
        this.stun = stun;
        this.stunDuration = stunDuration;
        this.goingRight = goingRight;
        this.owner = owner;
    }

    public virtual void OnTriggerEnter2D(Collider2D other) {

	//	if (other.tag == enemyHitboxTag) {
		//
        if (other.tag == "Hitbox" && !SameTeam(other)) {
			BeingController being = other.GetComponent<Hitbox> ().being;
			int damageDealt = being.TakeDamage (damage);
            owner.AddUltCharge(damageDealt);
			if (knockback) {
				Vector2 knockbackDirection = goingRight ? Vector2.right : Vector2.left;
				being.KnockBack (knockbackDirection, knockbackDistance);
			}
            if (stun) {
                being.Stun(stunDuration);
            }
		}
	}
		
}
