using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

	public abstract void Init ();

	public abstract void HandleInput (
		Vector2 stickInput,
	Transform characterTransform);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
