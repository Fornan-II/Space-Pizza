using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pizza {

    public GameObject DestinationPlanet;

    protected static uint IDCounter = 0;
    protected uint _numericID;
    public uint NumericID { get { return _numericID; } }

    protected int _value;
    public int Value
    {
        get { return _value; }
        set
        {
            _value = value;
            if(_value < 0)
            {
                _value = 0;
            }
        }
    }
    public static int valueDecayAmount = 1;

    #region Constructors
    public Pizza()
    {
        SelectRandomPlanet();

        _numericID = IDCounter;
        IDCounter++;

        SetRandomValue();
    }

    public Pizza(GameObject planet)
    {
        DestinationPlanet = planet;

        _numericID = IDCounter;
        IDCounter++;

        SetRandomValue();
    }

    public Pizza(int value)
    {
        SelectRandomPlanet();

        _numericID = IDCounter;
        IDCounter++;

        _value = value;
    }

    public Pizza(int valMin, int valMax)
    {
        SelectRandomPlanet();

        _numericID = IDCounter;
        IDCounter++;

        SetRandomValue(valMin, valMax);
    }

    public Pizza(GameObject planet, int value)
    {
        DestinationPlanet = planet;

        _numericID = IDCounter;
        IDCounter++;

        _value = value;
    }

    public Pizza(GameObject planet, int valMin, int valMax)
    {
        DestinationPlanet = planet;

        _numericID = IDCounter;
        IDCounter++;

        SetRandomValue(valMin, valMax);
    }
    #endregion

    public virtual void SelectRandomPlanet()
    {
        //Select a planet
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
        int i = Random.Range(0, planets.Length - 1);
        DestinationPlanet = planets[i];
    }

    public virtual void SetRandomValue(int minimum = 0, int maximum = 1000)
    {
        _value = Random.Range(minimum, maximum);
    }

    public virtual void DecayValue()
    {
        Value -= valueDecayAmount;
    }
}
