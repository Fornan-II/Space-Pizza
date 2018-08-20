using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public DeliveryUI[] deliveryUIObjects;
    public DeliveryUI totalScoreUIObject;

    public GameObject MessagePanel;
    public Text MessageTitle;
    public Text MessageBody;

    public Text ShiftTimer;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void AddDeliveryUI(Pizza p)
    {
        if(deliveryUIObjects.Length >= 1)
        {
            int i = 1;
            DeliveryUI dui = deliveryUIObjects[0];
            while(dui.gameObject.activeSelf && i < deliveryUIObjects.Length)
            {
                dui = deliveryUIObjects[i];
                i++;
            }

            dui.gameObject.SetActive(true);
            dui.SetPizza(p);
        }
    }

    public virtual void RemoveDeliveryUI(Pizza p)
    {
        bool queriedPizzaFound = false;
        for(int i = 0; i < deliveryUIObjects.Length; i++)
        {
            if(deliveryUIObjects[i].gameObject.activeSelf)
            {
                if (deliveryUIObjects[i].TrackedPizza.NumericID == p.NumericID || queriedPizzaFound)
                {
                    queriedPizzaFound = true;
                    if (i + 1 < deliveryUIObjects.Length)
                    {
                        if (deliveryUIObjects[i + 1].gameObject.activeSelf)
                        {
                            deliveryUIObjects[i].SetPizza(deliveryUIObjects[i + 1].TrackedPizza);
                        }
                        else
                        {
                            deliveryUIObjects[i].gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    public virtual void UpdateDeliveryUI()
    {
        foreach(DeliveryUI dui in deliveryUIObjects)
        {
            if(dui.gameObject.activeSelf)
            {
                dui.SetValue(dui.TrackedPizza.Value);
            }
        }
    }

    public virtual void UpdateScore()
    {
        totalScoreUIObject.SetValue(GameManager.Self.PlayerScore);
    }

    public void SetMessage(bool value)
    {
        MessagePanel.SetActive(value);
    }

    public void SetMessage(bool value, string title, string body)
    {
        MessageTitle.text = title;
        MessageBody.text = body;
        MessagePanel.SetActive(value);
    }

    public void UpdateShiftTimer(float value)
    {
        int min = (int)(value / 60);
        int sec = (int)(value % 60);
        if(sec < 10)
        {
            ShiftTimer.text = min + ":0" + sec;
        }
        else
        {
            ShiftTimer.text = min + ":" + sec;
        }
    }
}
