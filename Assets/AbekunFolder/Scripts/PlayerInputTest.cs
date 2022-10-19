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
  //  [SerializeField] CinemachineVirtualCamera TRASECamera;    //TRASEカメラ
    [Header("コントローラーデッドゾーン")]
    [SerializeField] private float deadZone = 0.5f;
    [SerializeField]
    private static bool b_Charge = false;
    [SerializeField]
    private static bool b_AimMode = false;
    [SerializeField]
    private bool b_Controller = false;
    private static bool controller;
    [SerializeField]
    GameObject TPSTarget;
    [SerializeField]
    private float PlayerAngleY = 0.0f;
    Quaternion rotate;
    [SerializeField]
    private float rotYDif;
    [SerializeField]
    private float playerEulerY;
    [SerializeField]
    private float TPSCameraEulerY;
    [SerializeField]
    private float PlayerMoveRot = 0;
    [SerializeField]
    private bool PlayerMoveFlg = false;
    [SerializeField]private bool b_Left = false;
    [SerializeField]private bool b_Right = true;
    [SerializeField] private int PlayerDirect = 0;
    private static float PlayerRotY = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current == null) b_Controller = true;
        controller = true;  //マウスのみ

        
        Vector3 pos = this.transform.position;
        Quaternion myRotation = this.transform.rotation;
        //myRotation = Quaternion.identity;
        
        if (controller)
        {
            //if(Input.GetKeyDown("W"))
            //PlayerAngleY = this.transform.eulerAngles.y;
             //rotYDif = TPSCamera.transform.eulerAngles.y;

            //memo

            //プレイやアングル180以上ならマイナスにして考えたほうがいいかも？
            //例 270 -> -90

            //マイナスにして考える場合カメラのアングルもマイナスに変換する。

            //回転するとき、差分を取って最短の向きで回転させる。
            //この時注意すべき点が、-180と180を共有しているため、最短の向きの探索には絶対値を利用する。

            TPSCameraEulerY = TPSCamera.transform.eulerAngles.y;
            if (playerEulerY - TPSCameraEulerY > 180)
            {
               // playerEulerY -= 360;

            }
            if (playerEulerY - TPSCameraEulerY < -180)
            {
                //playerEulerY += 360;
            }

            if(playerEulerY<TPSCameraEulerY)
            {
                rotYDif = -(this.transform.eulerAngles.y - TPSCamera.transform.eulerAngles.y);

            }
            else
            {
                rotYDif = -(this.transform.eulerAngles.y - (TPSCamera.transform.eulerAngles.y-360));
            }
            if(rotYDif>360)
            {
                rotYDif -= 360;
            }
            if (rotYDif < -360)
            {
                rotYDif += 360;
            }
            if(rotYDif<-180)
            {
                rotYDif = 360 - rotYDif;
            }
            if (rotYDif > 180)
            {
                rotYDif = 360-playerEulerY+TPSCameraEulerY;
            }
            if (rotYDif >360)
            {
                rotYDif = -(playerEulerY+360 - TPSCameraEulerY);
            }
            if (rotYDif < 1 && rotYDif > -1)
                rotYDif = 0;
            if (Input.GetKey(KeyCode.W))
            {
                PlayerMoveFlg = true;
                //PlayerAngleY = PlayerAngleY * 0.9f + TPSCamera.transform.eulerAngles.y * 0.1f;
                //this.transform.position += new Vector3(0, 0, PlayerMove);
            //    this.transform.position += transform.forward * PlayerMove * Time.deltaTime;
                //rotYDif = rotYDif*0.9f+TPSCamera.transform.eulerAngles.y*0.1f;
                //if(0<rotYDif)
                //PlayerMoveRot = PlayerMoveRot * 0.9f + (0) * 0.1f;
                playerEulerY = this.transform.eulerAngles.y;
                //TPSCameraEulerY = TPSCamera.transform.eulerAngles.y;
                if (playerEulerY - TPSCameraEulerY > 180)
                {
                    // playerEulerY -= 360;
                    //rotate = Quaternion.Euler(0.0f, -360, 0.0f);
                    //this.transform.rotation = rotate * Quaternion.identity;
                }
                if (playerEulerY - TPSCameraEulerY < -180)
                {
                   // playerEulerY += 360;
                    //rotate = Quaternion.Euler(0.0f, 360, 0.0f);
                    //this.transform.rotation = rotate * Quaternion.identity;
                }
                  //  rotate = Quaternion.Euler(0.0f, (playerEulerY * 0.9f + (playerEulerY+rotYDif) * 0.1f), 0.0f);
                

                    //this.transform.rotation = rotate * Quaternion.identity;
               // this.transform.rotation = (Quaternion.Euler(0.0f, PlayerMoveRot, 0.0f)) * Quaternion.identity;

            }
           
            if (Input.GetKey(KeyCode.D))
            {
                b_Right = true;
                b_Left = false;
                if (this.transform.eulerAngles.y < -91)
                {
                    //this.transform.eulerAngles += new Vector3(0, 360, 0);
                }
                //this.transform.position -= new Vector3(PlayerMove, 0, 0);
                //this.transform.position -= transform.right * PlayerMove * Time.deltaTime;
                PlayerMoveFlg = true;
                playerEulerY = this.transform.eulerAngles.y;
               
                //TPSCameraEulerY = TPSCamera.transform.eulerAngles.y;
                //PlayerMoveRot = PlayerMoveRot * 0.9f + (90) * 0.1f;
                if (playerEulerY - TPSCameraEulerY > 180)
                {
                    //playerEulerY -= 360;

                }
                if (playerEulerY - TPSCameraEulerY < -180)
                {
                   // playerEulerY += 360;
                }
                
             //   this.transform.position += transform.forward * PlayerMove * Time.deltaTime;

               
                    //rotate = Quaternion.Euler(0.0f, (playerEulerY * 0.9f + (playerEulerY + rotYDif + 90) * 0.1f), 0.0f);
                    //this.transform.rotation = rotate * Quaternion.identity;
               

                //this.transform.rotation = (Quaternion.Euler(0.0f,PlayerMoveRot,0.0f)) * Quaternion.identity;


            }
            
            
            if (Input.GetKey(KeyCode.A))
            {
               
                b_Left = true;
                b_Right = false;
                
                PlayerMoveFlg = true;
                playerEulerY = this.transform.eulerAngles.y;
                //TPSCameraEulerY = TPSCamera.transform.eulerAngles.y;
                //PlayerMoveRot = PlayerMoveRot * 0.9f + (-90) * 0.1f;
                if (playerEulerY - TPSCameraEulerY > 180)
                {
                   // playerEulerY -= 360;

                }
                if (playerEulerY - TPSCameraEulerY < -180)
                {
                   // playerEulerY += 360;
                }
                
               // this.transform.position += transform.forward * PlayerMove * Time.deltaTime;
                //if(playerEulerY<TPSCameraEulerY)
                //if (rotYDif > -1)
                //{
//                rotYDif = rotYDif * 0.9f + -90 * 0.1f;
//                rotate = Quaternion.Euler(0.0f, (playerEulerY * 0.9f + (playerEulerY + rotYDif) * 0.1f) - 90 * 0.1f, 0.0f);
                //rotate = Quaternion.Euler(0.0f,  TPSCameraEulerY -90, 0.0f);

                //this.transform.rotation = rotate * Quaternion.identity;
                //}
                //else if(rotYDif<1)
                //{
                //    rotate = Quaternion.Euler(0.0f, (playerEulerY * 0.9f + (playerEulerY + rotYDif) * 0.1f) , 0.0f);

                //    this.transform.rotation = rotate * Quaternion.identity;
                //}
                //else
                //{
                //    rotate = Quaternion.Euler(0.0f, -3, 0.0f);

                //    this.transform.rotation = rotate * Quaternion.identity;
                //}
                //if (rotYDif > 91 || rotYDif < -90)
                //{
                //    this.transform.eulerAngles += new Vector3(0, PlayerRot, 0);

                //}
                //else if (rotYDif<89 || rotYDif>-89)
                //{
                //    this.transform.eulerAngles -= new Vector3(0, PlayerRot, 0);
                //}
                

            }
            if (Input.GetKey(KeyCode.S))
            {
                PlayerMoveFlg = true;
                playerEulerY = this.transform.eulerAngles.y;
                //TPSCameraEulerY = TPSCamera.transform.eulerAngles.y;
                //PlayerMoveRot = PlayerMoveRot * 0.9f + (90) * 0.1f;
                
                //this.transform.position += transform.forward * PlayerMove * Time.deltaTime;
                //if (b_Left)
                //rotate = Quaternion.Euler(0.0f, (playerEulerY * 0.9f + TPSCameraEulerY * 0.1f) + (-180) * 0.1f, 0.0f);

                //{
                //    if (playerEulerY > 90)
                //    {
                //        //playerEulerY -= 360;
                //        //rotate = Quaternion.Euler(0.0f, -360, 0.0f);
                //        //this.transform.rotation = rotate * Quaternion.identity;
                //    }

                //}
                // if (b_Right)
                //rotYDif = rotYDif + 180;
                //rotate = Quaternion.Euler(0.0f, (playerEulerY * 0.9f + (playerEulerY + rotYDif) * 0.1f) - 90 * 0.1f, 0.0f);
                //rotate = Quaternion.Euler(0.0f, TPSCameraEulerY+180, 0.0f);

                //{
                //    if (playerEulerY < -90)
                //    {
                //        //playerEulerY += 360;
                //        //rotate = Quaternion.Euler(0.0f, 360, 0.0f);
                //        //this.transform.rotation = rotate * Quaternion.identity;
                //    }
                //    rotate = Quaternion.Euler(0.0f, (playerEulerY * 0.9f + TPSCameraEulerY * 0.1f) + (180) * 0.1f, 0.0f);
                //}

               // this.transform.rotation = rotate * Quaternion.identity;
            }

            //if (Input.GetKey(KeyCode.Q))
            //{
            //    this.transform.eulerAngles -= new Vector3(0, PlayerRot, 0);
            //}
            //if (Input.GetKey(KeyCode.E))
            //{
            //    this.transform.eulerAngles += new Vector3(0, PlayerRot, 0);
            //}
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
            if ((Gamepad.current.rightTrigger.ReadValue() > deadZone) && !b_AimMode)    //マウスの左クリックが押された
            {
                TPSCamera.Priority = 0;
                FPSCamera.Priority = 100;
                b_Charge = true;
                b_AimMode = true;
            }
            if ((Gamepad.current.rightTrigger.ReadValue() < deadZone) && b_AimMode)  //マウスの左クリックが外れたとき
            {
                TPSCamera.Priority = 100;
                FPSCamera.Priority = 0;
                if (b_AimMode)
                    b_AimMode = false;
                if (b_Charge)
                    b_Charge = false;
            }
            if ((Gamepad.current.buttonEast.ReadValue() > deadZone) && b_Charge)    //マウスの右クリックが押された
            {
                TPSCamera.Priority = 100;
                FPSCamera.Priority = 0;
                //b_Charge = false;
                b_AimMode = true;
            }
        }
        else
        {
             
        }
        PlayerRotY = rotYDif;

    }
    public static bool GetControllerUse()
    {
        return controller;
    }
    public static float GetPlayerYRotation()
    {
        return PlayerRotY;
    }
    public static bool GetAimMode()
    {
        return b_AimMode;
    }
    public static bool GetChargeMode()
    {
        return b_Charge;
    }
}
