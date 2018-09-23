using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcRoom : MonoBehaviour {

    public RoomSpawnNodes[] MyRoomSpawnNodes;
    public List<POISpawn> MyPOISpawns;
    protected bool _hasGenerated = false;
    public bool HasGenerated { get { return _hasGenerated; } }

    public virtual IEnumerator GenerateRoom(GameObject[] tileSet, Vector4 limits, int potency, List<POISpawn> poiStorage)
    {
        if(!_hasGenerated)
        {
            int remainingPotency = potency;
            while (remainingPotency > 0)
            {
                int distributingAmount = Random.Range(0, potency) + 1;
                MyRoomSpawnNodes[Random.Range(0, MyRoomSpawnNodes.Length)].potency += distributingAmount;
                remainingPotency -= distributingAmount;
            }

            poiStorage.AddRange(MyPOISpawns);
            foreach (RoomSpawnNodes rsn in MyRoomSpawnNodes)
            {
                //yield return null;
                if (rsn.potency >= 1)
                {
                    yield return rsn.GenerateNewRoom(tileSet, limits, poiStorage);
                }
            }
            _hasGenerated = true;
        }
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
