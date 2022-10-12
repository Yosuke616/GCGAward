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
    [SerializeField] CinemachineVirtualCamera TRASECamera;    //TRASE�J����
    [Header("�R���g���[���[�f�b�h�]�[��")]
    [SerializeField] private float deadZone = 0.5f;
    [SerializeField]
    private bool b_Charge = false;
    [SerializeField]
    private bool b_AimMode = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        Quaternion myRotation = this.transform.rotation;
        //myRotation = Quaternion.identity;
        
        if(Gamepad.current.leftStick.ReadValue().x >deadZone)//�E
        {
            this.transform.position += transform.right * PlayerMove * Time.deltaTime;
        }
        if (Gamepad.current.leftStick.ReadValue().x < -deadZone)//��
        {
            this.transform.position -= transform.right * PlayerMove * Time.deltaTime;
        }
        if(Gamepad.current.leftStick.ReadValue().y>deadZone)//�O
        {
            this.transform.position += transform.forward * PlayerMove * Time.deltaTime;
        }
        if (Gamepad.current.leftStick.ReadValue().y < -deadZone)//��
        {
            this.transform.position -= transform.forward * PlayerMove * Time.deltaTime;
        }
        
        
        if (Input.GetKey(KeyCode.W))
        {
            //this.transform.position += new Vector3(0, 0, PlayerMove);
            this.transform.position += transform.forward * PlayerMove*Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //this.transform.position -= new Vector3(PlayerMove, 0, 0);
            this.transform.position -= transform.right * PlayerMove*Time.deltaTime;
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
        
        if (Input.GetMouseButtonDown(0) || Gamepad.current.rightTrigger.ReadValue() > deadZone && !b_AimMode)    //�}�E�X�̍��N���b�N�������ꂽ
        {
            TPSCamera.Priority = 0;
            FPSCamera.Priority = 100;
            b_Charge = true;
            b_AimMode = true;
        }
        if (Input.GetMouseButtonUp(0) || Gamepad.current.rightTrigger.ReadValue() < deadZone && b_AimMode)  //�}�E�X�̍��N���b�N���O�ꂽ�Ƃ�
        {
            TPSCamera.Priority = 100;
            FPSCamera.Priority = 0;
            if(b_AimMode)
            b_AimMode = false;
            if(b_Charge)
            b_Charge = false;
        }
        if (Input.GetMouseButtonDown(1)&&b_Charge || Gamepad.current.leftTrigger.ReadValue() > deadZone && b_Charge)    //�}�E�X�̉E�N���b�N�������ꂽ
        {
            TPSCamera.Priority = 100;
            FPSCamera.Priority = 0;
            b_Charge = false;
        }
    }
}
