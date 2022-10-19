using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class CDamageUI : MonoBehaviour
{
    #region serialize field
    [Header("ダメージ数UIの色")]
    [SerializeField] private Color textColor;
    [SerializeField] private GameObject objDamageUI;
    [Header("UI表示時間")]
    [SerializeField] public float fLifeTime;     // UIを表示させる時間
    #endregion

    #region valiable
    private int nShowNum = 0;           // 表示する数値
    #endregion

    /*
     * @brief ダメージを受けた通知を受け取る
     * @param DamageNum 受けたダメージ数
     * @sa オブジェクトがダメージを受けた時
     * @details ダメージを受けたオブジェクトからダメージ量を受け取る
    */
    #region tell damaged
    public void TellDamaged(int DamageNum)
    {
        nShowNum = DamageNum;
        GameObject obj = Instantiate(objDamageUI);
        //obj.transform.parent = this.transform;
        //GameObject test = obj.transform.GetChild(0).;
        obj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = DamageNum.ToString();
        //StartCoroutine("DestroyUI",(obj, fLifeTime));
        StartCoroutine("DestroyUI",obj);
    }
    #endregion

    /*
     * @brief UIを削除する
     * @details UI表示時間を過ぎたらUIを削除する
    */
    #region destroy ui
    public IEnumerator DestroyUI(GameObject target)
    {
        yield return new WaitForSeconds(fLifeTime);
        Destroy(target);
    }
    #endregion
}
