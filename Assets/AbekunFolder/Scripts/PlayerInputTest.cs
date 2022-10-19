/**
 * @file        PlayerInputTest
 * @author      Abe_Kokoro
 * @date        2022/10/5
 * @brief       �v���C���[�̃L�[���͂̃e�X�g
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
public class PlayerInputTest : MonoBehaviour
{
    
    [Header("�v���C���[�X�e�[�^�X")]
    [SerializeField] private float PlayerMove = 1.0f;
    [SerializeField] private float PlayerRot = 1.0f;
    [Header("�o�[�`�����J����")]
    [SerializeField] CinemachineVirtualCamera FPSCamera;    //FPS�J����
    [SerializeField] CinemachineVirtualCamera TPSCamera;    //TPS�J����
  //  [SerializeField] CinemachineVirtualCamera TRASECamera;    //TRASE�J����
    [Header("�R���g���[���[�f�b�h�]�[��")]
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
        controller = true;  //�}�E�X�̂�

        
        Vector3 pos = this.transform.position;
        Quaternion myRotation = this.transform.rotation;
        //myRotation = Quaternion.identity;
        
        if (controller)
        {
            //if(Input.GetKeyDown("W"))
            //PlayerAngleY = this.transform.eulerAngles.y;
             //rotYDif = TPSCamera.transform.eulerAngles.y;

            //memo

            //�v���C��A���O��180�ȏ�Ȃ�}�C�i�X�ɂ��čl�����ق������������H
            //�� 270 -> -90

            //�}�C�i�X�ɂ��čl����ꍇ�J�����̃A���O�����}�C�i�X�ɕϊ�����B

            //��]����Ƃ��A����������čŒZ�̌����ŉ�]������B
            //���̎����ӂ��ׂ��_���A-180��180�����L���Ă��邽�߁A�ŒZ�̌����̒T���ɂ͐�Βl�𗘗p����B

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
            if ((Input.GetMouseButtonDown(0)))//(Gamepad.current.rightTrigger.ReadValue() > deadZone) && !b_AimMode && !controller)    //�}�E�X�̍��N���b�N�������ꂽ
            {
                TPSCamera.Priority = 0;
                FPSCamera.Priority = 100;
                b_Charge = true;
                b_AimMode = true;
            }
            if (Input.GetMouseButtonUp(0))//|| Gamepad.current.rightTrigger.ReadValue() < deadZone && b_AimMode && !controller)  //�}�E�X�̍��N���b�N���O�ꂽ�Ƃ�
            {
                TPSCamera.Priority = 100;
                FPSCamera.Priority = 0;
                if (b_AimMode)
                    b_AimMode = false;
                if (b_Charge)
                    b_Charge = false;
            }
            if ((Input.GetMouseButtonDown(1)))// || Gamepad.current.leftTrigger.ReadValue() > deadZone) && b_Charge && !controller)    //�}�E�X�̉E�N���b�N�������ꂽ
            {
                TPSCamera.Priority = 100;
                FPSCamera.Priority = 0;
                b_Charge = false;
            }
            if ((Gamepad.current.rightTrigger.ReadValue() > deadZone) && !b_AimMode)    //�}�E�X�̍��N���b�N�������ꂽ
            {
                TPSCamera.Priority = 0;
                FPSCamera.Priority = 100;
                b_Charge = true;
                b_AimMode = true;
            }
            if ((Gamepad.current.rightTrigger.ReadValue() < deadZone) && b_AimMode)  //�}�E�X�̍��N���b�N���O�ꂽ�Ƃ�
            {
                TPSCamera.Priority = 100;
                FPSCamera.Priority = 0;
                if (b_AimMode)
                    b_AimMode = false;
                if (b_Charge)
                    b_Charge = false;
            }
            if ((Gamepad.current.buttonEast.ReadValue() > deadZone) && b_Charge)    //�}�E�X�̉E�N���b�N�������ꂽ
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
