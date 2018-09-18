using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLooping : MonoBehaviour {

    public bool DoWorldLooping = true;
    public float WorldPlaceDistance;
    public float WorldPlaceTimeOut = 3.0f;
    protected bool playerInsideLevel = false;
    protected bool coroutineActive = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!DoWorldLooping) { return; }

        Pawn otherPawn = other.GetComponent<Pawn>();
        if (GameManager.Self.player && otherPawn)
        {
            if (otherPawn == GameManager.Self.player.possessedPawn)
            {
                playerInsideLevel = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!DoWorldLooping) { return; }

        Pawn otherPawn = other.GetComponent<Pawn>();
        if (GameManager.Self.player && otherPawn)
        {
            if (otherPawn == GameManager.Self.player.possessedPawn)
            {
                playerInsideLevel = false;
                if(!coroutineActive)
                {
                    StartCoroutine(MoveLevelLocation());
                }
            }
        }
    }

    protected virtual IEnumerator MoveLevelLocation()
    {
        coroutineActive = true;

        Rigidbody2D shipRB = GameManager.Self.player.possessedPawn.GetComponent<Rigidbody2D>();
        Vector3 newWorldPos;
        float timer = 0.0f;

        while (!playerInsideLevel && DoWorldLooping)
        {
            if(timer <= 0.0f)
            {
                newWorldPos = shipRB.velocity.normalized * WorldPlaceDistance;
                if(newWorldPos != Vector3.zero)
                {
                    transform.position = GameManager.Self.player.possessedPawn.transform.position + newWorldPos;
                    timer = WorldPlaceTimeOut;
                }
            }

            yield return null;
            timer -= Time.deltaTime;
        }

        coroutineActive = false;
    }
}
