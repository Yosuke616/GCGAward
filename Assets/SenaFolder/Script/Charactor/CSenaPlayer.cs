using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSenaPlayer : MonoBehaviour
{
    #region serialize field
    [Header("�v���C���[�̍ő�HP")]
    [SerializeField] private int nMaxHp;        // �v���C���[��HP�̍ő�l
    [Header("HP�o�[1�}�X��HP")]
    [SerializeField] private int nValHp;        // 1�}�X��HP��
    [SerializeField] private GameObject prefabHPBar;        // HP�o�[�̃v���n�u
    #endregion

    // �ϐ��錾
    #region variable
    private int nCurrentHp;     // ���݂�HP
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        nCurrentHp = 0;     // HP�̏�����
        //SetHpUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     * @brief HP�o�[�̃Z�b�g
     * @param setNum �ݒu����HP�o�[�̌�
     * @sa CPlayer::Start
     * @details HP�̕�������ݒ肵�A�A������HP�o�[��ݒu����
   �@*/
    #region set hp UI
    private void SetHpUI()
    {
        //for (int num = 0; num < 5; ++num)
        //{
        //    GameObject hpBar = Instantiate(prefabHPBar);
        //    hpBar.GetComponent<CHPBar>().SetHpBarParam(num);
        //    hpBar.transform.SetParent()
        //}
    }
    #endregion
}
