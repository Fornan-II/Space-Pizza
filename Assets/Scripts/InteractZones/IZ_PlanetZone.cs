using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IZ_PlanetZone : InteractZone
{
    protected override void HasSuccessfullyInteracted()
    {
        //These 3 lines probably want to be in a coroutine in a loading level thing - check Brackeys
        GameManager.Self.runTimers = false;
        //Load other scene and Generate Level
        GameManager.Self.runTimers = true;
    }

}
