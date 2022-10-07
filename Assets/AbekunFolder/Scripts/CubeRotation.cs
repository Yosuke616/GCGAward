using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    [SerializeField] GameObject Player;
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
        if (MouseMoveY < 0.0f)
        {
            MouseMoveY = 0.0f;
        }
        if (MouseMoveY >= 360.0f)
        {
            MouseMoveY = 0.0f;
        }
        
        MouseMoveY -= Input.GetAxis("Mouse Y") * Sensi;
        //float sinX = Mathf.Sin(MouseMoveX);
        //float cosX = Mathf.Cos(MouseMoveX);
        float sinY = Mathf.Sin(MouseMoveY);
        float cosY = Mathf.Cos(MouseMoveY);
        this.transform.Rotate(MouseMoveY,0.0f,0.0f);
        
    }
}
