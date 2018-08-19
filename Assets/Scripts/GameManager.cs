using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    protected static GameManager _self;
    public static GameManager Self { get { return _self; } }
    protected static float pizzaValueDecayInterval = 1.0f;
    public bool runTimers = true;

    //public UI GameUI;

    public List<Pizza> ActiveDeliveries;
    //Used to track game progression
    public int TotalDeliveries = 0;

    public int PlayerScore = 0;

	// Use this for initialization
	protected virtual void Awake () {
        if(_self)
        {
            //Singleton, there should be not be other objects of this class.
            Debug.LogWarning("Multiple GameManagers found - deleting extra.");
            Destroy(this);
        }
        else
        {
            _self = this;
        }
	}

    protected virtual void Start()
    {
        StartCoroutine(PizzaValueDecayTimer());
    }

    IEnumerator PizzaValueDecayTimer()
    {
        float timePassed = 0.0f;
        while (true)
        {
            if (runTimers)
            {
                if (timePassed >= pizzaValueDecayInterval)
                {
                    foreach(Pizza p in ActiveDeliveries)
                    {
                        p.DecayValue();
                    }
                    timePassed = 0.0f;
                }
                timePassed += Time.deltaTime;
            }
            yield return null;
        }
    }
}
