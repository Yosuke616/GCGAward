using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDamageUI : MonoBehaviour
{
    #region serialize field
    [Header("�_���[�W��UI�̐F")]
    [SerializeField] private Color textColor;
    [SerializeField] private GameObject objDamageUI;
    #endregion

    #region valiable
    private int nShowNum = 0;           // �\�����鐔�l
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * @brief �_���[�W���󂯂��ʒm���󂯎��
     * @param DamageNum �󂯂��_���[�W��
     * @sa �I�u�W�F�N�g���_���[�W���󂯂���
     * @details �_���[�W���󂯂��I�u�W�F�N�g����_���[�W�ʂ��󂯎��
    */
    #region tell damaged
    public void TellDamaged(int DamageNum)
    {
        nShowNum = DamageNum;
        Instantiate(objDamageUI);
    }
    #endregion
}
