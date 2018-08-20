using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryUI : MonoBehaviour {
    protected Pizza _pizza;
    public Pizza TrackedPizza { get { return _pizza; } }
    public Image destinationIcon;
    public Text valueField;

    public virtual void SetPizza(Pizza p)
    {
        _pizza = p;
        if(p == null) { return; }
        SetValue(p.Value);
        SpriteRenderer planetSR = p.DestinationPlanet.GetComponent<SpriteRenderer>();
        if(planetSR)
        {
            destinationIcon.sprite = planetSR.sprite;
        }
        else
        {
            Debug.LogError("Planet " + p.DestinationPlanet.name + " is missing a sprite renderer!");
        }
    }

    public virtual void SetValue(int value)
    {
        valueField.text = "" + value;
    }
}
