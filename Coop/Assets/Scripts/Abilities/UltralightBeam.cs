using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltralightBeam : Ability {

	public float abilityDuration = 5f;

	public override void Init() {
        base.icon = icon;
		base.Init (true);
	}

    public override void HandleButtonDown(
		Vector2 leftStickInput, 
		Transform characterTransform)
	{
        Debug.Log("Ultralight beam cooldown is " + cooldownDuration);               
       // ui.StartCoolDown(cooldownDuration);
		if (!StartCooldown ()) {
			return;
		}
        

	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

}
