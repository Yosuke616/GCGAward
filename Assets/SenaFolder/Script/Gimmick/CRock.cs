using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRock : MonoBehaviour
{
    [Header("�΍U���̂��ߎ���")]
    [SerializeField] private float fWaitTime;
    [Header("�΂̔�ԃX�s�[�h"),Range(1.0f,100.0f)]
    [SerializeField] private float fSpeed;
    [Header("��Ԋp�x")]
    [SerializeField] private Vector3 direction;
    //[Header("")]

    private Rigidbody rb;           // ��̍���

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        StartCoroutine("RockShoot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �Δ���
    private IEnumerator RockShoot()
    {
        yield return new WaitForSeconds(fWaitTime);
        rb.useGravity = true;
        //Vector3 direction = new Vector3(-1.0f, 1.0f, 0f);
        rb.AddForce(direction * fSpeed, ForceMode.Impulse);        // ��𔭎˂���
    }
}
