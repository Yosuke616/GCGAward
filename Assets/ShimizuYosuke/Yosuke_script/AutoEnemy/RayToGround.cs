using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayToGround : MonoBehaviour
{
    //���������Ԗڂ̎ˏo�n�_����F�����Ă������߂̔ԍ�
    private int Number;

    [Header("���C�̋���")]
    [SerializeField] private int Distance = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        //���g�̐^���Ƀ��C���΂�
        this.transform.eulerAngles = new Vector3(90.0f,0,0);
        Vector3 direction = new Vector3(0,0,0);
        Ray ray = new Ray(this.transform.position,direction);
        //int layerMask = ~(1 << 13);
        if (Physics.Raycast(ray, out hit,Distance,0))
        {
            
        }
    }

    //���������Ԗڂ���ݒ肷��֐�
    public void SetNumber(int number) {
        Number = number;
    }

}
