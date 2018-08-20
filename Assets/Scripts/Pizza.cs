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
        _numericID = IDCounter;
        IDCounter++;

        Value = 0;
    }

    public Pizza(GameObject planet)
    {
        DestinationPlanet = planet;

        _numericID = IDCounter;
        IDCounter++;

        Value = 0;
    }

    public Pizza(int value)
    {
        _numericID = IDCounter;
        IDCounter++;

        Value = value;
    }

    public Pizza(GameObject planet, int value)
    {
        DestinationPlanet = planet;

        _numericID = IDCounter;
        IDCounter++;

        Value = value;
    }
    #endregion

    public virtual void SelectRandomPlanet()
    {
        //Select a planet
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
        int i = Random.Range(0, planets.Length);
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
