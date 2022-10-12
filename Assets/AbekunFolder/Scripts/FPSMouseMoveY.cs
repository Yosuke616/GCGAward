using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class FPSMouseMoveY : MonoBehaviour
{
    // Start is called before the first frame update
    private bool b_Charge = false;
    private bool b_AimMode = false;
    [SerializeField]
    private float FPSSensi = 1.0f;
    //private float MouseMoveX = 0.0f;
    [SerializeField]
    private float ControllerSensi = 1.0f;
    [SerializeField]
    private float MouseMoveY = 0.0f;
    private float deadZone = 0.5f;
    private bool controller = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        controller = PlayerInputTest.GetControllerUse();


        if (controller)
        {
            if ((Input.GetMouseButtonDown(0)))//&&controller || Gamepad.current.rightTrigger.ReadValue() > deadZone)&&!b_AimMode && !controller)    //マウスの左クリックが押された
            {
                b_AimMode = true;
                b_Charge = true;
            }
            if (Input.GetMouseButtonUp(0))// && controller || Gamepad.current.rightTrigger.ReadValue() < deadZone&&b_AimMode && !controller)  //マウスの左クリックが外れたとき
            {
                if (b_AimMode)
                    b_AimMode = false;
                if (b_Charge)
                    b_Charge = false;
            }
            if ((Input.GetMouseButtonDown(1)))// && controller || Gamepad.current.leftTrigger.ReadValue() > deadZone) && b_Charge && !controller)    //マウスの右クリックが押された
            {

                b_Charge = false;

            }
        }
        else
        {
            
            if ((Gamepad.current.rightTrigger.ReadValue() > deadZone)&&!b_AimMode)    //マウスの左クリックが押された
            {
                b_AimMode = true;
                b_Charge = true;
            }
            if ((Gamepad.current.rightTrigger.ReadValue() < deadZone)&&b_AimMode )  //マウスの左クリックが外れたとき
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
        }
        //MouseMoveX += Input.GetAxis("Mouse X") * FPSSensi;
        
        if (b_Charge)
        {
            if(controller)
            MouseMoveY += Input.GetAxis("Mouse Y") * FPSSensi;
            if(!controller)
            MouseMoveY += Gamepad.current.rightStick.ReadValue().y * ControllerSensi;
            if (MouseMoveY>90)
            {
                MouseMoveY = 90;
            }
            if(MouseMoveY<-90)
            {
                MouseMoveY = -90;
            }
            this.transform.eulerAngles = new Vector3(-MouseMoveY, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        }
        if (!b_Charge)
        {

            if (MouseMoveY < -1)
            {
                MouseMoveY ++;
                this.transform.eulerAngles = new Vector3(-MouseMoveY, this.transform.eulerAngles.y, this.transform.eulerAngles.z);

            }
            else if (MouseMoveY > 1)
            {
                MouseMoveY--;
                this.transform.eulerAngles = new Vector3(-MouseMoveY, this.transform.eulerAngles.y, this.transform.eulerAngles.z);

            }

        }
    }
}
