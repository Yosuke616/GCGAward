/**
 * @file        PlayerInputTest
 * @author      Abe_Kokoro
 * @date        2022/10/5
 * @brief       プレイヤーのキー入力のテスト
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
public class PlayerInputTest : MonoBehaviour
{
    
    [Header("プレイヤーステータス")]
    [SerializeField] private float PlayerMove = 1.0f;
    [SerializeField] private float PlayerRot = 1.0f;
    [Header("バーチャルカメラ")]
    [SerializeField] CinemachineVirtualCamera FPSCamera;    //FPSカメラ
    [SerializeField] CinemachineVirtualCamera TPSCamera;    //TPSカメラ
    [SerializeField] CinemachineVirtualCamera TRASECamera;    //TRASEカメラ
    [Header("コントローラーデッドゾーン")]
    [SerializeField] private float deadZone = 0.5f;
    [SerializeField]
    private bool b_Charge = false;
    [SerializeField]
    private bool b_AimMode = false;
    [SerializeField]
    private bool b_Controller = false;
    private static bool controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current == null) b_Controller = true;
        controller = b_Controller;
        
        Vector3 pos = this.transform.position;
        Quaternion myRotation = this.transform.rotation;
        //myRotation = Quaternion.identity;
        if (!controller)
        {
            if (Gamepad.current.leftStick.ReadValue().x > deadZone)//右
            {
                this.transform.position += transform.right * PlayerMove * Time.deltaTime;
            }
            if (Gamepad.current.leftStick.ReadValue().x < -deadZone)//左
            {
                this.transform.position -= transform.right * PlayerMove * Time.deltaTime;
            }
            if (Gamepad.current.leftStick.ReadValue().y > deadZone)//前
            {
                this.transform.position += transform.forward * PlayerMove * Time.deltaTime;
            }
            if (Gamepad.current.leftStick.ReadValue().y < -deadZone)//後
            {
                this.transform.position -= transform.forward * PlayerMove * Time.deltaTime;
            }
        }
        if (controller)
        {
            if (Input.GetKey(KeyCode.W))
            {
                //this.transform.position += new Vector3(0, 0, PlayerMove);
                this.transform.position += transform.forward * PlayerMove * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                //this.transform.position -= new Vector3(PlayerMove, 0, 0);
                this.transform.position -= transform.right * PlayerMove * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                //this.transform.position -= new Vector3(0, 0, PlayerMove);
                this.transform.position -= transform.forward * PlayerMove * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                //this.transform.position += new Vector3(PlayerMove, 0, 0);

                this.transform.position += transform.right * PlayerMove * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                this.transform.eulerAngles -= new Vector3(0, PlayerRot, 0);
            }
            if (Input.GetKey(KeyCode.E))
            {
                this.transform.eulerAngles += new Vector3(0, PlayerRot, 0);
            }
        }
        if (controller)
        {
            if ((Input.GetMouseButtonDown(0)))//(Gamepad.current.rightTrigger.ReadValue() > deadZone) && !b_AimMode && !controller)    //マウスの左クリックが押された
            {
                TPSCamera.Priority = 0;
                FPSCamera.Priority = 100;
                b_Charge = true;
                b_AimMode = true;
            }
            if (Input.GetMouseButtonUp(0))//|| Gamepad.current.rightTrigger.ReadValue() < deadZone && b_AimMode && !controller)  //マウスの左クリックが外れたとき
            {
                TPSCamera.Priority = 100;
                FPSCamera.Priority = 0;
                if (b_AimMode)
                    b_AimMode = false;
                if (b_Charge)
                    b_Charge = false;
            }
            if ((Input.GetMouseButtonDown(1)))// || Gamepad.current.leftTrigger.ReadValue() > deadZone) && b_Charge && !controller)    //マウスの右クリックが押された
            {
                TPSCamera.Priority = 100;
                FPSCamera.Priority = 0;
                b_Charge = false;
            }
        }
        else
        {
             if ((Gamepad.current.rightTrigger.ReadValue() > deadZone) && !b_AimMode  )    //マウスの左クリックが押された
             {
                 TPSCamera.Priority = 0;
                 FPSCamera.Priority = 100;
                 b_Charge = true;
                 b_AimMode = true;
             }
             if ((Gamepad.current.rightTrigger.ReadValue() < deadZone) && b_AimMode )  //マウスの左クリックが外れたとき
             {
                 TPSCamera.Priority = 100;
                 FPSCamera.Priority = 0;
                 if (b_AimMode)
                     b_AimMode = false;
                 if (b_Charge)
                     b_Charge = false;
             }
             if ((Gamepad.current.leftTrigger.ReadValue() > deadZone) && b_Charge )    //マウスの右クリックが押された
             {
                 TPSCamera.Priority = 100;
                 FPSCamera.Priority = 0;
                 b_Charge = false;
             }
        }

    }
    public static bool GetControllerUse()
    {
        return controller;
    }
}
