using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effekseer;

public class CArrow : MonoBehaviour
{
    #region variable
    private Rigidbody rb;           // ��̍���
    private float arrowForce = 0.0f;     // ������
    private GameObject objBow;
    private int nArrowNum;          // ���Ԗڂ̖
    private int nArrowAtk;          // �U����
    private int nOldStep = 0;
    #endregion

    #region serialize field
    [SerializeField] private float fFlyDistance;        // ��̔򋗗�
    [Header("�ύX����G�t�F�N�g�̍Đ��I�u�W�F�N�g")]
    [SerializeField] private GameObject[] objEff;       // �G�t�F�N�g�̍Đ��I�u�W�F�N�g
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // �|�̃I�u�W�F�N�g���擾
        objBow = GameObject.FindWithTag("Weapon");
        nArrowNum = 0;
        //ChangeEffectColor(objEffSide, effSide, 2);
    }

    // Update is called once per frame
    void Update()
    {
        int nStep = objBow.GetComponent<CBow>().GetChargeStep();      // �|�̃`���[�W�i�K�����擾����

        // �i�K���ς���Ă�����G�t�F�N�g�̐F��ύX����
        if (nStep != nOldStep)
        {
            // �ύX����S�ẴG�t�F�N�g�̐F��ύX����
            for(int i = 0; i < objEff.Length; ++i)
                ChangeEffectColor(objEff[i], nStep, nOldStep);
        }
        nOldStep = nStep;
        Debug.Log("�擾�����i�K��" + nStep);
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

    /*
    * @brief �G�t�F�N�g�̐F��ύX����
    * @param GameObject �G�t�F�N�g���i�[����I�u�W�F�N�g
    * @param EffekseerEffectAsset[]�@�Đ�����G�t�F�N�g�A�Z�b�g�z��
    * @param int �Đ�����J���[�ԍ�
    * @sa CBow::Update()
    */
    #region change effect color
    private void ChangeEffectColor(GameObject objEff, int newStep, int oldStep)
    {
        EffekseerEmitter ComponentOldEff = objEff.GetComponent<CEffectManager>().GetEmitterEff(oldStep);
        ComponentOldEff.enabled = false;
        EffekseerEmitter ComponentCurEff = objEff.GetComponent<CEffectManager>().GetEmitterEff(newStep);
        ComponentCurEff.enabled = true;
    }
    #endregion 

}
