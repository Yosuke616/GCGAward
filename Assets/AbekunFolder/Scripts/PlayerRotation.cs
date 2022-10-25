using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform FPSTargetMove;
    [SerializeField] float PlayerYRot = 0.0f;
    [SerializeField]private float Rotation = 0.0f;
    //[SerializeField] private bool rightTurn = true;
    [SerializeField] private float RotDif = 0;
    [SerializeField] private static bool playerMove = false;
    [SerializeField] private bool PlayerMoveFlg = false;
    [SerializeField] private int Direct = 0;
    Quaternion Rotate;
    [SerializeField] public static bool Controller = false;
    [SerializeField] private static bool ControllerUse;
    [SerializeField] private float ControllerDeadZone = 0.5f;
    [SerializeField] Vector2 ControllerLeftStick;
    [SerializeField] float ControllerLeftStickInput;
    [SerializeField] private float controllerRot;
    private static int RotationPlayer = 0;
    [SerializeField] private bool ChangeDirectFlg = false;
    //private float playerRot = 0;
    [SerializeField] private Vector3 FixedAngle;
    //[SerializeField] Transform FPS;
    void Start()
    {
        if (Gamepad.current == null)
        {
            //ControllerUse = false;
        }
        else
        {
            //ControllerUse = true;
        }
    }
    private void FixedUpdate()
    {
        if (PlayerInputTest.GetChargeMode())
        {
            //playerMove = false;
            this.transform.LookAt(FPSTargetMove);
            if (this.transform.rotation.x > 0)
            {
                  this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            }
        }
        else
        {
            

            if (playerMove)
            {
                this.transform.eulerAngles += FixedAngle;
               }
            else
            {
                this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
                //this.transform.eulerAngles += new Vector3(0.0f, (RotDif * 0.1f), 0.0f);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        ControllerUse = Controller;
        
        PlayerYRot = PlayerInputTest.GetPlayerYRotation();
        if (PlayerInputTest.GetChargeMode())
        {
            playerMove = false;
            //this.transform.LookAt(FPSTargetMove);
            if(this.transform.rotation.x<0)
            {
              //  this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            }
            //this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y,0);
            if (ControllerUse)
            {
                ControllerLeftStick = Gamepad.current.leftStick.ReadValue();
                ControllerLeftStickInput = ControllerLeftStick.magnitude;
                if (ControllerLeftStickInput < ControllerDeadZone)
                {
                    ControllerLeftStick.x = 0;
                    ControllerLeftStick.y = 0;
                    playerMove = false;
                }
                else
                {
                    playerMove = true;
                }
                controllerRot = GetAngle(new Vector2(0.0f, 0.0f), ControllerLeftStick);
                if (playerMove)
                {
                    if (controllerRot < 23)
                    {
                        Direct = 0;
                    }
                    else if (controllerRot < 68)
                    {
                        Direct = 1;
                    }
                    else if (controllerRot < 113)
                    {
                        Direct = 2;
                    }
                    else if (controllerRot < 158)
                    {
                        Direct = 3;
                    }
                    else if (controllerRot < 203)
                    {
                        Direct = 4;
                    }
                    else if (controllerRot < 248)
                    {
                        Direct = 5;
                    }
                    else if (controllerRot < 293)
                    {
                        Direct = 6;
                    }
                    else if (controllerRot < 338)
                    {
                        Direct = 7;
                    }
                    else
                    {
                        Direct = 0;
                    }
                }
            }
            //if (Input.GetKeyUp("s") || Input.GetKeyUp("w") || Input.GetKeyUp("d") || Input.GetKeyUp("a"))
            // playerMove = false;
            //Rotation = 0;
            if (!ControllerUse)
            {

                playerMove = false;
                if (Input.GetKey("a"))
                {
                    Direct = 6;
                    playerMove = true;
                }

                if (Input.GetKey("d"))
                {
                    Direct = 2;
                    playerMove = true;
                }
                if (Input.GetKey("s"))
                {
                    Direct = 4;
                    playerMove = true;
                    if (Input.GetKey("a"))
                    {
                        Direct = 5;
                        playerMove = true;
                        if (Input.GetKey("d"))
                        {
                            Direct = 4;
                        }
                    }
                    else if (Input.GetKey("d"))
                    {
                        Direct = 3;

                    }
                }
                if (Input.GetKey("w"))
                {
                    Direct = 0;
                    playerMove = true;
                    if (Input.GetKey("a"))
                    {
                        Direct = 7;
                        playerMove = true;
                        if (Input.GetKey("d"))
                        {
                            Direct = 0;
                        }
                    }
                    else if (Input.GetKey("d"))
                    {
                        Direct = 1;
                    }

                }

            }
            RotationPlayer = Direct;
        }
        else
        {
            
            if (PlayerYRot > 181)
            {
                PlayerYRot -= 360;
            }
            if (PlayerYRot < -180)
            {
                PlayerYRot += 360;
            }
            if (ControllerUse)
            {
                ControllerLeftStick = Gamepad.current.leftStick.ReadValue();
                ControllerLeftStickInput = ControllerLeftStick.magnitude;
                if (ControllerLeftStickInput < ControllerDeadZone)
                {
                    ControllerLeftStick.x = 0;
                    ControllerLeftStick.y = 0;
                    playerMove = false;
                }
                else
                {
                    playerMove = true;
                }
                controllerRot = GetAngle(new Vector2(0.0f, 0.0f), ControllerLeftStick);
                if (playerMove)
                {
                    if (controllerRot < 23)
                    {
                        Direct = 0;
                    }
                    else if (controllerRot < 68)
                    {
                        Direct = 1;
                    }
                    else if (controllerRot < 113)
                    {
                        Direct = 2;
                    }
                    else if (controllerRot < 158)
                    {
                        Direct = 3;
                    }
                    else if (controllerRot < 203)
                    {
                        Direct = 4;
                    }
                    else if (controllerRot < 248)
                    {
                        Direct = 5;
                    }
                    else if (controllerRot < 293)
                    {
                        Direct = 6;
                    }
                    else if (controllerRot < 338)
                    {
                        Direct = 7;
                    }
                    else
                    {
                        Direct = 0;
                    }
                }
            }
            //if (Input.GetKeyUp("s") || Input.GetKeyUp("w") || Input.GetKeyUp("d") || Input.GetKeyUp("a"))
            // playerMove = false;
            //Rotation = 0;
            if (!ControllerUse)
            {

                playerMove = false;
                if (Input.GetKey("a"))
                {
                    Direct = 6;
                    playerMove = true;
                }

                if (Input.GetKey("d"))
                {
                    Direct = 2;
                    playerMove = true;
                }
                if (Input.GetKey("s"))
                {
                    Direct = 4;
                    playerMove = true;
                    if (Input.GetKey("a"))
                    {
                        Direct = 5;
                        playerMove = true;
                        if (Input.GetKey("d"))
                        {
                            Direct = 4;
                        }
                    }
                    else if (Input.GetKey("d"))
                    {
                        Direct = 3;

                    }
                }
                if (Input.GetKey("w"))
                {
                    Direct = 0;
                    playerMove = true;
                    if (Input.GetKey("a"))
                    {
                        Direct = 7;
                        playerMove = true;
                        if (Input.GetKey("d"))
                        {
                            Direct = 0;
                        }
                    }
                    else if (Input.GetKey("d"))
                    {
                        Direct = 1;
                    }

                }

            }
            if (playerMove)
            {
                PlayerMoveFlg = true;
                switch (Direct)
                {
                    case 0:
                        Rotation = 0;
                        break;
                    case 1:
                        Rotation = 45;
                        break;
                    case 2:
                        Rotation = 90;
                        break;
                    case 3:
                        Rotation = 135;
                        break;
                    case 4:
                        Rotation = 180;
                        break;
                    case 5:
                        Rotation = -135;
                        break;
                    case 6:
                        Rotation = -90;
                        break;
                    case 7:
                        Rotation = -45;
                        break;

                }

                RotDif = PlayerYRot + Rotation;
                if (RotDif < -180)
                {
                    RotDif = RotDif + 360;
                }
                if (RotDif > 180)
                {
                    RotDif = RotDif - 360;
                }

                if(RotDif>10||RotDif<-10)
                {
                    ChangeDirectFlg = true;
                }
                if(PlayerYRot+Rotation == 0|| PlayerYRot + Rotation == 360)
                {
                    ChangeDirectFlg = false;
                }

                if (ChangeDirectFlg)
                {
                    //this.transform.eulerAngles += new Vector3(0.0f, (RotDif * 0.1f), 0.0f);
                    FixedAngle = new Vector3(0.0f, (RotDif * 0.1f), 0.0f);
                }
                else
                {
                    FixedAngle = new Vector3(0.0f, (RotDif), 0.0f);
                }

            }
            else
            {
                PlayerMoveFlg = false;
            }
            //RotDif = (PlayerYRot + Rotation) * 0.1f;

            //if (playerMove)
            //Rotate = Quaternion.Euler(0.0f, this.transform.eulerAngles.y+RotDif, 0.0f);

            //  this.transform.rotation = Rotate*Quaternion.identity;
            
            RotationPlayer = Direct;

        }
    }
    float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.x, dt.y);
        float degree = rad * Mathf.Rad2Deg;

        if (degree < 0)
        {
            degree += 360;
        }

        return degree;
    }
    public static bool GetPlayerMove()
    {
        return playerMove;
    }
    public static bool GetControllerUse()
    {
        return ControllerUse;
    }
    public static int GetPlayerRotation()
    {
        return RotationPlayer;
    }

    //trueのときコントローラー操作
    public static void SetControllerUse(bool controllerUse)
    {
        Controller = controllerUse;
    }
}
