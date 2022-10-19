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
    private int nOldStep;
    #endregion

    #region serialize field
    [SerializeField] private float fFlyDistance;        // ��̔򋗗�
    [SerializeField] private GameObject objEffSide;     // �O���̃G�t�F�N�g�I�u�W�F�N�g
    [SerializeField] private GameObject objEffTop;      // ��[�̃G�t�F�N�g�I�u�W�F�N�g
    [Header("��̃G�t�F�N�g(side)1�i�K�ڂ��珇��")]
    [SerializeField] private EffekseerEffectAsset[] effSide;
    [Header("��̃G�t�F�N�g(top)1�i�K�ڂ��珇��")]
    [SerializeField] private EffekseerEffectAsset[] effTop;
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
        int nStep = objBow.GetComponent<CBow>().GetStep();      // �|�̃`���[�W�i�K�����擾����

        // �i�K���ς���Ă�����G�t�F�N�g�̐F��ύX����
        if (nStep != nOldStep)
        {
            ChangeEffectColor(objEffSide, effSide, nStep);      // �O��
            ChangeEffectColor(objEffTop, effTop, nStep);        // ��[
        }

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
    private void ChangeEffectColor(GameObject objEff, EffekseerEffectAsset[] effect, int num)
    {
        Vector3 pos = objEff.transform.position;
        objEff.GetComponent<EffekseerEmitter>().effectAsset = effect[num];
    }
    #endregion 

}
