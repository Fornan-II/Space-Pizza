using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public Vector2 TrackingPosition;
    public float TrackingDepth = -10.0f;

    protected void Update()
    {
        transform.position = new Vector3(TrackingPosition.x, TrackingPosition.y, TrackingDepth);
    }
}
