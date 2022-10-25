using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSettings : MonoBehaviour
{
    [Header("UseDeviceSetting")]
    [SerializeField] protected bool Controller = false;

    [Header("TPSMouseSetting")]
    [SerializeField, Range(0.1f, 10)] private float TPSMouseSensiX = 1.0f;
    [SerializeField, Range(0.1f, 10)] private float TPSMouseSensiY = 1.0f;

    [Header("FPSMouseSetting")]
    [SerializeField, Range(0.1f, 10)] private float FPSMouseSensiX = 1.0f;
    [SerializeField, Range(0.1f,10)] private float FPSMouseSensiY = 1.0f;

    [Header("TPSControllerSetting")]
    [SerializeField, Range(0.1f,10)] private float TPSControllerSensiX = 1.0f;
    [SerializeField, Range(0.1f, 10)] private float TPSControllerSensiY = 1.0f;
    
    [Header("FPSControllerSetting")]
    [SerializeField, Range(0.1f, 10)] private float FPSControllerSensiX = 1.0f;
    [SerializeField, Range(0.1f,10)] private float FPSControllerSensiY = 1.0f;
    
    [Header("ControllerDeadZoneSetting")]
    [SerializeField, Range(0.1f, 0.9f)] private float TriggerDeadZone = 0.5f;
    [SerializeField, Range(0.1f, 0.9f)] private float StickDeadZone = 0.5f;

    public static float LTDeadZoneGetter = 0;

    [SerializeField]private bool CursorLock = true;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        LTDeadZoneGetter = TriggerDeadZone;
        if (Gamepad.current == null)
        {
            Controller = false;
            PlayerRotation.SetControllerUse(Controller);
        }
        else 
        {
            PlayerRotation.SetControllerUse(Controller);
            if(Input.GetKeyDown("c"))
            {
                Controller = !Controller;
            }
        }
        FPSCameraTarget2.SetFPSSetting(new Vector2(FPSMouseSensiX/5, FPSMouseSensiY/5), new Vector2(FPSControllerSensiX/5, FPSControllerSensiY/5), StickDeadZone);
        PlayerInputTest.SetTriggerDeadZone(TriggerDeadZone);
        TPSCameraTargetMove.SetTPSSetting(new Vector2(TPSMouseSensiX/5, TPSMouseSensiY/5), new Vector2(TPSControllerSensiX/5, TPSControllerSensiY/5), StickDeadZone);
        if(Input.GetKeyDown("v"))
        {
            CursorLock = !CursorLock;
        }
        if(!CursorLock)
        { 
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
    }
    
    
}
