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
    [SerializeField] private float RotDif =0;
    [SerializeField] private bool playerMove = false;
    [SerializeField]private int Direct = 0;
    Quaternion Rotate;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerYRot = PlayerInputTest.GetPlayerYRotation();
        if(Input.GetMouseButtonUp(0)||Input.GetMouseButton(0))
        this.transform.eulerAngles = new Vector3(0.0f, FPSTargetMove.transform.eulerAngles.y, 0.0f);
        if(PlayerYRot>181)
        {
            PlayerYRot -= 360;
        }
        if(PlayerYRot<-180)
        {
            PlayerYRot += 360;
        }
        //if (Input.GetKeyUp("s") || Input.GetKeyUp("w") || Input.GetKeyUp("d") || Input.GetKeyUp("a"))
        // playerMove = false;
        //Rotation = 0;
        playerMove = true;
         if (Input.GetKey("a"))
        {
            Direct = 6;
        }
         else if (Input.GetKey("d"))
        {
            Direct = 2;
        }
        else  if(Input.GetKey("s"))
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
        else if (Input.GetKey("w"))
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
        else
        {
            playerMove = false;   
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


        if (playerMove)
        {
            this.transform.eulerAngles += new Vector3(0.0f, (RotDif * 0.1f), 0.0f);
            
        }
        //RotDif = (PlayerYRot + Rotation) * 0.1f;

        //if (playerMove)
        //Rotate = Quaternion.Euler(0.0f, this.transform.eulerAngles.y+RotDif, 0.0f);
        
          //  this.transform.rotation = Rotate*Quaternion.identity;

    }
}
