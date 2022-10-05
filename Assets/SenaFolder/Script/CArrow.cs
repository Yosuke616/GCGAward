using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArrow : MonoBehaviour
{
    #region variable
    private Rigidbody rb;           // ��̍���
    private Vector3 arrowForce = new Vector3(0.0f,0.0f,0.0f);     // ������
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
        arrowForce.z = chargeTime * 1000.0f;
        rb.AddForce(arrowForce);        // ��𔭎˂���
        Debug.Log("arrowForce" + arrowForce.z);
    }
    #endregion
}
