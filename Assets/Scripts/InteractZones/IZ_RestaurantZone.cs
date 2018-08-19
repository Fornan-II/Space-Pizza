using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IZ_RestaurantZone : InteractZone
{
    public int minDeliveryValue = 100;

    protected override void HasSuccessfullyInteracted()
    {
        if (GameManager.Self.ActiveDeliveries.Count == 0)
        {
            GenerateDeliveries();
        }
    }

    protected virtual void GenerateDeliveries()
    {
        int deliveryCount = 1;
        int newMinValue = 0;
        if(GameManager.Self.TotalDeliveries > 0)
        {
            deliveryCount += (int)Mathf.Log(Random.Range(GameManager.Self.TotalDeliveries * 0.6f, GameManager.Self.TotalDeliveries),2);
            newMinValue = (int)(100 * Mathf.Log(GameManager.Self.TotalDeliveries));
        }

        
        for (int counter = 0; counter < deliveryCount; counter++)
        {
            GameManager.Self.ActiveDeliveries.Add(new Pizza(minDeliveryValue + newMinValue, minDeliveryValue + 4 * newMinValue));
        }
    }
}
