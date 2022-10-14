using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArrow : MonoBehaviour
{
    #region variable
    private Rigidbody rb;           // ��̍���
    private float arrowForce = 0.0f;     // ������
    private GameObject objBow;
    private int nArrowNum;          // ���Ԗڂ̖
    private int nArrowAtk;          // �U����
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
        nArrowNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    * @brief ��𔭎˂���
    * @param chargeTime �`���[�W���ꂽ����
    * @param nAtk �U����
    * @sa CBow::Update()
    */
    #region shoot
    public void Shot(int chargeTime, int nAtk)
    {
        rb.useGravity = true;
        arrowForce = chargeTime * fFlyDistance;
        nArrowAtk = nAtk;
        Vector3 direction = -transform.up;
        rb.AddForce(direction * arrowForce, ForceMode.Impulse);        // ��𔭎˂���
        //Debug.Log("arrowForce" + arrowForce);
    }
    #endregion

    #region collision
    private void OnCollisionEnter(Collision collision)
    {
        // �n�ʂɏՓ˂����������ł�����
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
    #endregion
    /*
    * @brief ���Ԗڂ̖�ǂ�����ݒ肷��
    * @param num �w�肷��ԍ�
    * @sa �����������
    */
    #region set num
    public void setNum(int index)
    {
        nArrowNum = index;
    }
    #endregion

    /*
     * @brief �U���͂�`����
     * @return int �U����
     * @sa �����������
   */
    #region get arrow atk
    public int GetArrowAtk()
    {
        return nArrowAtk;
    }
    #endregion

}
