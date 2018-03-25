using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltralightBeam : Ultimate {

    // How long the beam lasts. Should only be a second or so
    public float duration;

    // How much damage beam does. Hint: a lot
    public int damage;

    // How much the caster slows down while casting this ult
    public float casterSlowdown;

    // The actual beam that does damage
    public GameObject beamPrefab;

    GameObject beam;

	public override void Init(BeingController being) {
        base.icon = icon;
		base.Init (being, true);
	}

    public override void HandleButtonDown(
		Vector2 leftStickInput, 
		Transform characterTransform)
	{
        if (UseUltCharge())
        {
            owner.FreezeUltCharge();
            Beam();
        }


    }

    IEnumerator _EndBeamAfter()
    {
        yield return new WaitForSeconds(duration);
        Destroy(beam);
        owner.UnfreezeUltCharge();
    }

    void Beam()
    {
        beam = Instantiate(beamPrefab);
        beam.GetComponent<Damageable>().Init(damage, owner);
        
        // Position beam correctly and parent it to owner
        beam.transform.position = owner.transform.position;

        // This centers the beam on the player
        beam.transform.Translate(0, -beam.GetComponent<BoxCollider2D>().size.y / 2, 0);
        beam.transform.SetParent(owner.transform);

        owner.Slow(duration, casterSlowdown);
        StartCoroutine(_EndBeamAfter());
    }


}
