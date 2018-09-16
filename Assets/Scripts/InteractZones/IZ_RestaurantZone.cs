using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IZ_RestaurantZone : InteractZone
{
    public int minDeliveryValue = 100;
    public int maximumDeliveryCount = 8;

    protected override void HasSuccessfullyInteracted()
    {
        base.HasSuccessfullyInteracted();
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
        if(deliveryCount > maximumDeliveryCount)
        {
            deliveryCount = maximumDeliveryCount;
        }
        
        for (int counter = 0; counter < deliveryCount; counter++)
        {
            Pizza p = new Pizza();
            p.SetRandomValue(minDeliveryValue + newMinValue, minDeliveryValue + 4 * newMinValue);
            p.SelectRandomPlanet();
            GameManager.Self.ActiveDeliveries.Add(p);
            GameManager.Self.playerHUD.AddDeliveryUI(p);
        }
        SpaceshipPawn sp = (SpaceshipPawn)GameManager.Self.player.possessedPawn;
        if(sp)
        {
            sp.SetHasPizza(true);
        }
    }
}
