using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public Pawn possessedPawn;
    [HideInInspector]public bool usingMouse = true;
    public float android_brakePressRadius = 4.0f;

    protected CameraManager mainCamManager;

    public enum InputType { MOUSE_AND_KEYBOARD, GAMEPAD, TOUCHSCREEN }
    public InputType activeInputType = InputType.MOUSE_AND_KEYBOARD;

	// Use this for initialization
	void Start () {
        if(Application.platform == RuntimePlatform.Android)
        {
            activeInputType = InputType.TOUCHSCREEN;
        }
        mainCamManager = Camera.main.GetComponent<CameraManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if(possessedPawn)
        {
            HandleInput();
            mainCamManager.TrackingPosition = possessedPawn.transform.position;
        }
    }

    protected virtual void HandleInput()
    {
        if (activeInputType == InputType.MOUSE_AND_KEYBOARD)
        {
            possessedPawn.HandleLeftShift(Input.GetButton("Fire1"));
            possessedPawn.HandleSpacebar(Input.GetButton("Fire2"));

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = possessedPawn.transform.position.z;
            possessedPawn.HandleMousePosition(Camera.main.ScreenToWorldPoint(mousePos));

            if (Input.GetAxisRaw("Horizontal") > float.Epsilon || Input.GetAxisRaw("Vertical") > float.Epsilon)
            {
                activeInputType = InputType.GAMEPAD;
            }
        }
        else if (activeInputType == InputType.GAMEPAD)
        {
            possessedPawn.HandleLeftShift(Input.GetButton("Fire1"));
            possessedPawn.HandleSpacebar(Input.GetButton("Fire2"));

            Vector2 locationToPointAt = possessedPawn.transform.position;
            Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), -1f * Input.GetAxisRaw("Vertical"));
            if (inputVector.sqrMagnitude > float.Epsilon)
            {
                possessedPawn.HandleMousePosition(locationToPointAt + GetProperInputVector(inputVector));
            }

            if (Input.GetAxis("Mouse X") > float.Epsilon || Input.GetAxis("Mouse Y") > float.Epsilon)
            {
                activeInputType = InputType.MOUSE_AND_KEYBOARD;
            }
        }
        else if(activeInputType == InputType.TOUCHSCREEN)
        {
            Touch[] prevTouches = Input.touches;

            bool isPressing = false;
            bool isBraking = false;


            if (prevTouches.Length > 0)
            {
                isPressing = true;
            }

            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.z = possessedPawn.transform.position.z;
            possessedPawn.HandleMousePosition(mousePos);

            Debug.Log("Pawn @ " + possessedPawn.transform.position + "\nMouse @ " + mousePos);
            if(isPressing && (Vector3.Distance(possessedPawn.transform.position, mousePos) <= android_brakePressRadius))
            {
                isBraking = true;
                isPressing = false;
            }
            possessedPawn.HandleLeftShift(isBraking);
            possessedPawn.HandleSpacebar(isPressing);
        }
    }

    protected virtual Vector2 GetProperInputVector(Vector2 i)
    {
        Vector2 inputVector = new Vector2(i.x, i.y);
        Vector2 maxedVector = Vector2.one;

        //Find maximum value 
        if (Mathf.Abs(i.x) > Mathf.Abs(i.y))
        {
            maxedVector.Set(1.0f, i.y / i.x);
            if (i.x < 0.0f)
            {
                maxedVector.x = -1.0f;
            }
        }
        else if (Mathf.Abs(i.x) < Mathf.Abs(i.y))
        {
            maxedVector.Set(i.x / i.y, 1.0f);
            if (i.y < 0.0f)
            {
                maxedVector.y = -1.0f;
            }
        }

        inputVector /= maxedVector.magnitude;

        return new Vector2(inputVector.x, inputVector.y);
    }

    public InputType GetInputType()
    {
        return activeInputType;
    }
}
