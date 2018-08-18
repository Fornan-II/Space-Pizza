using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public Pawn possessedPawn;
    [HideInInspector]public bool usingMouse = true;

    protected CameraManager mainCamManager;

	// Use this for initialization
	void Start () {
        mainCamManager = Camera.main.GetComponent<CameraManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if(possessedPawn)
        {
            possessedPawn.HandleHorizontal(Input.GetAxis("Horizontal"));
            possessedPawn.HandleVertical(Input.GetAxis("Vertical"));
            possessedPawn.HandleLeftShift(Input.GetButtonDown("Fire1"));
            possessedPawn.HandleSpacebar(Input.GetButtonDown("Fire2"));

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = possessedPawn.transform.position.z;
            possessedPawn.HandleMousePosition(Camera.main.ScreenToWorldPoint(mousePos));

            mainCamManager.TrackingPosition = possessedPawn.transform.position;
        }
    }
}
