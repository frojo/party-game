using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericProjectile : MovingDamageable {

    // How much this thing heals
    public int healAmount;

    public void Init(int damage, bool knockback, bool goingRight, BeingController owner,
        float speedArg, int heal)
    {
        base.Init(damage, knockback, goingRight, owner, speedArg);
        healAmount = heal;

    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Cleric has hit!");
        // Let the subclass handle the damage stuff
        base.OnTriggerEnter2D(other);


        // Cleric projectiles can heal allies
        if (other.CompareTag(friendlyHitboxTag)) {
            int amountHealed = other.GetComponent<Hitbox>().being.Heal(healAmount);
            owner.AddUltCharge(amountHealed);
        } else if (other.CompareTag(enemyHitboxTag))
        {
            Destroy(gameObject);
        }
        {

        }
    }
}
