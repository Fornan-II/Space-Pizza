using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaIndicator : MonoBehaviour {

    protected SpriteRenderer _sr;

    protected List<IZ_DeliveryZone> existingDeliveryZones = new List<IZ_DeliveryZone>();
    protected Transform indicatedDeliveryZoneTransform;

    public bool useIndicator = true;

    // Use this for initialization
    protected virtual void Start () {
        _sr = gameObject.GetComponent<SpriteRenderer>();
        if(_sr)
        {
            _sr.enabled = false;
        }
        else
        {
            Debug.LogError(name + " has no sprite renderer component!");
        }
	}

    protected virtual void FixedUpdate()
    {
        if (existingDeliveryZones.Count > 0 && useIndicator && GameManager.Self.levelTransitioner.IsOpenOrOpenning)
        {
            if(!_sr.enabled)
            {
                _sr.enabled = true;
            }
            if(!indicatedDeliveryZoneTransform)
            {
                indicatedDeliveryZoneTransform = existingDeliveryZones[0].transform;
            }
            
            foreach(IZ_DeliveryZone dz in existingDeliveryZones)
            {
                if(Vector2.Distance(dz.transform.position, transform.position) < Vector2.Distance(indicatedDeliveryZoneTransform.position, transform.position))
                {
                    indicatedDeliveryZoneTransform = dz.transform;
                }
            }

            RotateToFace(indicatedDeliveryZoneTransform.position);
        }
        else if(_sr.enabled)
        {
            _sr.enabled = false;
        }
    }

    protected virtual void RotateToFace(Vector2 position)
    {
        Vector3 p = position;
        p.z = transform.position.z;
        Vector3 desiredVector = p - transform.position;
        Vector3 _desiredDirection = desiredVector;

        float newRotation = Vector3.SignedAngle(Vector3.up, _desiredDirection, Vector3.forward);
        Vector3 currentRotation = new Vector3(0.0f, 0.0f, newRotation);
        transform.rotation = Quaternion.Euler(currentRotation);
    }

    public virtual void AddDeliveryZone(IZ_DeliveryZone dz)
    {
        existingDeliveryZones.Add(dz);
    }

    public virtual void RemoveDeliveryZone(IZ_DeliveryZone dz)
    {
        existingDeliveryZones.Remove(dz);
    }
}
