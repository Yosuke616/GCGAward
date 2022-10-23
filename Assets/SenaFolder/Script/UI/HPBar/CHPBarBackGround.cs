using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHPBarBackGround : MonoBehaviour
{
    #region serialize field
    [Header("���o�J�n����")]
    [SerializeField] private float fStartTime;
    [Header("���o�p������"),Range(0.01f, 2.0f)]
    [SerializeField] private float fStagingTime;
    #endregion

    #region variable
    private float fPerChangeValue;          // 1�t���[���ŕύX�����
    private float fChangeValue;             // �ύX�����
    private int nPlayerMaxHp;               // �v���C���[�̍ő�HP
    private GameObject objPlayer;           // �v���C���[�̃I�u�W�F�N�g
    private Slider slider;
    private bool isMove;                    // �X���C�_�[�������Ă��邩�ǂ���
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        // �v���C���[�̏��̎擾
        objPlayer = GameObject.FindWithTag("Player").gameObject;        // �v���C���[�̃I�u�W�F�N�g���擾����
        nPlayerMaxHp = objPlayer.GetComponent<CCharactorManager>().nMaxHp;  // �v���C���[�̍ő�HP���擾����

        // �X���C�_�[�̒l�̐ݒ�
        slider = GetComponent<Slider>();
        slider.maxValue = nPlayerMaxHp;         // �X���C�_�[�̍ő�l
        slider.value = nPlayerMaxHp;            // �X���C�_�[�̌��ݒl

        // �t���O�̏�����
        isMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            slider.value -= fPerChangeValue;

            if (slider.value <= fChangeValue)
                isMove = false;
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
        // �ύX��̒l���i�[����
        fChangeValue = slider.value + num;
        // 1�t���[���ŕύX����ʂ��v�Z����
        fPerChangeValue = Mathf.Abs(num) / (60.0f * fStagingTime);
        Debug.Log("�o�[�������܂�");
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
