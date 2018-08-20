using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipPawn : Pawn {

    #region Variables
    //Public
    public float moveSpeed = 5.0f;
    public float boostForce = 10.0f;
    public float inertia = 0.0f;
    public float driftInertia = 1.0f;
    public float interactSpeedMaximum = 1.0f;
    public bool AbleToInteract { get { return _rb.velocity.sqrMagnitude < interactSpeedMaximum * interactSpeedMaximum;  } }
    public Sprite shipNormal;
    public Sprite shipWithPizza;

    //Member
    protected Rigidbody2D _rb;
    protected Vector2 _desiredMoveVelocity;
    protected Vector3 _desiredDirection;
    protected bool _isBraking = false;
    protected bool _isBoosting = false;
    protected ShipFire _sf;
    #endregion

    // Use this for initialization
    protected override void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        if (!_rb)
        {
            Debug.LogError("No rigidbody found on pawn!");
        }
        _sf = gameObject.GetComponentInChildren<ShipFire>();
    }

    protected virtual void FixedUpdate()
    {
        RotateShip();
        SetShipVelocity();
    }

    #region InputRelated
    public override void HandleHorizontal(float value)
    {
        _desiredMoveVelocity.x = value;
    }

    public override void HandleVertical(float value)
    {
        _desiredMoveVelocity.y = value;
    }

    public override void HandleMousePosition(Vector2 input)
    {
        Vector3 i = input;
        i.z = transform.position.z;
        Vector3 desiredVector = i - transform.position;
        _desiredDirection = desiredVector;
        //Debug.DrawRay(transform.position, _desiredDirection, Color.red, 1.0f);
        
    }

    public override void HandleSpacebar(bool value)
    {
        _isBoosting = value;
        _sf.SetFireTo(value);
    }

    public override void HandleLeftShift(bool value)
    {
        _isBraking = value;
    }
    #endregion

    protected virtual void RotateShip()
    {
        float newRotation = Vector3.SignedAngle(Vector3.up, _desiredDirection, Vector3.forward);
        Vector3 currentRotation = new Vector3(0.0f, 0.0f, newRotation);
        transform.rotation = Quaternion.Euler(currentRotation);
    }

    protected virtual void SetShipVelocity()
    {
        //Code for basic ship orientation based movement
        /*_rb.velocity = _desiredMoveVelocity;
        Vector2 newVelocity = transform.up * _desiredMoveVelocity.x + transform.forward * _desiredMoveVelocity.y;
        _rb.velocity = newVelocity;*/

        if(_isBoosting)
        {
            Vector2 newVelocity = Vector2.Lerp(transform.up * boostForce, _rb.velocity, inertia);
            _rb.velocity = newVelocity;
        }
        else if (_isBraking)
        {
            Vector2 newVelocity = Vector2.Lerp(Vector2.zero, _rb.velocity, inertia);
            _rb.velocity = newVelocity;
        }
        else
        {
            _desiredMoveVelocity = GetProperInputVector(_desiredMoveVelocity);
            _desiredMoveVelocity *= moveSpeed;
            if (_desiredMoveVelocity.sqrMagnitude > float.Epsilon)
            {
                Vector2 newVelocity = Vector2.Lerp(_desiredMoveVelocity, _rb.velocity, inertia);
                _rb.velocity = newVelocity;
            }
            else
            {
                Vector2 newVelocity = Vector2.Lerp(Vector2.zero, _rb.velocity, driftInertia);
                _rb.velocity = newVelocity;
            }
        }
    }

    protected virtual Vector2 GetProperInputVector(Vector2 inputVector)
    {
        //Vector2 inputVector = new Vector2(_forwardVelocity, _strafeVelocity);
        Vector2 maxedVector = Vector2.one;

        //Find maximum value 
        if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
        {
            maxedVector.Set(1.0f, inputVector.y / inputVector.x);
            if (inputVector.x < 0.0f)
            {
                maxedVector.x = -1.0f;
            }
        }
        else if (Mathf.Abs(inputVector.x) < Mathf.Abs(inputVector.y))
        {
            maxedVector.Set(inputVector.x / inputVector.y, 1.0f);
            if (inputVector.y < 0.0f)
            {
                maxedVector.y = -1.0f;
            }
        }

        inputVector /= maxedVector.magnitude;

        return new Vector3(inputVector.x, inputVector.y);
    }

    public override void TakeDamage(float value)
    {
        foreach(Pizza p in GameManager.Self.ActiveDeliveries)
        {
            p.Value -= (int)value;
        }
    }

    public virtual void SetHasPizza(bool state)
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        if(sr)
        {
            if (state)
            {
                sr.sprite = shipWithPizza;
            }
            else
            {
                sr.sprite = shipNormal;
            }
        }
        
    }
}
