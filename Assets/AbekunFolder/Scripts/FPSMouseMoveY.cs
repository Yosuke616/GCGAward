using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMouseMoveY : MonoBehaviour
{
    // Start is called before the first frame update
    private bool b_Charge = false;
    [SerializeField]
    private float FPSSensi = 1.0f;
    //private float MouseMoveX = 0.0f;
    private float MouseMoveY = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {




        if (Input.GetMouseButtonDown(0))    //�}�E�X�̍��N���b�N�������ꂽ
        {

            b_Charge = true;
        }
        if (Input.GetMouseButtonUp(0))  //�}�E�X�̍��N���b�N���O�ꂽ�Ƃ�
        {

            b_Charge = false;
        }
        if (Input.GetMouseButtonDown(1) && b_Charge)    //�}�E�X�̉E�N���b�N�������ꂽ
        {

            b_Charge = false;
        }
        //MouseMoveX += Input.GetAxis("Mouse X") * FPSSensi;
        MouseMoveY += Input.GetAxis("Mouse Y") * FPSSensi;
        if (b_Charge)
        {
            this.transform.eulerAngles = new Vector3(-MouseMoveY, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        }
        if (!b_Charge)
        {

            if (MouseMoveY <= 0)
            {
                MouseMoveY++;
            }
            else if (MouseMoveY >= 0)
            {
                MouseMoveY--;
            }
            this.transform.eulerAngles = new Vector3(-MouseMoveY, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        }
    }
}
