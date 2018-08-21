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
        Debug.Log("New planet generating");

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
        GameManager.Self.player.possessedPawn.transform.position = spawnCoord;
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
        GameManager.Self.player.possessedPawn.transform.position = Vector3.zero;
        ProcRoom pr = _seedRoom.GetComponent<ProcRoom>();
        pr.DestroyRoom();
        GameManager.Self.runTimers = true;
    }
}
