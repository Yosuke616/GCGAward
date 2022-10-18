using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSMouseMoveX : MonoBehaviour
{
    private bool b_Charge = false;
    private bool b_AimMode = false;
    [SerializeField]
    private float FPSSensi = 1.0f;
    [SerializeField]
    private float ControllerSensi = 1.0f;
    [SerializeField]
    private float deadZone = 0.5f;
    [SerializeField]
    GameObject TPSTarget;
    [SerializeField]
    private float MouseMoveX = 0.0f;
    private bool controller;
    [SerializeField]
    private float TPSVectorX;
    [SerializeField]
    private float TPSVectorZ;

    private static float mouseX = 0.0f;
    //private float MouseMoveY = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //controller = PlayerInputTest.GetControllerUse();
        controller = true;
        if (controller)
        {
            if ((Input.GetMouseButtonDown(0)))//&& controller || Gamepad.current.rightTrigger.ReadValue()>deadZone )&& !b_AimMode && !controller)    //マウスの左クリックが押された
            {
                b_AimMode = true;
                b_Charge = true;
                MouseMoveX = TPSTarget.transform.eulerAngles.y;
            }
            if (Input.GetMouseButtonUp(0))// && controller || Gamepad.current.rightTrigger.ReadValue() < deadZone && b_AimMode && !controller)  //マウスの左クリックが外れたとき
            {
                if (b_AimMode)
                    b_AimMode = false;
                if (b_Charge)
                    b_Charge = false;

            }
            if (Input.GetMouseButtonDown(1))// && controller || Gamepad.current.leftTrigger.ReadValue() > deadZone) && b_Charge && !controller)    //マウスの右クリックが押された
            {

                b_Charge = false;

            }
            MouseMoveX += Input.GetAxis("Mouse X") * FPSSensi;
        }
        else
        {
            if ((Gamepad.current.rightTrigger.ReadValue()>deadZone )&& !b_AimMode )    //マウスの左クリックが押された
            {
                b_AimMode = true;
                b_Charge = true;
                MouseMoveX = this.transform.eulerAngles.y;
                //MouseMoveX = TPSTarget.transform.eulerAngles.y;
            }
            if ((Gamepad.current.rightTrigger.ReadValue() < deadZone) && b_AimMode )  //マウスの左クリックが外れたとき
            {
                if (b_AimMode)
                    b_AimMode = false;
                if (b_Charge)
                    b_Charge = false;

            }
            if ((Gamepad.current.leftTrigger.ReadValue() > deadZone) && b_Charge )    //マウスの右クリックが押された
            {

                b_Charge = false;

            }

            MouseMoveX += Gamepad.current.rightStick.ReadValue().x * ControllerSensi;
            
        }
        //MouseMoveX = TPSTarget.transform.forward.x;
        //MouseMoveY += Input.GetAxis("Mouse Y") * FPSSensi;
        if (b_Charge)
        {
           
            this.transform.eulerAngles = new Vector3(TPSTarget.transform.eulerAngles.x,MouseMoveX, this.transform.eulerAngles.z);
        }
        //this.transform.eulerAngles = new Vector3(TPSTarget.transform.eulerAngles.x, MouseMoveX, this.transform.eulerAngles.z);

        //if(MouseMoveX>180)
        //{
        //    MouseMoveX -= 360;
        //}
        //if(MouseMoveX<-180)
        //{
        //    MouseMoveX += 360;
        //}
        MouseMoveX = MouseMoveX % 360;
        //MouseMoveX = PlayerViewRotation.GetTPSVectorX();
        TPSVectorX = PlayerViewRotation.GetTPSVectorX();
        TPSVectorZ =PlayerViewRotation.GetTPSVectorZ();
        //PlayerAngleY = this.transform.eulerAngles.y - PlayerViewRotation.GetTPSVectorX();
        mouseX = MouseMoveX/180;
    }
    public static float GetMouseMoveX()
    {
        return mouseX;
    }
}
