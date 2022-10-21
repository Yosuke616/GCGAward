using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CCharactorManager : MonoBehaviour
{
    // �v���C���[�̏��
    #region plater state
    public enum CHARACTORSTATE
    {
        CHARACTOR_ALIVE = 0,       // �������
        CHARACTOR_DEAD,            // ���S���
    }
    #endregion
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
    public int nCurrentHp;      // ���݂�HP
    [System.NonSerialized]
    public int nOldHp;          // 1�t���[���O��HP
    [System.NonSerialized]
    public GameObject[] objFrontHPBar;
    [System.NonSerialized]
    public GameObject[] objBGHPBar;
    private int nChangeBGHPBar;       // �ύX����HP�o�[�̔ԍ�(���݂�HP����v�Z����)
    private int nChangeFrontBarIndex = 0;       // �ύX����HP�o�[�̔ԍ�(���݂�HP����v�Z����)
    private int nCurrentFrontVal = 0;
    [System.NonSerialized]
    public int nCurrentAtk;                // �U����
    private int nPerVal;                  // �X���C�_�[1������̐��l
    #endregion

    /*
     * @brief HP�̏�����
�@  */
    #region init hp
    public void InitHP()
    {
        nCurrentHp = nMaxHp;
        nOldHp = nCurrentHp;
        nCurrentFrontVal = nMaxHp;
        nPerVal = nMaxHp / nValNum;
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
    #region calc change front bar num
    public virtual void CalcFrontBarNum()
    {
        // HP�����^���̎��A�ԍ���1����邽�ߒ�������
        if (nCurrentHp == nMaxHp)
            nChangeFrontBarIndex = nCurrentHp / (nMaxHp / nValNum) - 1;
        else
            nChangeFrontBarIndex = nCurrentHp / (nMaxHp / nValNum);
    }
    #endregion

    #region calc change bg bar num
    public virtual void CalcBGBarNum()
    {
        // HP�����^���̎��A�ԍ���1����邽�ߒ�������
        if (nCurrentHp == nMaxHp)
            nChangeBGHPBar = nCurrentHp / (nMaxHp / nValNum) - 1;
        else
            nChangeBGHPBar = nCurrentHp / (nMaxHp / nValNum);
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
        // ���݂̃o�[�̒l���擾����
        int barValue = objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().nCurrentValue;
        // �̗͂������Ă���Ƃ����݂�HP�X���C�_�[�̒l�ƌ����ʂ��r���č��������̃X���C�_�[�ɔ��f������
        if (num < 0)
        {
            if(Mathf.Abs(num) > barValue)
            {
                Debug.Log("<color=red>BarIndexChange</color>");
                // ���݂̃o�[�̂��邾���̒l�����炷
                objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().AddValue(-1 * barValue);
                objFrontHPBar[nChangeFrontBarIndex - 1].GetComponent<CHPBar>().AddValue(-1 * (Mathf.Abs(num) - barValue));

            }
            else
                objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().AddValue(num);
        }
        // �̗͂����Z�b�g�����Ƃ����݌��Ă���X���C�_�[�̒l��0�̏ꍇ1�O�̃X���C�_�[�̒l���r����
        else
        {
            if(barValue <= 0)
            {
                Debug.Log("<color=yellow>BarIndexChange</color>");
            }
            else
                objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().AddValue(num);
        }
        // 
    }
    #endregion

    
//#region Add front bar
    //public void AddFrontBar(int num)
    //{
    //    //nCurrentFrontVal += num;
    //    float remain = objFrontHPBar[nChangeFrontBarIndex].GetComponent<Slider>().value;
    //    float perHPBar = nMaxHp / nValNum;
    //    float ChangeValue = Mathf.Abs((float)num / perHPBar);
    //    // �Y����HP�o�[�̎c��ʂ��ύX����l��菭�Ȃ��ꍇ�AHP�o�[���܂����������s��
    //    if (remain < ChangeValue)
    //    {
    //        objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().AddValue(-1 * (int)(remain * perHPBar));
    //        //float dif = ChangeValue - remain;
    //        // ���炵����Ȃ�������������HP�o�[�Ō��炷
    //        if (num < 0)
    //        {
    //            objFrontHPBar[nChangeFrontBarIndex - 1].GetComponent<CHPBar>().AddValue(-1 * (int)(dif * perHPBar));
    //        }
    //        else
    //        {
    //            //nChangeBGHPBar--;
    //            objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().AddValue((int)(dif * perHPBar));
    //        }
    //    }
    //    else
    //        // FrontHPBar�̒l�����炷
    //        objFrontHPBar[nChangeFrontBarIndex].GetComponent<CHPBar>().AddValue(num);
    //}
    //#endregion
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
        objBGHPBar[nChangeBGHPBar].GetComponent<CHPBar>().AddValue(nDecHP);
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
        objBGHPBar[nChangeBGHPBar].GetComponent<CBGHPBar>().changeBarValue();
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
