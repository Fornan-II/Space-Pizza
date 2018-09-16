using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IZ_DeliveryZone : InteractZone {

    public int orderID = -1;
    protected bool _awaitingDelivery = true;

    protected override void Start()
    {
        base.Start();
        GameManager.Self.indicator.AddDeliveryZone(this);
    }

    protected override void HasSuccessfullyInteracted()
    {
        base.HasSuccessfullyInteracted();
        if (_awaitingDelivery)
        {
            for (int i = 0; i < GameManager.Self.ActiveDeliveries.Count; i++)
            {
                Pizza p = GameManager.Self.ActiveDeliveries[i];
                if (p.NumericID == orderID)
                {
                    IZ_PlanetZone pz = p.DestinationPlanet.GetComponent<IZ_PlanetZone>();
                    if(pz)
                    {
                        pz.activeDeliveryPOIs.Remove(this);
                    }

                    GameManager.Self.PlayerScore += p.Value;
                    GameManager.Self.playerHUD.UpdateScore();
                    GameManager.Self.TotalDeliveries++;
                    GameManager.Self.playerHUD.RemoveDeliveryUI(p);
                    GameManager.Self.ActiveDeliveries.Remove(p);
                    i--;
                    DeliveryMade();
                }
            }
        }
        
    }

    protected virtual void DeliveryMade()
    {
        if(GameManager.Self.ActiveDeliveries.Count <= 0)
        {
            SpaceshipPawn sp = (SpaceshipPawn)GameManager.Self.player.possessedPawn;
            if (sp)
            {
                sp.SetHasPizza(false);
            }
        }
        GameManager.Self.indicator.RemoveDeliveryZone(this);
        Destroy(gameObject);
    }
}
