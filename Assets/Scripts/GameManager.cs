using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    protected static GameManager _self;
    public static GameManager Self { get { return _self; } }
    public HUD playerHUD;
    public Controller player;
    protected static float pizzaValueDecayInterval = 1.0f;
    public bool runTimers = true;

    public float TotalShiftTime = 180.0f;
    protected float _elapsedShiftTime = 0.0f;
    public float TimeRemaining { get { return TotalShiftTime - _elapsedShiftTime; } }

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
        //DontDestroyOnLoad(gameObject);
        StartCoroutine(PizzaValueDecayTimer());
        StartCoroutine(ShiftTimer());
    }

    protected virtual void EndGame()
    {
        Debug.Log("Game Ended!");
        player.possessedPawn = null;
        if (!ScoreManager.LoadScore())
        {
            ScoreManager.HighScore = PlayerScore;
            ScoreManager.SaveScore();
            playerHUD.SetMessage(true, "High Score! ", "" + PlayerScore);
        }
        else if(ScoreManager.HighScore < PlayerScore)
        {
            ScoreManager.HighScore = PlayerScore;
            ScoreManager.SaveScore();
            playerHUD.SetMessage(true, "High Score! ", "" + PlayerScore);
        }
        else
        {
            playerHUD.SetMessage(true, "Final Score: ", "" + PlayerScore);
        }
        StartCoroutine(WaitToReturnToMainMenu());
    }

    protected virtual IEnumerator PizzaValueDecayTimer()
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
                        playerHUD.UpdateDeliveryUI();
                    }
                    timePassed = 0.0f;
                }
                timePassed += Time.deltaTime;
            }
            yield return null;
        }
    }

    protected virtual IEnumerator ShiftTimer()
    {
        while(_elapsedShiftTime < TotalShiftTime)
        {
            yield return null;
            if(runTimers)
            {
                _elapsedShiftTime += Time.deltaTime * Time.timeScale;
                playerHUD.UpdateShiftTimer(TimeRemaining);
            }
        }
        EndGame();
    }

    protected virtual IEnumerator WaitToReturnToMainMenu()
    {
        while(!Input.anyKeyDown)
        {
            yield return null;
        }
        MenuScript ms = FindObjectOfType<MenuScript>();
        ms.ReturnToMainMenu();
    }
}
