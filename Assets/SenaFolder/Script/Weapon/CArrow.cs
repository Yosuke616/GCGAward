using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArrow : MonoBehaviour
{
    #region variable
    private Rigidbody rb;           // ��̍���
    private float arrowForce = 0.0f;     // ������
    private GameObject objBow;
    #endregion

    #region serialize field
    [SerializeField] private float fFlyDistance;        // ��̔򋗗�
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // �|�̃I�u�W�F�N�g���擾
        objBow = GameObject.FindWithTag("Weapon");
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
        arrowForce = chargeTime * fFlyDistance;
        Vector3 direction = -transform.up;
        rb.AddForce(direction * arrowForce, ForceMode.Impulse);        // ��𔭎˂���
        //Debug.Log("arrowForce" + arrowForce);
    }
    #endregion
}
