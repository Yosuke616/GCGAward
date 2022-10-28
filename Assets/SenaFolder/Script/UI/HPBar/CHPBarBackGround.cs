using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHPBarBackGround : MonoBehaviour
{
    #region serialize field
    [Header("���o�J�n����")]
    [SerializeField] private float fStartTime;
    [Header("1�b�Ɍ���HP�̗�"),Range(1, 10)]
    [SerializeField] private float fValuePerSec;
    #endregion

    #region variable
    private float fPerChangeValue;          // 1�t���[���ŕύX�����
    private float fChangeValue;             // �ύX�����
    private int nMaxHp;               // �v���C���[�̍ő�HP
    private GameObject objParent;           // �v���C���[�̃I�u�W�F�N�g
    private Slider slider;
    private bool isMove;                    // �X���C�_�[�������Ă��邩�ǂ���
    private bool isValueDec;                // ���l�����炷�̂����₷�̂�
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        // �v���C���[�̏��̎擾
        objParent = transform.root.gameObject;
        if(objParent.name == "Canvas")
        {
            objParent = GameObject.FindWithTag("Player");
        }

        nMaxHp = objParent.GetComponent<CCharactorManager>().nMaxHp;  // �v���C���[�̍ő�HP���擾����

        // �X���C�_�[�̒l�̐ݒ�
        slider = GetComponent<Slider>();
        slider.maxValue = nMaxHp;         // �X���C�_�[�̍ő�l
        slider.value = nMaxHp;            // �X���C�_�[�̌��ݒl

        // �t���O�̏�����
        isMove = false;
        fPerChangeValue = 60.0f / 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isMove);
        if (isMove)
        {
            // �l�����炷�Ƃ�
            if (isValueDec)
            {
                slider.value -= fPerChangeValue * Time.deltaTime;
                if (slider.value <= fChangeValue)
                    isMove = false;
            }
            // �l�𑝂₷�Ƃ�
            else 
            {
                slider.value += fPerChangeValue * Time.deltaTime;
                if (slider.value >= fChangeValue)
                    isMove = false;
            }
        }
        else
        {
            slider.value = objParent.GetComponent<CCharactorManager>().nCurrentHp;
        }
        
    }

    /*
     * @brief �o�[�̒l��ύX����
     * @param num �ύX�����
     * @sa ����˂��ꂽ�Ƃ�/�G�ɍU�����󂯂���
�@  */
    #region move bar
    public void MoveBar(int num)
    {
        // �v���C���[��HP�����炷�ꍇ
        //if (num < 0)
        // �X���C�_�[�������Ă���Œ��ł���΁A�ŏI�̒l�ɕύX����
        if(isMove)
        {
            slider.value = fChangeValue;
        }
        isValueDec = num < 0;
        // �ύX��̒l���i�[����
        fChangeValue = slider.value + num;
        //Debug.Log("�o�[�������܂�");
        StartCoroutine("setSliderMove");
    }
    #endregion 

    private IEnumerator setSliderMove()
    {
        // ���o�J�n���Ԃ��o�߂�����A�X���C�_�[�𓮂���
        yield return new WaitForSeconds(fStartTime);
        isMove = true;      // �X���C�_�[�𓮂���
    }
}
