using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeArea : MonoBehaviour {

    public EnemyController owner;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner.gameObject)
        {
            owner.isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == owner.gameObject)
        {
            owner.isInRange = false;
        }
    }

}
