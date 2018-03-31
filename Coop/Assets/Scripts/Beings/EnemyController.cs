using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BeingController {

	public GameObject target;

    public GameObject rangeArea;

    // Whether or not this enemy is in range of the target
    public bool isInRange;

	// Use this for initialization
	void Start () {
		FindTarget ();
	}
	
	// Update is called once per frame
	void Update () {

        // If in range, attack. otherwise, get in range
        if (isInRange)
        {
            Debug.Log("Attack attack attack!");
        } else
        {
            TryToGetInRange();
        }
	}

    void FindTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        GameObject hitbox = target.GetComponentInChildren<BoxCollider2D>().gameObject;
        SetRangeArea(hitbox);
    }

    public virtual void SetRangeArea(GameObject hitbox)
    {
        rangeArea.transform.position = hitbox.transform.position;
        rangeArea.transform.SetParent(hitbox.transform);
    }

    void TryToGetInRange()
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
                target.transform.position, speed * Time.deltaTime * 8);
        }
    }



}
