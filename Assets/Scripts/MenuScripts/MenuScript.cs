using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    protected int activeMenuIndex = -1;
    protected int previousMenuIndex = -1;
    public GameObject[] MenuScreens;
    public int StartingMenu = 0;

    public bool PauseMenuExists = false;

    protected GameManager _gm;
    public float runtimeTimeScale = 1;

    protected bool _isPaused = false;
    public bool IsPaused
    {
        get { return _isPaused; }
    }

    public Text gameLargeText;
    bool glt_active = false;
    float glt_activeTimer = 0.0f;
    public Text gameSmallText;
    bool gst_active = false;
    float gst_activeTimer = 0.0f;

    private void Start()
    {
        foreach(GameObject menu in MenuScreens)
        {
            if(menu)
            {
                menu.SetActive(false);
            }
        }
        ChangeMenuTo(StartingMenu);
    }

    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }

        if(gameLargeText)
        {
            if (glt_active && !_isPaused)   { gameLargeText.gameObject.SetActive(true); }
            else                            { gameLargeText.gameObject.SetActive(false); }
        }

        if(gameSmallText)
        {
            if (gst_active && !_isPaused)   { gameSmallText.gameObject.SetActive(true); }
            else                            { gameSmallText.gameObject.SetActive(false); }
        }

        if (glt_activeTimer > 0.0f)
        {
            glt_activeTimer -= Time.deltaTime;
            if (glt_activeTimer <= 0.0f)
            {
                SetGameLargeText(false);
            }
        }
        if (gst_activeTimer > 0.0f)
        {
            gst_activeTimer -= Time.deltaTime;
            if(gst_activeTimer <= 0.0f)
            {
                SetGameSmallText(false);
            }
        }
    }

    //MAIN MENU FUNCTIONALITY
    //
    //
    public void StartGame()
    {
        SceneManager.LoadScene("Game_Planets");
    }

    //Close splash screen
    public void CloseSplash()
    {
        if(activeMenuIndex == 0)
        {
            ChangeMenuTo(1);
        }
    }

    //Navigate between menus.
    public void ChangeMenuTo(int newMenuIndex)
    {
        //Debug.Log("Previous:" + previousMenuIndex + ", Active:" + activeMenuIndex + ", New:" + newMenuIndex);
        if (newMenuIndex != activeMenuIndex)
        {
            if (0 <= activeMenuIndex && activeMenuIndex < MenuScreens.Length)
            {
                if (MenuScreens[activeMenuIndex])
                {
                    MenuScreens[activeMenuIndex].SetActive(false);
                }
            }
            if (0 <= newMenuIndex && newMenuIndex < MenuScreens.Length)
            {
                if (MenuScreens[newMenuIndex])
                {
                    MenuScreens[newMenuIndex].SetActive(true);
                }
            }

            previousMenuIndex = activeMenuIndex;
            activeMenuIndex = newMenuIndex;
        }
    }

    //Navigate to previous menu
    public void BackToPreviousMenu()
    {
        ChangeMenuTo(previousMenuIndex);
    }

    //Also used in pause menu
    public void QuitGame()
    {
        Application.Quit();
    }

    //
    //
    //PAUSE MENU FUNCTIONALITY
    //
    //
    public void ResumeGame()
    {
        ChangeMenuTo(0);
        Time.timeScale = runtimeTimeScale;
        _isPaused = false;
    }

    public void TogglePause()   //create isPaused variable to determine if paused or not
    {
        if (PauseMenuExists)
        {
            if (!_isPaused)
            {
                ChangeMenuTo(1);
                runtimeTimeScale = Time.timeScale;
                Time.timeScale = 0.0f;
                _isPaused = true;
            }
            else
            {
                ChangeMenuTo(0);
                Time.timeScale = runtimeTimeScale;
                _isPaused = false;
            }
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = runtimeTimeScale;
        SceneManager.LoadScene("Menu");
    }
    //
    //
    //GAME MESSAGE FUNCTIONALITY
    //
    //
    public void SetGameLargeText(bool setEnabled, string message = "", float time = 0.0f)
    {
        if(!gameLargeText) { return; }

        gameLargeText.text = message;

        glt_active = setEnabled;
        if(setEnabled && time > 0.0f)
        {
            glt_activeTimer = time;
        }
    }

    public void SetGameSmallText(bool setEnabled, string message = "", float time = 0.0f)
    {
        if (!gameSmallText) { return; }

        gameSmallText.text = message;

        gst_active = setEnabled;
        if (setEnabled && time > 0.0f)
        {
            gst_activeTimer = time;
        }
    }
}
