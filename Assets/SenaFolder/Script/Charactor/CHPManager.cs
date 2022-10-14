using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHPManager : MonoBehaviour
{
    #region serialize field
    [Header("�L�����N�^�[�̍ő�HP")]
    [SerializeField, Range(1, 100)] public int nMaxHp;        // �L�����N�^�[��HP�̍ő�l
    [SerializeField] public GameObject HPFrontBarGroup;
    [SerializeField] public GameObject HPBGBarGroup;
    [Header("HP�o�[�̕�����")]
    [SerializeField] public int nValNum;        // 1�}�X��HP��
    #endregion

    #region variable
    [System.NonSerialized]
    public int nCurrentHp;     // ���݂�HP
    [System.NonSerialized]
    public GameObject[] objFrontHPBar;
    [System.NonSerialized]
    public GameObject[] objBGHPBar;
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

    #region reset hp
    public void resetHP()
    {
        nCurrentHp = nMaxHp;
    }
    #endregion 
}
