using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScorePanel : MonoBehaviour {

    protected float _highScore;
    public Text scoreField;

	// Use this for initialization
	void Start () {
        if(ScoreManager.LoadScore())
        {
            gameObject.SetActive(true);
            scoreField.text = "" + ScoreManager.HighScore;
        }
        else
        {
            gameObject.SetActive(false);
        }
	}
}
