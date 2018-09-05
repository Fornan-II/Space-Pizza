using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlDisplay : MonoBehaviour {

    public Controller playerController;
    public Text BodyLeft;
    public Text BodyRight;

    protected string _bodyR = ". . . Boost\n. . . Brake";

    protected string _bodyL_Keyboard = "Spacebar . . .\nLeft shift . . .\n\nSpaceship orients itself to your cursor!";
    protected string _bodyL_Gamepad = "A button . . .\nB button . . .\n\nSpaceship orients itself to either joystick!";
    protected string _bodyL_Touchscreen = "Press somewhere to fly towards it!\n\nPressing on your ship will slow it down.";
    protected string _bodyR_Touchscreen = "";

    protected virtual void OnEnable()
    {
        if(!playerController) { return; }

        if(playerController.GetInputType() == Controller.InputType.MOUSE_AND_KEYBOARD)
        {
            BodyLeft.text = _bodyL_Keyboard;
            BodyRight.text = _bodyR;
        }
        else if(playerController.GetInputType() == Controller.InputType.GAMEPAD)
        {
            BodyLeft.text = _bodyL_Gamepad;
            BodyRight.text = _bodyR;
        }
        else if(playerController.GetInputType() == Controller.InputType.TOUCHSCREEN)
        {
            BodyLeft.text = _bodyL_Touchscreen;
            BodyRight.text = _bodyR_Touchscreen;
        }
    }
}
