using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @file        PlayerViewRotation.cs
 * @author      Abe_Kokoro
 * @date        2022/10/6
 * @brief       �v���C���[��TPS�J�����̎��_���ړ�������i�}�E�X�j
 */

public class PlayerViewRotation : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [Header("�����_")]
   
    [SerializeField] private float CameraRange = 5.0f;

    [Header("���_�ړ����x")]
    [SerializeField] private float Sensi = 1.0f;
    [Header("�f�o�b�O�p")]
    [SerializeField] Vector3 ObjectPos;
    //[SerializeField]private float pi = 0.0f;
    [SerializeField]
    private float MouseMoveX = 0.0f;
    [SerializeField]
    private float MouseMoveY = 0.0f;
    private bool b_Charge = false; 
    // Start is called before the first frame update
    void Start()
    {
        //Vector3 Pos = new Vector3(0.0f, 0.0f, CameraRange);
        //this.transform.position = Pos;
    }

    // Update is called once per frame
    void Update()
    {
        ObjectPos = this.transform.position;
        //pi+=0.01f;
        //if(pi>=6.28)
        //{
        //    pi = 0.0f;
        //}
        //if(pi<=0.0f)
        //{
        //    pi = 6.27f;
        //}

        if (MouseMoveY < 0.1f)
        {
            MouseMoveY = 0.1f;
        }
        if (MouseMoveY >= 3.14f)
        {
            MouseMoveY = 3.13f;
        }
        if(MouseMoveX<0.0f)
        {
            MouseMoveX = 6.27f;
        }
        if(MouseMoveX>6.28f)
        {
            MouseMoveX = 0.0f;
        }
        if (!b_Charge)
        {
            MouseMoveX += Input.GetAxis("Mouse X") * Sensi;
            MouseMoveY += Input.GetAxis("Mouse Y") * Sensi;
            float sinX = Mathf.Sin(MouseMoveX);//X
            float cosX = Mathf.Cos(MouseMoveX);//Z
            float sinY = Mathf.Sin(MouseMoveY);//Z
            float cosY = Mathf.Cos(MouseMoveY);//Y
            
            //this.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1.0f, Player.transform.position.z) + (-Player.transform.forward * CameraRange);



                this.transform.position = new Vector3((Player.transform.position.x + CameraRange * sinX),
                                                        (Player.transform.position.y + 1.0f + CameraRange * cosY),
                                                        (Player.transform.position.z + CameraRange * cosX));

            //this.transform.position = -Player.transform.forward * (Player.transform.position.z + CameraRange * cosX);
            //this.transform.position = -Player.transform.right * (Player.transform.position.x + CameraRange * sinX);
            //this.transform.position = Player.transform.up * (Player.transform.position.y + 1.0f + CameraRange * cosY);
            //this.transform.position += transform.forward * PlayerMove * Time.deltaTime;

        }
            if (Input.GetMouseButtonDown(0))    //�}�E�X�̍��N���b�N�������ꂽ
        {

            b_Charge = true;
        }
        if (Input.GetMouseButtonUp(0))  //�}�E�X�̍��N���b�N���O�ꂽ�Ƃ�
        {

            b_Charge = false;
        }
        if (Input.GetMouseButtonDown(1) && b_Charge)    //�}�E�X�̉E�N���b�N�������ꂽ
        {

            b_Charge = false;
        }
        
    }
}
