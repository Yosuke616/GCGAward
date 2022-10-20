using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayToGround : MonoBehaviour
{
    //���������Ԗڂ̎ˏo�n�_����F�����Ă������߂̔ԍ�
    private int Number;

    [Header("���C�̋���")]
    [SerializeField] private int Distance = 100;

    private GameObject CreateEnemy;

    // Start is called before the first frame update
    void Start()
    {
        CreateEnemy = GameObject.Find("Enemy");

        RaycastHit hit;


        //���g�̐^���Ƀ��C���΂�
        this.transform.eulerAngles = new Vector3(90.0f, 0, 0);
        Vector3 direction = new Vector3(0, -1, 0);
        Ray ray = new Ray(this.transform.position, direction);
        //int layerMask = ~(1 << 13);
        if (Physics.Raycast(ray, out hit, Distance))
        {
            //���ɓG�I�u�W�F�N�g�����ݏo����Ă����琶�ݏo���Ȃ�
            if (hit.collider.CompareTag("Enemy")) {
                Debug.Log(23456786543);
            }

            //�I�u�W�F�N�g�����o����
            GameObject Enemy = Instantiate(CreateEnemy, hit.point, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //���������Ԗڂ���ݒ肷��֐�
    public void SetNumber(int number) {
        Number = number;
    }

}
