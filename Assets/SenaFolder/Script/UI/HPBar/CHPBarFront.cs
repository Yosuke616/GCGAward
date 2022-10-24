using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHPBarFront : MonoBehaviour
{
    #region variable
    private int nPlayerMaxHp;               // �v���C���[�̍ő�HP
    private GameObject objPlayer;           // �v���C���[�̃I�u�W�F�N�g
    private Slider slider;
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
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*
     * @brief �o�[�̒l��ύX����
     * @param num �ύX�����
     * @sa ����˂��ꂽ�Ƃ�/�G�ɍU�����󂯂���
�@  */
    #region move bar
    public void MoveBar(int num)
    {
        slider.value += num;
    }
    #endregion 

    
}
