using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IZ_PlanetZone : InteractZone
{
    public GameObject[] TileSet;
    public float expansivenessMultiplier = 1.3f;
    protected static Vector4 spawnBoundaries = new Vector4(500, 500, 1000, 1500);

    protected GameObject _seedRoom;
    public List<IZ_DeliveryZone> activeDeliveryPOIs = new List<IZ_DeliveryZone>();
    protected bool _isGenerating = false;

    protected override void HasSuccessfullyInteracted()
    {
        base.HasSuccessfullyInteracted();
        //These 3 lines probably want to be in a coroutine in a loading level thing - check Brackeys
        bool pizzaForHere = false;
        foreach(Pizza p in GameManager.Self.ActiveDeliveries)
        {
            if(p.DestinationPlanet == gameObject)
            {
                pizzaForHere = true;
            }
        }

        if(pizzaForHere && !_isGenerating)
        {
            GameManager.Self.runTimers = false;
            StartCoroutine(GenerateLevel((int)(expansivenessMultiplier * GameManager.Self.TotalDeliveries)));
        }
    }

    protected virtual IEnumerator GenerateLevel(int expansiveness)
    {
        //Generation Setup
        GameManager.Self.runTimers = false;
        _isGenerating = true;

        //Generation Proper
        Vector3 spawnCoord = new Vector3(Mathf.Lerp(spawnBoundaries.x, spawnBoundaries.z, 0.5f), Mathf.Lerp(spawnBoundaries.y, spawnBoundaries.w, 0.5f), 0.0f);
        _seedRoom = Instantiate(TileSet[Random.Range(0, TileSet.Length)], spawnCoord, Quaternion.Euler(Vector3.zero));
        ProcRoom pr = _seedRoom.GetComponent<ProcRoom>();
        List<POISpawn> POISpawns = new List<POISpawn>();
        yield return pr.GenerateRoom(TileSet, spawnBoundaries, expansiveness, POISpawns);

        foreach(Pizza p in GameManager.Self.ActiveDeliveries)
        {
            if(p.DestinationPlanet == gameObject)
            {
                POISpawn selectedPOI = POISpawns[Random.Range(0, POISpawns.Count)];
                activeDeliveryPOIs.Add(selectedPOI.GenerateAsDelivery((int)p.NumericID));
                POISpawns.Remove(selectedPOI);
            }
        }

        foreach(POISpawn poi in POISpawns)
        {
            poi.GenerateAsDangerousObject();
        }

        //Post-Generation Actions;
        GameManager.Self.DoWorldLooping = false;
        GameManager.Self.player.possessedPawn.transform.position = new Vector3(spawnCoord.x, spawnCoord.y, GameManager.Self.player.possessedPawn.transform.position.z);
        GameManager.Self.runTimers = true;
        StartCoroutine(WaitForPlayerToFinishLevel());
        _isGenerating = false;
    }

    protected virtual IEnumerator WaitForPlayerToFinishLevel()
    {
        while(activeDeliveryPOIs.Count > 0)
        {
            yield return null;
        }

        GameManager.Self.runTimers = false;
        if(GameManager.Self.player.possessedPawn)
        {
            GameManager.Self.DoWorldLooping = true;
            if(GameManager.Self.worldLooper)
            {
                GameManager.Self.worldLooper.transform.position = Vector3.zero;
            }
            GameManager.Self.player.possessedPawn.transform.position = new Vector3(0f, 0f, GameManager.Self.player.possessedPawn.transform.position.z);
        }
        ProcRoom pr = _seedRoom.GetComponent<ProcRoom>();
        pr.DestroyRoom();
        GameManager.Self.runTimers = true;
    }
}
