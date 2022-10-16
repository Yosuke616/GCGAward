using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    [Header("コントロール視点移動感度")]
    [SerializeField] private float ControllSensi = 1.0f;
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
    [SerializeField]
    private float DeadZone = 0.5f;
    //private float si;
    private float cosY;
    [SerializeField]private bool b_Charge = false; 
    [SerializeField]private bool b_AimMode = false;
    [SerializeField]
    private float cameraHeight = 0.0f;
    private bool controller = true ;
    private static float TPSVectorX;
    private static float TPSVectorZ;
    // Start is called before the first frame update
    void Start()
    {
        //Vector3 Pos = new Vector3(0.0f, 0.0f, CameraRange);
        //this.transform.position = Pos;
        PlayerVectorX = -Player.transform.forward.x;//X
        PlayerVectorZ = -Player.transform.forward.z;//Z
    }

    // Update is called once per frame
    void Update()
    {
        
        //controller = PlayerInputTest.GetControllerUse();
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
        if (MouseMoveY < 0)
        {
            MouseMoveY = 0.01f;
        }
        if (MouseMoveY >= 3.14f)
        {
            MouseMoveY = 3.13f;
        }
        //if(MouseMoveX<0.0f)
        //{
        //    MouseMoveX = 6.27f;
        //}
        //if(MouseMoveX>6.28f)
        //{
        //    MouseMoveX = 0.0f;
        //}
        MouseMoveX = MouseMoveX % 6.28f;
        if (!b_Charge)
        {
            if (controller)
            {
                MouseMoveX += Input.GetAxis("Mouse X") * Sensi;
                MouseMoveY += Input.GetAxis("Mouse Y") * Sensi;
            }
            else
            {
                //MouseMoveX += Gamepad.current.rightStick.ReadValue().x * ControllSensi;
               // MouseMoveY += (Gamepad.current.rightStick.ReadValue().y * ControllSensi) / 2;
            }
           
                sinX = Mathf.Sin(MouseMoveX + Mathf.Atan2(PlayerVectorX, PlayerVectorZ));//X
                cosX = Mathf.Cos(MouseMoveX + Mathf.Atan2(PlayerVectorX, PlayerVectorZ));//Z
            
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
           
                
           // }

            this.transform.position = new Vector3((Player.transform.position.x + CameraRange * sinX),
                                                       (Player.transform.position.y  + CameraRange * cosY+cameraHeight),
                                                       (Player.transform.position.z + CameraRange * cosX));

            //this.transform.position = -Player.transform.forward * (Player.transform.position.z + CameraRange * cosX);
            //this.transform.position = -Player.transform.right * (Player.transform.position.x + CameraRange * sinX);
            //this.transform.position = Player.transform.up * (Player.transform.position.y + 1.0f + CameraRange * cosY);
            //this.transform.position += transform.forward * PlayerMove * Time.deltaTime;

        }
        if(b_Charge)
        {
            PlayerVectorX = -Player.transform.forward.x;//X
            PlayerVectorZ = -Player.transform.forward.z;//Z
        }
        if (controller)
        {


            if ((Input.GetMouseButtonDown(0)))// && controller || Gamepad.current.rightTrigger.ReadValue() > DeadZone)&&!b_AimMode && !controller)    //マウスの左クリックが押された
            {
                MouseMoveX = 0;
                MouseMoveY = 0.85f;
                b_Charge = true;
                b_AimMode = true;
            }
            if (Input.GetMouseButtonUp(0))//&& controller || Gamepad.current.rightTrigger.ReadValue() < DeadZone&&b_AimMode && !controller)  //マウスの左クリックが外れたとき
            {

                //sinX = PlayerVectorX;
                //cosX = PlayerVectorZ;
                if (b_AimMode)
                    b_AimMode = false;
                if (b_Charge)
                {
                    b_Charge = false;
                    MouseMoveX = 0;
                    MouseMoveY = 0.85f;
                }
            }
            if ((Input.GetMouseButtonDown(1)))// && controller || Gamepad.current.leftTrigger.ReadValue() > DeadZone) && b_Charge && !controller)    //マウスの右クリックが押された
            {
                MouseMoveX = 0;
                MouseMoveY = 0.85f;
                //sinX = PlayerVectorX;
                //cosX = PlayerVectorZ;
                b_Charge = false;
            }

        }
        else
        {
            if ((Gamepad.current.rightTrigger.ReadValue() > DeadZone)&&!b_AimMode)    //マウスの左クリックが押された
            {
                MouseMoveX = 0;
                MouseMoveY = 0.85f;
                b_Charge = true;
                b_AimMode = true;
            }
            if (( Gamepad.current.rightTrigger.ReadValue() < DeadZone)&&b_AimMode)  //マウスの左クリックが外れたとき
            {

                //sinX = PlayerVectorX;
                //cosX = PlayerVectorZ;
                if (b_AimMode)
                    b_AimMode = false;
                if (b_Charge)
                {
                    b_Charge = false;
                    MouseMoveX = 0;
                    MouseMoveY = 0.85f;
                }
            }
            if ((Gamepad.current.leftTrigger.ReadValue() > DeadZone) && b_Charge)    //マウスの右クリックが押された
            {
                MouseMoveX = 0;
                MouseMoveY = 0.85f;
                //sinX = PlayerVectorX;
                //cosX = PlayerVectorZ;
                b_Charge = false;
            }
        }
        TPSVectorX = sinX;
        TPSVectorZ = cosX;
    }
    public static float GetTPSVectorX()
    {
        return TPSVectorX;
    }
    public static float GetTPSVectorZ()
    {
        return TPSVectorZ;
    }
}
