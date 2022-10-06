using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayView : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] int RayLength = 30;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = transform.position;      // �n�_���I�u�W�F�N�g�̒��S���W�ɐݒ�
        Vector3 direction = transform.forward;       // X��������\���x�N�g��
        Ray ray = new Ray(origin, direction);           // Ray�𐶐�
        Debug.DrawRay(ray.origin, ray.direction * RayLength, Color.red);    
    }
}
