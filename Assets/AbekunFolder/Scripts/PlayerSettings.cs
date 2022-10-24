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
    [SerializeField, Range(0.1f, 0.9f)] private float TPSControllerDeadZone = 0.5f;

    [Header("FPSControllerSetting")]
    [SerializeField, Range(0.1f, 10)] private float FPSControllerSensiX = 1.0f;
    [SerializeField, Range(0.1f,10)] private float FPSControllerSensiY = 1.0f;
    [SerializeField, Range(0.1f, 0.9f)] private float FPSControllerDeadZone = 0.5f;

    [Header("ControllerTriggerSetting")]
    [SerializeField, Range(0.1f, 0.9f)] private float TriggerDeadZone = 0.5f;
   

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Gamepad.current == null)
        {
            Controller = false;
            PlayerRotation.SetControllerUse(Controller);
        }
        else 
        {
            PlayerRotation.SetControllerUse(Controller);
        }
        FPSCameraTarget2.SetFPSSetting(new Vector2(FPSMouseSensiX, FPSMouseSensiY), new Vector2(FPSControllerSensiX, FPSControllerSensiY), FPSControllerDeadZone);
        PlayerInputTest.SetTriggerDeadZone(TriggerDeadZone);
        TPSCameraTargetMove.SetTPSSetting(new Vector2(TPSMouseSensiX, TPSMouseSensiY), new Vector2(TPSControllerSensiX, TPSControllerSensiY), TPSControllerDeadZone);
        
    }
    
}
