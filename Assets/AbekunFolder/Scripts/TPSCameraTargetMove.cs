using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TPSCameraTargetMove : MonoBehaviour
{
    [SerializeField] public Transform PlayerTransform;
    [SerializeField] private float TPSCameraDistance;
    [SerializeField] private float FPSMouseSensi;
    public Vector2 MouseMove = Vector2.zero;
    private Vector3 pos = Vector3.zero;
    private Vector3 nowPos;
    private static float MouseX;
    // Start is called before the first frame update
    void Start()
    {
        nowPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
            MouseMove -= new Vector2(-Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * Time.deltaTime * FPSMouseSensi;

        
        MouseMove.y = Mathf.Clamp(MouseMove.y, -0.4f + 0.5f, 0.4f + 0.5f);
        //MouseMove += new Vector2(Input.GetAxis("Mouse X")*FPSMouseSensi, Input.GetAxis("Mouse Y")*FPSMouseSensi);
        // 球面座標系変換
        pos.x = TPSCameraDistance * Mathf.Sin(MouseMove.y * Mathf.PI) * Mathf.Cos(MouseMove.x * Mathf.PI);
        pos.y = -TPSCameraDistance * Mathf.Cos(MouseMove.y * Mathf.PI);
        pos.z = -TPSCameraDistance * Mathf.Sin(MouseMove.y * Mathf.PI) * Mathf.Sin(MouseMove.x * Mathf.PI);
        //pos *= nowPos.z;

        //pos.y += nowPos.y;
        if(Input.GetMouseButtonUp(0))
        {
            
            MouseMove.x=FPSMouseMoveX.GetMouseMoveX()+0.5f;
            MouseMove.y = 0.6f;
        }
        MouseX = MouseMove.x-0.5f;
        // 座標の更新
        transform.position = pos + PlayerTransform.position;
       // transform.LookAt(PlayerTransform.position);
    }
    public static float GetMouseX()
    {
        return MouseX;
    }
}
