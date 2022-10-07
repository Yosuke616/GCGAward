using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCameraTargetY : MonoBehaviour
{
    [SerializeField] GameObject Player;

    [SerializeField] GameObject CameraTargetX;
    [Header("íçéãì_")]

    [SerializeField] private float CameraRange = 5.0f;

    [Header("éãì_à⁄ìÆä¥ìx")]
    [SerializeField] private float Sensi = 1.0f;

    [SerializeField]
    private float MouseMoveY = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //Vector3 Pos = new Vector3(0.0f, 0.0f, CameraRange);
        //this.transform.position = Pos;
    }

    // Update is called once per frame
    void Update()
    {
        if(MouseMoveY<0.1f)
        {
            MouseMoveY = 0.1f;
        }
        if (MouseMoveY >= 3.14f)
        {
            MouseMoveY = 3.13f;
        }

        MouseMoveY -= Input.GetAxis("Mouse Y") * Sensi;
        //float sinX = Mathf.Sin(MouseMoveX);
        //float cosX = Mathf.Cos(MouseMoveX);
        float sinY = Mathf.Sin(MouseMoveY);
        float cosY = Mathf.Cos(MouseMoveY);
        
        this.transform.position = new Vector3(CameraTargetX.transform.position.x, Player.transform.position.y + 1.0f+CameraRange*cosY, Player.transform.position.z + CameraRange * sinY);

    }
}
