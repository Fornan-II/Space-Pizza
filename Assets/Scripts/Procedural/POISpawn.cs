using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POISpawn : MonoBehaviour {

    public GameObject DeliveryPrefab;
    public GameObject DangerousPrefab;
    protected bool _hasGenerated = false;
    protected GameObject _thisPOI;

    public virtual IZ_DeliveryZone GenerateAsDelivery(int orderID)
    {
        if(_hasGenerated) { return null; }
        _thisPOI = Instantiate(DeliveryPrefab, transform);
        IZ_DeliveryZone dz = _thisPOI.GetComponent<IZ_DeliveryZone>();
        dz.orderID = orderID;

        _hasGenerated = true;

        return dz;
    }

    public virtual void GenerateAsDangerousObject()
    {
        if(_hasGenerated) { return; }
        _thisPOI = Instantiate(DangerousPrefab, transform);

        _hasGenerated = true;
    }

    public virtual void DestroyPOI()
    {
        Destroy(_thisPOI);
        Destroy(gameObject);
    }
}
