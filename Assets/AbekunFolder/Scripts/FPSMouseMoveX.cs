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
    private float MouseMoveX = 0.0f;
    //private float MouseMoveY = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

        

        if (Input.GetMouseButtonDown(0)|| Gamepad.current.rightTrigger.ReadValue()>deadZone && !b_AimMode)    //マウスの左クリックが押された
        {
            b_AimMode = true;
            b_Charge = true;
            MouseMoveX = this.transform.eulerAngles.y;
        }
        if (Input.GetMouseButtonUp(0)|| Gamepad.current.rightTrigger.ReadValue() < deadZone && b_AimMode)  //マウスの左クリックが外れたとき
        {
            if (b_AimMode)
                b_AimMode = false;
            if (b_Charge)
                b_Charge = false;

        }
        if (Input.GetMouseButtonDown(1) && b_Charge || Gamepad.current.leftTrigger.ReadValue() > deadZone && b_Charge)    //マウスの右クリックが押された
        {
            
            b_Charge = false;
            
        }
        MouseMoveX += Input.GetAxis("Mouse X") * FPSSensi;
        MouseMoveX += Gamepad.current.rightStick.ReadValue().x*ControllerSensi;
        //MouseMoveY += Input.GetAxis("Mouse Y") * FPSSensi;
        if (b_Charge)
        {
            this.transform.eulerAngles = new Vector3( this.transform.eulerAngles.x,MouseMoveX, this.transform.eulerAngles.z);
        }
       
    }
}
