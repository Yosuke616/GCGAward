using UnityEngine;
using UnityEngine.InputSystem;

public class TestInputSystem : MonoBehaviour
{
    private GUIStyle style;
    private void Start()
    {
        style = new GUIStyle();
        style.fontSize = 20;

    }
    void Update()
    {
        // ゲームパッドが接続されていないとnullになる。
        if (Gamepad.current == null) return;

        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            Debug.Log("Button Northが押された！");
        }
        if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
        {
            Debug.Log("Button Southが離された！");
        }
    }

    void OnGUI()
    {
        if (Gamepad.current == null) return;

        GUILayout.Label($"leftStick: {Gamepad.current.leftStick.ReadValue()}",style);
        GUILayout.Label($"buttonNorth: {Gamepad.current.buttonNorth.isPressed}", style);
        GUILayout.Label($"buttonSouth: {Gamepad.current.buttonSouth.isPressed}", style);
        GUILayout.Label($"buttonEast: {Gamepad.current.buttonEast.isPressed}", style);
        GUILayout.Label($"buttonWest: {Gamepad.current.buttonWest.isPressed}", style);
        GUILayout.Label($"leftShoulder: {Gamepad.current.leftShoulder.ReadValue()}", style);
        GUILayout.Label($"leftTrigger: {Gamepad.current.leftTrigger.ReadValue()}", style);
        GUILayout.Label($"rightShoulder: {Gamepad.current.rightShoulder.ReadValue()}", style);
        GUILayout.Label($"rightTrigger: {Gamepad.current.rightTrigger.ReadValue()}", style);
    }
}