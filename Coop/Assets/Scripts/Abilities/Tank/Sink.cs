using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : Ability
{
	// How far out from the character is the center of the sink
	public float distance;

	// Dictates the size of the affected area
	public float radius;

	// How long the sink collider lasts. Should be fraction of a second
	public float sinkDuration;

	// How enemies are stunned after getting hit by ability
	public float stunDuration;

	public GameObject sinkColliderPrefab;
	public GameObject sinkCollider;

	public override void Init(BeingController being)
	{
		base.Init(being, true);
	}

	public override void HandleButtonDown(
		Vector2 leftStickInput,
		Transform characterTransform)
	{
		Debug.Log("Sink!");

		if (!StartCooldown())
		{
			return;
		}

		DoSink (characterTransform);

	}

	void DoSink(Transform characterTransform) {
		// Calculate position of sink
		bool facingRight = characterTransform.GetComponent<PlayerController> ().facingRight;
		Vector3 aimDirection = facingRight ? Vector3.right : Vector3.left;
		Vector3 sinkPosition = characterTransform.position + aimDirection * distance;

		// Create sink collider and place it
		sinkCollider = Instantiate (sinkColliderPrefab);
		sinkCollider.GetComponent<SinkCollider>().Init (stunDuration);
		sinkCollider.GetComponent<CircleCollider2D> ().radius = radius;
		sinkCollider.transform.position = sinkPosition;
		StartCoroutine (_DestroySinkAfter (sinkDuration));

		// TODO: After we make sure we have the colliding functionality part working, we can 
		// scale the test gameobj back to 1 and then use this to set a sane radius



	}

	IEnumerator _DestroySinkAfter(float duration) {
		yield return new WaitForSeconds (duration);
		Destroy(sinkCollider);
	}
		

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{


	}


}

