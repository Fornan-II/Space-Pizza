using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBehavior : MonoBehaviour {

    public float maxAmplitude = 1f;
    public float bobSpeed = 1f;
    protected float _baseYCoordinate;

    protected virtual void Start()
    {
        _baseYCoordinate = transform.localPosition.y;
    }

    protected virtual void FixedUpdate()
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = _baseYCoordinate + maxAmplitude * Mathf.Sin(bobSpeed * Time.timeSinceLevelLoad);
        transform.localPosition = newPosition;
    }
}
