using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArrowDecUI : MonoBehaviour
{
    #region serialize field
    [Header("���������̐F")]
    [SerializeField] private Color colorOn;
    #endregion

    #region variable
    private bool isSwitch;      // �����Ă��邩�ǂ���
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        isSwitch = false;       // ����Ȃ��悤�ɂ���
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    * @brief ��Ԃ̍X�V(���t���[�����s�����)
    * @param PLAYERSTATE �v���C���[�̏��
    * @sa CPlayer::Update
    * @details �v���C���[�̏�Ԃ��擾���A��Ԃɍ��킹���X�V���������s����
  �@*/
    #region set switch on
    public void setSwitch(bool flg)
    {
        isSwitch = flg;
    }
    #endregion
}
