using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CCharactorManager : MonoBehaviour
{
    #region serialize field
    [Header("�L�����N�^�[�̍ő�HP")]
    [SerializeField, Range(1, 100)] public int nMaxHp;        // �L�����N�^�[��HP�̍ő�l
    [SerializeField] public GameObject HPFrontBarGroup;
    [SerializeField] public GameObject HPBGBarGroup;
    [Header("HP�o�[�̕�����")]
    [SerializeField] public int nValNum;        // 1�}�X��HP��
    //[Header("�U���͂̃f�t�H���g�l")]
    //[SerializeField] private int nDefAtk;       // �U���͂̏����l
    #endregion

    #region variable
    [System.NonSerialized]
    public int nCurrentHp;     // ���݂�HP
    [System.NonSerialized]
    public GameObject[] objFrontHPBar;
    [System.NonSerialized]
    public GameObject[] objBGHPBar;
    private int nChangeHPBar;       // �ύX����HP�o�[�̔ԍ�(���݂�HP����v�Z����)
    [System.NonSerialized]
    public int nCurrentAtk;                // �U����
    #endregion

    /*
     * @brief HP�̏�����
�@  */
    #region init hp
    public void InitHP()
    {
        nCurrentHp = nMaxHp;
    }
    #endregion

    /*
     * @brief �U���͂̏�����
�@  */
    #region init atk
    public void InitAtk()
    {
        //nCurrentAtk = nDefAtk;
    }
    #endregion 

    #region set hp bar
    public void SetHPBar()
    {
        objFrontHPBar = new GameObject[nValNum];
        objBGHPBar = new GameObject[nValNum];
        //cBGHPBar = HPBGBar.GetComponent<CBGHPBar>();
        for (int num = 0; num < nValNum; ++num)
        {
            objFrontHPBar[num] = HPFrontBarGroup.transform.GetChild(num).gameObject;
            objFrontHPBar[num].GetComponent<CHPBar>().SetHpBarParam(num, nMaxHp / nValNum);
            objBGHPBar[num] = HPBGBarGroup.transform.GetChild(num).gameObject;
            objBGHPBar[num].GetComponent<CHPBar>().SetHpBarParam(num, nMaxHp / nValNum);
        }
    }
    #endregion

    /*
     * @brief �ύX����o�[�̔ԍ��̕ύX
     * @param num �ύX�����
     * @param BarIndex �ύX����o�[�̔ԍ�
     * @sa �|���`���[�W���ꂽ�Ƃ�/�G��HP����������
     * @details ������HP�ɉ�����FrontHPBar��BarIndex�Ԗڂ̐��l��ύX����
�@  */
    #region calc change hp bar num
    public void CalcBarNum()
    {
        // HP�����^���̎��A�ԍ���1����邽�ߒ�������
        if (nCurrentHp == nMaxHp)
            nChangeHPBar = nCurrentHp / (nMaxHp / nValNum) - 1;
        else
            nChangeHPBar = nCurrentHp / (nMaxHp / nValNum);
    }
    #endregion

    /*
     * @brief �O�ʂ�HP�o�[��ύX����
     * @param num �ύX�����
     * @param BarIndex �ύX����o�[�̔ԍ�
     * @sa �|���`���[�W���ꂽ�Ƃ�/�G��HP����������
     * @details ������HP�ɉ�����FrontHPBar��BarIndex�Ԗڂ̐��l��ύX����
�@  */
    #region Add front bar
    public void AddFrontBar(int num)
    {
        float remain = objFrontHPBar[nChangeHPBar].GetComponent<Slider>().value;
        float perHPBar =  nMaxHp / nValNum;
        float ChangeValue = -1 * (float)num / perHPBar;
        // �Y����HP�o�[�̎c��ʂ��ύX����l��菭�Ȃ��ꍇ�AHP�o�[���܂����������s��
        if (remain < ChangeValue)
        {
            // ���݂�HP�o�[��0�ɂ���
            objFrontHPBar[nChangeHPBar].GetComponent<CHPBar>().AddValue(-1 * (int)(remain * perHPBar));
            float dif = ChangeValue - remain;
            // ���炵����Ȃ�������������HP�o�[�Ō��炷
            objFrontHPBar[nChangeHPBar - 1].GetComponent<CHPBar>().AddValue(-1 * (int)(dif * perHPBar));
        }
        else
            // FrontHPBar�̒l�����炷
            objFrontHPBar[nChangeHPBar].GetComponent<CHPBar>().AddValue(num);
    }
    #endregion

    /*
     * @brief �O�ʂ�HP�o�[��ύX����
     * @param num �ύX�����
     * @param BarIndex �ύX����o�[�̔ԍ�
     * @sa �|���`���[�W���ꂽ�Ƃ�/�G��HP����������
     * @details ������HP�ɉ�����FrontHPBar��BarIndex�Ԗڂ̐��l��ύX����
�@  */
    #region dec bg bar
    public void AddBGBar(int nDecHP)
    {
        // HP�����炷
        objBGHPBar[nChangeHPBar].GetComponent<CHPBar>().AddValue(nDecHP);
        nCurrentHp += nDecHP;
    }
    #endregion

    /*
     * @brief HP�̕ύX
     * @param num HP�̉��Z��
     * @sa �_���[�W����������Ƃ� / �񕜂�����
     * @details HP��num�����Z����
   �@*/
    #region change hp
    public void ChangeHp(int num)
    {
        AddFrontBar(num);
        SetHpBarAnim();
        AddBGBar(num);
    }
    #endregion

    #region set hp bar animation
    public void SetHpBarAnim()
    {
        objBGHPBar[nChangeHPBar].GetComponent<CBGHPBar>().changeBarValue();
    }
    #endregion

    /*
     * @brief ���݂̍U���͂̏���n��
     * @return int �U����
     * @sa ���g�̍U���������������ɑ���̕��ŌĂ΂��
   �@*/
    #region get atk
    public int GetAtk()
    {
        return nCurrentAtk;
    }
    #endregion
}
