using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TPSCameraTargetMove : MonoBehaviour
{
    [SerializeField] public Transform PlayerTransform;
    [SerializeField] private float TPSCameraDistance;
    [SerializeField] public static Vector2 TPSMouseSensi;
    [SerializeField] private Vector2 TPSMouseMove;
    public static Vector2 MouseMove = Vector2.zero;
    private Vector3 pos = Vector3.zero;
    private Vector3 nowPos;
    private static float MouseX;
    [Header("�R���g���[���[���x")]
    [SerializeField] public static Vector2 RightStickSensi;
    [Header("�R���g���[���[�f�b�h�]�[��")]
    [SerializeField] public static float RightStickDeadZone;
    [Header("�f�o�b�O�p")]
    [SerializeField] private Vector2 RightStick;
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
        if(PlayerInputTest.GetChargeMode())
        {
            ChargeFlg = false;
        }
        if (!PlayerRotation.GetControllerUse())
        {
            MouseMove -= new Vector2(-Input.GetAxis("Mouse X") * TPSMouseSensi.x, Input.GetAxis("Mouse Y")) * Time.deltaTime * TPSMouseSensi.y ;

            WheelAxis = -Input.GetAxis("Mouse ScrollWheel");
            TPSCameraDistance += WheelAxis;
        }
        else
        {
            RightStick = Gamepad.current.rightStick.ReadValue();
            if (Mathf.Abs(RightStick.x) < RightStickDeadZone)
            {
                RightStick.x = 0;
            }
            if (Mathf.Abs(RightStick.y) < RightStickDeadZone)
            {
                RightStick.y = 0;
            }
            MouseMove -= new Vector2(-RightStick.x * RightStickSensi.x * Time.deltaTime, RightStick.y * RightStickSensi.y * Time.deltaTime);
            if(Gamepad.current.dpad.ReadValue().y<-RightStickDeadZone)
            {
                TPSCameraDistance += 0.1f;
            }
            if (Gamepad.current.dpad.ReadValue().y>RightStickDeadZone)
            {
                TPSCameraDistance -= 0.1f;
            }

        }
        TPSCameraDistance = Mathf.Clamp(TPSCameraDistance, 1.0f, 5);
        MouseMove.y = Mathf.Clamp(MouseMove.y, -0.4f + 0.5f, 0.4f + 0.5f);
        //MouseMove += new Vector2(Input.GetAxis("Mouse X")*FPSMouseSensi, Input.GetAxis("Mouse Y")*FPSMouseSensi);
        // ���ʍ��W�n�ϊ�
        pos.x = TPSCameraDistance * Mathf.Sin(MouseMove.y * Mathf.PI) * Mathf.Cos(MouseMove.x * Mathf.PI);
        pos.y = -TPSCameraDistance * Mathf.Cos(MouseMove.y * Mathf.PI);
        pos.z = -TPSCameraDistance * Mathf.Sin(MouseMove.y * Mathf.PI) * Mathf.Sin(MouseMove.x * Mathf.PI);
        //pos *= nowPos.z;

        //pos.y += nowPos.y;
        if (!PlayerInputTest.GetChargeMode() && ChargeFlg == false)
        {
            ChargeFlg = true;
            MouseMove.x = FPSCameraTarget2.MouseMove.x;
            MouseMove.y = 1.2f- FPSCameraTarget2.MouseMove.y;

        }
        MouseX = MouseMove.x - 0.5f;
        // ���W�̍X�V
        transform.position = pos + PlayerTransform.position;
        // transform.LookAt(PlayerTransform.position);
        TPSMouseMove = MouseMove;
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
    public static void SetTPSSetting(Vector2 MSensi, Vector2 CSensi, float Dead)
    {
        TPSMouseSensi = MSensi;
        RightStickSensi = CSensi;
        RightStickDeadZone = Dead;
    }
}
