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
public class PlayerInputTest : MonoBehaviour
{
    
    [Header("�v���C���[�X�e�[�^�X")]
    [SerializeField] private float PlayerMove = 1.0f;
    [SerializeField] private float PlayerRot = 1.0f;
    [Header("�o�[�`�����J����")]
    [SerializeField] CinemachineVirtualCamera FPSCamera;    //FPS�J����
    [SerializeField] CinemachineVirtualCamera TPSCamera;    //TPS�J����
    [SerializeField] CinemachineVirtualCamera TRASECamera;    //TRASE�J����
    private bool b_Charge = false;
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
        if (Input.GetMouseButtonDown(0))    //�}�E�X�̍��N���b�N�������ꂽ
        {
            TPSCamera.Priority = 0;
            FPSCamera.Priority = 100;
            b_Charge = true;
        }
        if (Input.GetMouseButtonUp(0))  //�}�E�X�̍��N���b�N���O�ꂽ�Ƃ�
        {
            TPSCamera.Priority = 100;
            FPSCamera.Priority = 0;
            b_Charge = false;
        }
        if (Input.GetMouseButtonDown(1)&&b_Charge)    //�}�E�X�̉E�N���b�N�������ꂽ
        {
            TPSCamera.Priority = 100;
            FPSCamera.Priority = 0;
            b_Charge = false;
        }
    }
}
