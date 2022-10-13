using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CArrowDecUI : MonoBehaviour
{
    #region serialize field
    [Header("���������̐F")]
    [SerializeField] private Color colorOn;
    [Header("�ʏ펞�̐F")]
    [SerializeField] private Color colorOff;
    #endregion

    #region variable
    private bool isSwitch;      // �����Ă��邩�ǂ���
    private Image texture;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        isSwitch = false;       // ����Ȃ��悤�ɂ���
        // �e�N�X�`�������擾����
        texture = GetComponent<Image>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    * @brief �X�C�b�`�̐ؑ�
    * @param bool �ݒ肵����bool�ϐ�
    * @sa ��̈З͂�ύX���Ă���Ƃ�
    * @details UI�̃X�C�b�`��ON/OFF��؂�ւ���
  �@*/
    #region set switch
    public void setSwitch(bool flg)
    {
        isSwitch = flg;
        changeTex(flg);
    }
    #endregion

    /*
    * @brief �e�N�X�`����ύX����
    * @param bool �X�C�b�`�̏��
    * @sa CArrowDecUI::setSwitch
    * @details �X�C�b�`���؂�ւ�������Ƀe�N�X�`����؂�ւ���
  �@*/
    #region change texture
    private void changeTex(bool flg)
    {
        switch (flg)
        {
            case true:
                texture.color = colorOn;
                break;
            case false:
                texture.color = colorOff;
                break;
        }

    }
    #endregion
}
