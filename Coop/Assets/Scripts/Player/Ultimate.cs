using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ultimate: Ability
{

    public override void Init(BeingController being)
    {
        // Ultimates always have UI elements
        base.Init(being, true);
    }

    public override abstract void HandleButtonDown(
    Vector2 leftStickInput,
    Transform characterTransform);

    // Returns true if ult can be used, otherwise returns false
    public bool UseUltCharge()
    {
        if (owner.ultCharge >= 100)
        {
            owner.ResetUltCharge();
            return true;
        }
        return false;
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