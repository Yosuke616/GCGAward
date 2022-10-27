using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class FPSCameraTarget2 : MonoBehaviour
{
    [SerializeField] public Transform PlayerTransform;
    [SerializeField] private float FPSCameraDistance;
    [SerializeField] public static Vector2 FPSMouseSensi;
    public static Vector2 MouseMove = Vector2.zero;
    private Vector3 pos = Vector3.zero;
    private Vector3 nowPos;
    private static float MouseX;
    [Header("コントローラー感度")]
    [SerializeField] public  static Vector2 LeftstickSensi;
    [Header("コントローラーデッドゾーン")]
    [SerializeField] public static float DeadZone;
    [Header("デバッグ用")]
    [SerializeField] private Vector2 Leftstick;
    [SerializeField] private float RoghtStickRot;
    [SerializeField] private Vector2 MouseAxis;
    [SerializeField] private float WheelAxis;
    private bool ChargeFlg = false;
    private Vector3 MovePos;
    // Start is called before the first frame update
    void Start()
    {
        MouseMove.y = 0.6f;
        MouseMove.x = 0;
        nowPos = transform.position;
    }
    private void FixedUpdate()
    {
        //transform.LookAt(PlayerTransform);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        if (!(PlayerTransform == null))
        {
            transform.LookAt(PlayerTransform.position);
            transform.position = pos + new Vector3(PlayerTransform.position.x, PlayerTransform.position.y - 1, PlayerTransform.position.z);
        }

        MouseAxis.x = Input.GetAxis("Mouse X");
        MouseAxis.y = Input.GetAxis("Mouse Y");
        if (PlayerInputTest.GetChargeMode()&&ChargeFlg==true)
        {
            MouseMove.x = TPSCameraTargetMove.MouseMove.x ;
            if(TPSCameraTargetMove.MouseMove.y<0.6)
            {
                //MouseMove.y = 0.6f-TPSCameraTargetMove.MouseMove.y;

            }
            MouseMove.y = 1.2f-TPSCameraTargetMove.MouseMove.y;
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
            MouseMove += new Vector2(Leftstick.x * LeftstickSensi.x * Time.deltaTime, Leftstick.y * LeftstickSensi.y * Time.deltaTime);
           

        }
        FPSCameraDistance = Mathf.Clamp(FPSCameraDistance, 1.0f, 5);
        MouseMove.y = Mathf.Clamp(MouseMove.y, -0.15f + 0.5f, 0.3f + 0.5f);
        //MouseMove += new Vector2(Input.GetAxis("Mouse X")*FPSMouseSensi, Input.GetAxis("Mouse Y")*FPSMouseSensi);
        // 球面座標系変換
        pos.x = FPSCameraDistance * Mathf.Sin(MouseMove.y * Mathf.PI) * Mathf.Cos((MouseMove.x+1.0f) * Mathf.PI);
        pos.y = -FPSCameraDistance * Mathf.Cos(MouseMove.y * Mathf.PI);
        pos.z = -FPSCameraDistance * Mathf.Sin(MouseMove.y * Mathf.PI) * Mathf.Sin((MouseMove.x+1.0f) * Mathf.PI);
        //pos *= nowPos.z;

        //pos.y += nowPos.y;
        if (!PlayerInputTest.GetChargeMode() && ChargeFlg == false)
        {
            ChargeFlg = true;
            //MouseMove.x = TPSCameraTargetMove.MouseMove.x + 0.5f;
            //MouseMove.y = 0.6f;
        }
        MouseX = MouseMove.x - 0.5f;
        // 座標の更新
        
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
    public static void SetFPSSetting(Vector2 MSensi, Vector2 CSensi, float Dead)
    {
        FPSMouseSensi = MSensi;
        LeftstickSensi = CSensi;
        DeadZone = Dead;
    }
}
