using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour {

    public bool IgnoreDamage = false;

    protected virtual void Start()
    {

    }

    public virtual void HandleHorizontal(float value)
    {

    }
    
    public virtual void HandleVertical(float value)
    {

    }

    public virtual void HandleMousePosition(Vector2 input)
    {

    }

    public virtual void HandleSpacebar(bool input)
    {

    }

    public virtual void HandleLeftShift(bool input)
    {

    }

    //Public method called by other scripts to cause damage to this pawn.
    public virtual void TakeDamage(float value)
    {
        if(IgnoreDamage) { return; }
        ProcessDamage(value);
    }

    //Method that handles what to do with damage. Override this in inheriting classes.
    protected virtual void ProcessDamage(float value)
    {
        
    }
}
