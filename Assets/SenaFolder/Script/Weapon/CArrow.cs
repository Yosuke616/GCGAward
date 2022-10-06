using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArrow : MonoBehaviour
{
    #region variable
    private Rigidbody rb;           // ��̍���
    private float arrowForce = 0.0f;     // ������
    private GameObject objBow;
    private CBow scBow;             // �|�̃X�N���v�g
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // �|�̃I�u�W�F�N�g���擾
        objBow = GameObject.FindWithTag("Weapon");
        scBow = objBow.GetComponent<CBow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    * @brief ��𔭎˂���
    * @param chargeTime �`���[�W���ꂽ����
    * @sa CBow::Update()
    */
    #region shoot
    public void Shot(int chargeTime)
    {
        rb.useGravity = true;
        arrowForce = chargeTime * 10.0f;
        Vector3 direction = -transform.up;
        rb.AddForce(direction * arrowForce, ForceMode.Impulse);        // ��𔭎˂���
        Debug.Log("arrowForce" + arrowForce);
    }
    #endregion
}
