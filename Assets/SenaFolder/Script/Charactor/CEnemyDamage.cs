using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyDamage : MonoBehaviour
{
    #region serialize field
    [Header("ヒットカーソルの描画時間")]
    [SerializeField] private float fLifeTime;
    #endregion

    #region variable
    private GameObject objCursurUI;        // カーソルUI
    private GameObject objHitCursur;       // ヒットカーソル
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        objCursurUI = GameObject.FindWithTag("Cursur");
        objHitCursur = objCursurUI.transform.Find("HitCursur").gameObject;
        objHitCursur.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    /*
    * @brief 敵からの矢の衝突通知受け取り
    * @sa 敵に矢が当たった時
  　*/
    public void ArrowHit()
    {
        objHitCursur.SetActive(true);
        StartCoroutine("DisabledHitCursur", objHitCursur);
    }

    private IEnumerator DisabledHitCursur(GameObject cursur)
    {
        yield return new WaitForSeconds(fLifeTime);
        cursur.SetActive(false);
    }
}
