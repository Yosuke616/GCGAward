using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class FPSCameraTarget2 : MonoBehaviour
{
    [SerializeField] public Transform PlayerTransform;
    [SerializeField] private float FPSCameraDistance;
    [SerializeField] private Vector2 FPSMouseSensi;
    public Vector2 MouseMove = Vector2.zero;
    private Vector3 pos = Vector3.zero;
    private Vector3 nowPos;
    private static float MouseX;
    [Header("コントローラー感度")]
    [SerializeField] private Vector2 LeftstickSensi;
    [Header("コントローラーデッドゾーン")]
    [SerializeField] private float DeadZone;
    [Header("デバッグ用")]
    [SerializeField] private Vector2 Leftstick;
    [SerializeField] private float RoghtStickRot;
    [SerializeField] private Vector2 MouseAxis;
    [SerializeField] private float WheelAxis;
    private bool ChargeFlg = false;
    // Start is called before the first frame update
    void Start()
    {
        MouseMove.y = 0.6f;
        MouseMove.x = 0;
        nowPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MouseAxis.x = Input.GetAxis("Mouse X");
        MouseAxis.y = Input.GetAxis("Mouse Y");
        if (PlayerInputTest.GetChargeMode())
        {
            ChargeFlg = false;
        }
        if (!PlayerRotation.GetControllerUse())
        {
            MouseMove += new Vector2(Input.GetAxis("Mouse X") * FPSMouseSensi.x, Input.GetAxis("Mouse Y")) * Time.deltaTime * FPSMouseSensi.y;

            WheelAxis = Input.GetAxis("Mouse ScrollWheel");
            FPSCameraDistance += WheelAxis;
        }
        else
        {
            Leftstick = Gamepad.current.rightStick.ReadValue();
            if (Mathf.Abs(Leftstick.x) < DeadZone)
            {
                Leftstick.x = 0;
            }
            if (Mathf.Abs(Leftstick.y) < DeadZone)
            {
                Leftstick.y = 0;
            }
            MouseMove -= new Vector2(-Leftstick.x * LeftstickSensi.x * Time.deltaTime, Leftstick.y * LeftstickSensi.y * Time.deltaTime);
            if (Gamepad.current.dpad.ReadValue().y < -DeadZone)
            {
               // FPSCameraDistance -= 0.1f;
            }
            if (Gamepad.current.dpad.ReadValue().y > DeadZone)
            {
                //FPSCameraDistance += 0.1f;
            }

        }
        FPSCameraDistance = Mathf.Clamp(FPSCameraDistance, 1.0f, 5);
        MouseMove.y = Mathf.Clamp(MouseMove.y, -0.4f + 0.5f, 0.4f + 0.5f);
        //MouseMove += new Vector2(Input.GetAxis("Mouse X")*FPSMouseSensi, Input.GetAxis("Mouse Y")*FPSMouseSensi);
        // 球面座標系変換
        pos.x = FPSCameraDistance * Mathf.Sin(MouseMove.y * Mathf.PI) * Mathf.Cos((MouseMove.x+1) * Mathf.PI);
        pos.y = -FPSCameraDistance * Mathf.Cos(MouseMove.y * Mathf.PI);
        pos.z = -FPSCameraDistance * Mathf.Sin(MouseMove.y * Mathf.PI) * Mathf.Sin((MouseMove.x+1) * Mathf.PI);
        //pos *= nowPos.z;

        //pos.y += nowPos.y;
        if (!PlayerInputTest.GetChargeMode() && ChargeFlg == false)
        {
            ChargeFlg = true;
           // MouseMove.x = FPSMouseMoveX.GetMouseMoveX() + 0.5f;
            MouseMove.y = 0.6f;
        }
        MouseX = MouseMove.x - 0.5f;
        // 座標の更新
        transform.position = pos + PlayerTransform.position;
        // transform.LookAt(PlayerTransform.position);

    }
    public static float GetMouseX()
    {
        return MouseX;
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
}
