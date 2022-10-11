using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMouseMoveX : MonoBehaviour
{
    private bool b_Charge = false;
    [SerializeField]
    private float FPSSensi = 1.0f;
    private float MouseMoveX = 0.0f;
    //private float MouseMoveY = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

        

        if (Input.GetMouseButtonDown(0))    //マウスの左クリックが押された
        {
            
            b_Charge = true;
            MouseMoveX = this.transform.eulerAngles.y;
        }
        if (Input.GetMouseButtonUp(0))  //マウスの左クリックが外れたとき
        {
            
            b_Charge = false;
            
        }
        if (Input.GetMouseButtonDown(1) && b_Charge)    //マウスの右クリックが押された
        {
            
            b_Charge = false;
            
        }
        MouseMoveX += Input.GetAxis("Mouse X") * FPSSensi;
        //MouseMoveY += Input.GetAxis("Mouse Y") * FPSSensi;
        if (b_Charge)
        {
            this.transform.eulerAngles = new Vector3( this.transform.eulerAngles.x,MouseMoveX, this.transform.eulerAngles.z);
        }
       
    }
}
