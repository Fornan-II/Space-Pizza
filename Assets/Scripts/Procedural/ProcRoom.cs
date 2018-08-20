using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcRoom : MonoBehaviour {

    public RoomSpawnNodes[] MyRoomSpawnNodes;
    public List<POISpawn> MyPOISpawns;
    protected bool _hasGenerated = false;

    public virtual List<POISpawn> GenerateRoom(GameObject[] tileSet, Vector4 limits, int potency)
    {
        if(_hasGenerated) { return new List<POISpawn>(); }
        int remainingPotency = potency;
        while(remainingPotency > 0)
        {
            int distributingAmount = Random.Range(0, potency) + 1;
            MyRoomSpawnNodes[Random.Range(0, MyRoomSpawnNodes.Length)].potency += distributingAmount;
            remainingPotency -= distributingAmount;
        }

        List<POISpawn> allPOISpawns = MyPOISpawns;
        foreach(RoomSpawnNodes rsn in MyRoomSpawnNodes)
        {
            if(rsn.potency >= 1)
            {
                allPOISpawns.AddRange(rsn.GenerateNewRoom(tileSet, limits));
                //WaitForFixedUpdate();
            }
        }
        _hasGenerated = true;
        return allPOISpawns;
    }

    public virtual void DestroyRoom()
    {
        for (int i = 0; i < MyPOISpawns.Count; i++)
        {
            if(MyPOISpawns[i])
            {
                MyPOISpawns[i].DestroyPOI();
            }
        }
        for(int i = 0; i < MyRoomSpawnNodes.Length; i++)
        {
            if(MyRoomSpawnNodes[i])
            {
                MyRoomSpawnNodes[i].DestroyNode();
            }
        }
        Destroy(gameObject);
    }
}
