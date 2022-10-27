using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameOverMouse : MonoBehaviour, IPointerEnterHandler
{
    private GameOverScript GOS;
    private bool SelectButton = false;
    private float ControllerDeadZone = 0.5f;
    private bool dpadFlg = false;
    // Start is called before the first frame update
    void Start()
    {
        GOS = GameObject.Find("EventSystem").GetComponent<GameOverScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInputTest.GetControllerUse())
        {
            if((Gamepad.current.leftStick.ReadValue().x > ControllerDeadZone || (Gamepad.current.dpad.ReadValue().x > ControllerDeadZone)) && !dpadFlg)
            {
                dpadFlg = true;
                SelectButton = true;
                GOS.SetButton(1);
                GOS.SetButtonAny();
            }
            if ((Gamepad.current.leftStick.ReadValue().x < -ControllerDeadZone || (Gamepad.current.dpad.ReadValue().x < -ControllerDeadZone)) && !dpadFlg)
            {
                dpadFlg = true;
                SelectButton = false;
                GOS.SetButton(0);
                GOS.SetButtonAny();
            }
            if (dpadFlg && ((Gamepad.current.dpad.ReadValue().x == 0) && ((Gamepad.current.leftStick.ReadValue().x < ControllerDeadZone)&&(Gamepad.current.leftStick.ReadValue().x>-ControllerDeadZone))))
            {
                dpadFlg = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject.name == "Retory") {
            GOS.SetButton(0);
            GOS.SetButtonAny();
        } else if (this.gameObject.name == "Title") {
            GOS.SetButton(1);
            GOS.SetButtonAny();
        }
    }
}
