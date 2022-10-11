using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @file        PlayerViewRotation.cs
 * @author      Abe_Kokoro
 * @date        2022/10/6
 * @brief       プレイヤーのTPSカメラの視点を移動させる（マウス）
 */

public class PlayerViewRotation : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [Header("注視点")]
   
    [SerializeField] private float CameraRange = 5.0f;

    [Header("視点移動感度")]
    [SerializeField] private float Sensi = 1.0f;
    [Header("デバッグ用")]
    [SerializeField] Vector3 ObjectPos;
    //[SerializeField]private float pi = 0.0f;
    [SerializeField]
    private float MouseMoveX = 0.0f;
    [SerializeField]
    private float MouseMoveY = 0.0f;
    [SerializeField]
    private float PlayerVectorX = 0.0f;
    [SerializeField]
    private float PlayerVectorZ = 0.0f;
    [SerializeField]
    private float sinX;
    [SerializeField]
    private float cosX;
    //private float si;
    private float cosY;
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
        PlayerVectorX = -Player.transform.forward.x;//X
        PlayerVectorZ = -Player.transform.forward.z;//Z
        
        if (!b_Charge)
        {
            MouseMoveX += Input.GetAxis("Mouse X") * Sensi;
            MouseMoveY += Input.GetAxis("Mouse Y") * Sensi;
            

            sinX = Mathf.Sin(MouseMoveX+Mathf.Atan2(PlayerVectorX, PlayerVectorZ));//X
             cosX = Mathf.Cos(MouseMoveX+ Mathf.Atan2(PlayerVectorX, PlayerVectorZ));//Z
            //at sinY = Mathf.Sin(MouseMoveY);//Z
             cosY = Mathf.Cos(MouseMoveY);//Y
            
            //this.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 10.0f, Player.transform.position.z) + (-Player.transform.forward * CameraRange);

            //if (Input.GetMouseButton(1))
            //{
            //if (PlayerVectorX > sinX)
            //{
            //    sinX -= 0.01f;
            //}
            //if (PlayerVectorX > sinX)
            //{
            //    sinX += 0.01f;
            //}
            //if (PlayerVectorZ < cosX)
            //{
            //    cosX -= 0.01f;
            //}
            //if (PlayerVectorZ > cosX)
            //{
            //    cosX += 0.01f;
            //}
            if ((sinX == PlayerVectorX))
                {
                    
                }
                else {
                    //sinX = sinX * 0.9f + PlayerVectorX * 0.9f;
                }
                if ((cosX == PlayerVectorZ))
                {
                    
                }
                else {
                    //cosX = cosX * 0.9f + PlayerVectorZ * 0.9f;
                }

                
           // }

            this.transform.position = new Vector3((Player.transform.position.x + CameraRange * sinX),
                                                       (Player.transform.position.y + 1.0f + CameraRange * cosY),
                                                       (Player.transform.position.z + CameraRange * cosX));

            //this.transform.position = -Player.transform.forward * (Player.transform.position.z + CameraRange * cosX);
            //this.transform.position = -Player.transform.right * (Player.transform.position.x + CameraRange * sinX);
            //this.transform.position = Player.transform.up * (Player.transform.position.y + 1.0f + CameraRange * cosY);
            //this.transform.position += transform.forward * PlayerMove * Time.deltaTime;

        }
            if (Input.GetMouseButtonDown(0))    //マウスの左クリックが押された
        {
            MouseMoveX = 0;
            MouseMoveY = 0;
            b_Charge = true;
        }
        if (Input.GetMouseButtonUp(0))  //マウスの左クリックが外れたとき
        {
            MouseMoveX = 0;
            MouseMoveY = 0;
            //sinX = PlayerVectorX;
            //cosX = PlayerVectorZ;
            b_Charge = false;
        }
        if (Input.GetMouseButtonDown(1) && b_Charge)    //マウスの右クリックが押された
        {
            MouseMoveX = 0;
            MouseMoveY = 0;
            //sinX = PlayerVectorX;
            //cosX = PlayerVectorZ;
            b_Charge = false;
        }
        
    }
}
