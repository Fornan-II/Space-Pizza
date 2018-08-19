using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IZ_DeliveryZone : InteractZone {

    public int orderID = -1;

    protected override void HasSuccessfullyInteracted()
    {
        for(int i = 0; i < GameManager.Self.ActiveDeliveries.Count; i++)
        {
            Pizza p = GameManager.Self.ActiveDeliveries[i];
            if (p.NumericID == orderID)
            {
                GameManager.Self.PlayerScore += p.Value;
                GameManager.Self.TotalDeliveries++;
                GameManager.Self.ActiveDeliveries.Remove(p);
                i--;
                DeliveryMade();
            }
        }
    }

    protected virtual void DeliveryMade()
    {
        Destroy(gameObject, 1.0f);
    }
}
