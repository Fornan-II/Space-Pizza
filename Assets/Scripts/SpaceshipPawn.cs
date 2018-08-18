using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipPawn : Pawn {

    public float rotationSpeed = 30.0f;

    #region Member Variables
    protected Rigidbody2D _rb;
    protected Vector2 _desiredMoveVelocity;
    protected Vector3 _desiredDirection;
    #endregion

    // Use this for initialization
    protected override void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        if (!_rb)
        {
            Debug.LogError("No rigidbody found on pawn!");
        }
    }

    protected virtual void FixedUpdate()
    {
        //transform.rotation = Quaternion.Euler(_desiredDirection);
        float vecDot = Vector3.SignedAngle(Vector3.up, _desiredDirection, -1f * Vector3.forward);
        Debug.Log(vecDot);
        Vector3 currentRotation = new Vector3(vecDot - 90.0f, 90.0f, 0.0f);
        transform.rotation = Quaternion.Euler(currentRotation);

        _rb.velocity = _desiredMoveVelocity;
        Vector2 newVelocity = transform.up * _desiredMoveVelocity.x + transform.forward * _desiredMoveVelocity.y;
        _rb.velocity = newVelocity;
    }

    #region InputRelated
    public override void HandleHorizontal(float value)
    {
        _desiredMoveVelocity.x = value * -1f;
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
        Debug.DrawRay(transform.position, _desiredDirection, Color.red, 1.0f);
        
    }

    public override void HandleSpacebar(bool input)
    {
        //boost
    }

    public override void HandleLeftShift(bool input)
    {
        //slow down
    }
    #endregion
}
