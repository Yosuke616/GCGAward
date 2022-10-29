using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CHPText : MonoBehaviour
{
    #region variable
    private TextMeshProUGUI GUIText;
    private int nCurrentNum;
    private int nOldNum;
    private int nMaxNum;
    private GameObject objPlayer;
    #endregion 
    // Start is called before the first frame update
    void Start()
    {
        // プレイヤー情報を取得する
        objPlayer = GameObject.FindWithTag("Player").gameObject;
        nMaxNum = objPlayer.GetComponent<CCharactorManager>().nMaxHp;
        
        // テキスト情報を取得する
        GUIText = GetComponent<TextMeshProUGUI>();

        // テキスト情報を反映させる
        nCurrentNum = nMaxNum;
        nOldNum = nMaxNum;
        GUIText.text = nMaxNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        GUIText.text = objPlayer.GetComponent<CCharactorManager>().nCurrentHp.ToString();
    }
    //public void ChangeHPNum(int num)
    //{
    //    nOldNum = nCurrentNum;
    //    nCurrentNum += num;
    //    if (nCurrentNum > 100)
    //        nCurrentNum = 100;
    //    if (nCurrentNum <= 0)
    //        nCurrentNum = 0;
    //}
}
