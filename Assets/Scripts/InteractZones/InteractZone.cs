using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractZone : MonoBehaviour {
    //The 3 types of InteractZone: DELIVERY, PLANET, and RESTAURANT

    protected Collider2D _col;

	// Use this for initialization
	protected virtual void Start () {
        _col = gameObject.GetComponent<Collider2D>();
        if(!_col)
        {
            Debug.LogError(name + "'s InteractZone does not have a collider!");
        }
	}

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        SpaceshipPawn SP = collision.GetComponent<SpaceshipPawn>();
        if(SP)
        {
            GameManager.Self.interactionIndicator.InInteractZone(true);
            if(SP.AbleToInteract)
            {
                HasSuccessfullyInteracted();
            }
        }
    }

    //Override this on the subsequent types.
    protected virtual void HasSuccessfullyInteracted()
    {

    }
}
