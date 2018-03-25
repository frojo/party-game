using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : Ultimate {

	public float stunDuration;

	public GameObject stunCirclePrefab;
	GameObject stunCircle;

	public override void HandleButtonDown(
		Vector2 leftStickInput, 
		Transform characterTransform)
	{
		if (UseUltCharge())
		{
			StunAllEnemies ();
		}
	}

	void StunAllEnemies() {
		stunCircle = Instantiate(stunCirclePrefab);
		stunCircle.GetComponent<StunCircle>().Init(stunDuration);
		// Create sink collider and place it
		StartCoroutine (_DestroyStunCircleAfter (0));
	}

	IEnumerator _DestroyStunCircleAfter(float duration) {
		yield return new WaitForSeconds (duration);
		Destroy(stunCircle);
	}
		
}
