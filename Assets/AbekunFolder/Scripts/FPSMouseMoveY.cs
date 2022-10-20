using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class FPSMouseMoveY : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private bool b_Charge = false;
    private bool b_AimMode = false;
    [SerializeField]
    private float FPSSensi = 1.0f;
    //private float MouseMoveX = 0.0f;
    [SerializeField]
    private float ControllerSensi = 1.0f;
    [SerializeField]
    private float MouseMoveY = 0.0f;
    [SerializeField]
    private float deadZone = 0.5f;
    private bool controller = false;
    [SerializeField]
    CinemachineVirtualCamera TPSVirtualCamera;
    [SerializeField]
    private float active = 0.0f;
    [SerializeField]
    GameObject TPSCamera;
    CinemachineBrain brain;
    [SerializeField] float debugBlend = 0;
    private float TPSCameraEulerY = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        brain= CinemachineCore.Instance.FindPotentialTargetBrain(TPSVirtualCamera);
       

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        //controller = PlayerInputTest.GetControllerUse();
        controller = true;
        var blendtime = brain.ActiveBlend;
        debugBlend = blendtime.BlendWeight;
       // TPSCameraEulerY = TPSCamera.transform.rotation.x;

       
            b_Charge = PlayerInputTest.GetChargeMode();
            if ((Input.GetMouseButtonDown(0))&&b_Charge == false || Gamepad.current.rightTrigger.ReadValue() > deadZone&&b_Charge==false)//&&controller || Gamepad.current.rightTrigger.ReadValue() > deadZone)&&!b_AimMode && !controller)    //マウスの左クリックが押された
            {
                b_AimMode = true;
                b_Charge = true;
                //MouseMoveY = TPSCamera.transform.eulerAngles.x;
                MouseMoveY = 0;
                //TPSCameraEulerY = TPSCamera.transform.rotation.x;
                //MouseMoveY = TPSCameraEulerY;
               // this.transform.eulerAngles = new Vector3(MouseMoveY, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        }
            //if (Input.GetMouseButtonUp(0))// && controller || Gamepad.current.rightTrigger.ReadValue() < deadZone&&b_AimMode && !controller)  //マウスの左クリックが外れたとき
            //{
            //    if (b_AimMode)
            //        b_AimMode = false;
            //    if (b_Charge)
            //        b_Charge = false;
            //}
            //if ((Input.GetMouseButtonDown(1)))// && controller || Gamepad.current.leftTrigger.ReadValue() > deadZone) && b_Charge && !controller)    //マウスの右クリックが押された
            //{

            //    b_Charge = false;

            //}
        
        
        //MouseMoveX += Input.GetAxis("Mouse X") * FPSSensi;
       // b_Charge = PlayerInputTest.GetChargeMode();
        if (b_Charge)
        {
            //TPSCameraEulerY = TPSCamera.transform.rotation.x;
            if (!PlayerRotation.GetControllerUse())
            MouseMoveY -= Input.GetAxis("Mouse Y") * FPSSensi*Time.deltaTime;
            if(PlayerRotation.GetControllerUse())
            {
                if(Gamepad.current.rightStick.ReadValue().y>deadZone)
                MouseMoveY -= ControllerSensi;
                if (Gamepad.current.rightStick.ReadValue().y < -deadZone)
                    MouseMoveY += ControllerSensi;

            }
            if (MouseMoveY>90)
            {
                MouseMoveY = 90;
            }
            if(MouseMoveY<-90)
            {
                MouseMoveY = -90;
            }
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x+MouseMoveY, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        }
        //this.transform.eulerAngles = new Vector3(TPSTarget.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        if (!b_Charge)
        {
            // MouseMoveY = 0;

            if (blendtime.BlendWeight > 0.98f)
            {
            //MouseMoveY = 0;
            //this.transform.eulerAngles = new Vector3(-MouseMoveY, this.transform.eulerAngles.y, this.transform.eulerAngles.z);



            if (MouseMoveY < 1)
            {
                MouseMoveY += 0.5f;
                this.transform.eulerAngles = new Vector3(MouseMoveY, this.transform.eulerAngles.y, this.transform.eulerAngles.z);

            }
            else if (MouseMoveY > 1)
            {
                MouseMoveY -= 0.5f;
                this.transform.eulerAngles = new Vector3(MouseMoveY, this.transform.eulerAngles.y, this.transform.eulerAngles.z);


            }
            else
            {
                this.transform.eulerAngles = new Vector3(MouseMoveY, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
                MouseMoveY = 0;

            }
              }
            //MouseMoveY = TPSCamera.transform.eulerAngles.x;
            //this.transform.eulerAngles = new Vector3(MouseMoveY, this.transform.eulerAngles.y, this.transform.eulerAngles.z);

        }
    }
}
