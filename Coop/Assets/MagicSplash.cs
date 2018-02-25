using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSplash : Ability
{
    // public new float cooldownDuration = 3f;

    public override void Init()
    {
        base.icon = icon;
        base.Init(true);
    }

    public override void HandleInput(
        Vector2 leftStickInput,
        Transform characterTransform)
    {
        Debug.Log("Magic splash cooldown is " + cooldownDuration);
        ui.StartCoolDown(cooldownDuration);
        if (!StartCooldown())
        {
            return;
        }


        //		FireProjectile (characterTransform);

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
