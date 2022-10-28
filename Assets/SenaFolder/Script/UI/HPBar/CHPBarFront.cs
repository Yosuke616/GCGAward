using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHPBarFront : MonoBehaviour
{
    #region variable
    private int nMaxHp;               // �v���C���[�̍ő�HP
    private GameObject objParent;           // �v���C���[�̃I�u�W�F�N�g
    private Slider slider;
    private bool isMove;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        objParent = transform.root.gameObject;
        if (objParent.name == "Canvas")
        {
            objParent = GameObject.FindWithTag("Player");
        }
        nMaxHp = objParent.GetComponent<CCharactorManager>().nMaxHp;  // �v���C���[�̍ő�HP���擾����

        // �X���C�_�[�̒l�̐ݒ�
        slider = GetComponent<Slider>();
        slider.maxValue = nMaxHp;         // �X���C�_�[�̍ő�l
        slider.value = nMaxHp;            // �X���C�_�[�̌��ݒl
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMove)
            slider.value = objParent.GetComponent<CCharactorManager>().nCurrentHp;
    }

    /*
     * @brief �o�[�̒l��ύX����
     * @param num �ύX�����
     * @sa ����˂��ꂽ�Ƃ�/�G�ɍU�����󂯂���
�@  */
    #region move bar
    public void MoveBar(int num)
    {
        isMove = true;
        slider.value += num;
    }
    #endregion 

    public void ResetBar()
    {
        isMove = false;
    }


}
