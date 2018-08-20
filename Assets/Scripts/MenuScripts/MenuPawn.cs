using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPawn : Pawn {

    public Vector2 constantVelocity = Vector2.zero;
    protected Rigidbody2D _rb;

    protected override void Start()
    {
        base.Start();
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        if(_rb && constantVelocity != Vector2.zero)
        {
            _rb.velocity = constantVelocity;
        }
    }
}
