﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnNodes : MonoBehaviour {

    public int potency = 0;
    protected bool _hasGenerated = false;
    protected GameObject _thisRoom;
    public Transform RoomSpawnLocation;
    protected static float roomCheckRadius = 10;
    public bool HasGenerated { get { return _hasGenerated; } }

    public virtual IEnumerator GenerateNewRoom(GameObject[] TileSet, Vector4 spawnBoundaries, List<POISpawn> poiStorage)
    {
        if (!_hasGenerated)
        {
            //Debug.Log("Hi I'm at " + transform.position + " and I'm doing stuff with a potency of " + potency);
            //Debug.DrawLine(transform.position, RoomSpawnLocation.position, Color.magenta, 1.0f);
            //UnityEditor.EditorApplication.isPaused = true;

            Vector2 worldPos = transform.position;
            if (worldPos.x > spawnBoundaries.x && worldPos.y > spawnBoundaries.y && worldPos.x < spawnBoundaries.z && worldPos.y < spawnBoundaries.w)
            {
                RoomSpawnNodes closestRoomNode = null;
                float closestRoomDistance = 1000;
                ProcRoom pr = null;

                Collider2D[] foundCols = Physics2D.OverlapCircleAll(RoomSpawnLocation.position, roomCheckRadius);
                //Debug.Log("OverlapCircleAll: " + foundCols.Length);
                //Debug.DrawLine((Vector2)RoomSpawnLocation.position + Vector2.left * roomCheckRadius, (Vector2)RoomSpawnLocation.position + Vector2.right * roomCheckRadius, Color.green);
                //Debug.DrawLine((Vector2)RoomSpawnLocation.position + Vector2.up * roomCheckRadius, (Vector2)RoomSpawnLocation.position + Vector2.down * roomCheckRadius, Color.green);
                //UnityEditor.EditorApplication.isPaused = true;
                //yield return null;
                if (foundCols.Length > 0)
                {
                    foreach (Collider2D col in foundCols)
                    {
                        //Debug.DrawLine((Vector2)RoomSpawnLocation.position + Vector2.one, (Vector2)RoomSpawnLocation.position + Vector2.one * -1f, Color.red, 1.0f);
                        //Debug.DrawLine((Vector2)RoomSpawnLocation.position + new Vector2(1, -1), (Vector2)RoomSpawnLocation.position + new Vector2(-1, 1), Color.red, 1.0f);
                        ProcRoom existingPR = col.GetComponentInParent<ProcRoom>();
                        if (existingPR)
                        {
                            pr = existingPR;
                        }
                    }
                }
                else
                {
                    _thisRoom = Instantiate(TileSet[Random.Range(0, TileSet.Length)], RoomSpawnLocation.position, RoomSpawnLocation.rotation);
                    pr = _thisRoom.GetComponent<ProcRoom>();
                }

                foreach (RoomSpawnNodes rsn in pr.MyRoomSpawnNodes)
                {
                    float thisRoomDistance = Vector2.Distance(rsn.transform.position, transform.position);
                    if (thisRoomDistance < closestRoomDistance)
                    {
                        closestRoomNode = rsn;
                        closestRoomDistance = thisRoomDistance;
                    }
                }

                NodeUsedForRoom();
                if (closestRoomNode)
                {
                    closestRoomNode.NodeUsedForRoom();
                }

                if (_thisRoom)
                {
                    yield return pr.GenerateRoom(TileSet, spawnBoundaries, potency - 1, poiStorage);
                }
            }

            _hasGenerated = true;
        }
    }

    public virtual void NodeUsedForRoom()
    {
        Collider2D col = gameObject.GetComponent<Collider2D>();
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        if (col) { col.enabled = false; }
        if (sr) { sr.enabled = false; }
        _hasGenerated = true;
    }

    public virtual void DestroyNode()
    {
        if(_thisRoom)
        {
            ProcRoom pr = _thisRoom.GetComponent<ProcRoom>();
            pr.DestroyRoom();
        }
        Destroy(gameObject);
    }
}
