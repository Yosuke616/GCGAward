using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform FPSTargetMove;
    [SerializeField] float PlayerYRot = 0.0f;
    private float Rotation = 0.0f;
    [SerializeField] private bool rightTurn = true;
    [SerializeField] private float RotDif = 0;
    [SerializeField] private static bool playerMove = false;
    [SerializeField] private int Direct = 0;
    Quaternion Rotate;
    [SerializeField] private static bool ControllerUse;
    [SerializeField] private float ControllerDeadZone = 0.5f;
    [SerializeField] Vector2 ControllerLeftStick;
    [SerializeField] float ControllerLeftStickInput;
    [SerializeField] private float controllerRot;

    void Start()
    {
        if (Gamepad.current == null)
        {
            ControllerUse = false;
        }
        else
        {
            ControllerUse = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerYRot = PlayerInputTest.GetPlayerYRotation();
        if (PlayerInputTest.GetChargeMode())
            this.transform.eulerAngles = new Vector3(0.0f, FPSTargetMove.transform.eulerAngles.y, 0.0f);
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
            playerMove = true;
            if (Input.GetKey("a"))
            {
                Direct = 6;
            }
            if (Input.GetKey("d"))
            {
                Direct = 2;
            }
            if (Input.GetKey("s"))
            {
                Direct = 4;
                if (Input.GetKey("a"))
                {
                    Direct = 5;
                }
                else if (Input.GetKey("d"))
                {
                    Direct = 3;
                }
            }
            if (Input.GetKey("w"))
            {
                Direct = 0;
                if (Input.GetKey("a"))
                {
                    Direct = 7;
                }
                else if (Input.GetKey("d"))
                {
                    Direct = 1;
                }
            }

            if (!Input.anyKey) playerMove = false;
        }
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
            default:
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
        if (playerMove)
        {
            this.transform.eulerAngles += new Vector3(0.0f, (RotDif * 0.1f), 0.0f);

        }
        //RotDif = (PlayerYRot + Rotation) * 0.1f;

        //if (playerMove)
        //Rotate = Quaternion.Euler(0.0f, this.transform.eulerAngles.y+RotDif, 0.0f);

        //  this.transform.rotation = Rotate*Quaternion.identity;

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
}
