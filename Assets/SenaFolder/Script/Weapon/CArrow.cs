using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effekseer;

public class CArrow : MonoBehaviour
{
    #region variable
    private Rigidbody rb;           // 矢の剛体
    private float arrowForce = 0.0f;     // 矢を放つ力
    private GameObject objBow;
    private int nArrowNum;          // 何番目の矢か
    private int nArrowAtk;          // 攻撃力
    private int nOldStep;
    #endregion

    #region serialize field
    [SerializeField] private float fFlyDistance;        // 矢の飛距離
    [SerializeField] private GameObject objEffSide;     // 外側のエフェクトオブジェクト
    [SerializeField] private GameObject objEffTop;      // 先端のエフェクトオブジェクト
    [Header("矢のエフェクト(side)1段階目から順に")]
    [SerializeField] private EffekseerEffectAsset[] effSide;
    [Header("矢のエフェクト(top)1段階目から順に")]
    [SerializeField] private EffekseerEffectAsset[] effTop;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // 弓のオブジェクトを取得
        objBow = GameObject.FindWithTag("Weapon");
        nArrowNum = 0;
        //ChangeEffectColor(objEffSide, effSide, 2);
    }

    // Update is called once per frame
    void Update()
    {
        int nStep = objBow.GetComponent<CBow>().GetStep();      // 弓のチャージ段階数を取得する

        // 段階が変わっていたらエフェクトの色を変更する
        if (nStep != nOldStep)
        {
            ChangeEffectColor(objEffSide, effSide, nStep);      // 外側
            ChangeEffectColor(objEffTop, effTop, nStep);        // 先端
        }

    }
    /*
    * @brief 矢を発射する
    * @param chargeTime チャージされた時間
    * @param nAtk 攻撃力
    * @sa CBow::Update()
    */
    #region shoot
    public void Shot(int chargeTime, int nAtk)
    {
        rb.useGravity = true;
        arrowForce = chargeTime * fFlyDistance;
        nArrowAtk = nAtk;
        Vector3 direction = -transform.up;
        rb.AddForce(direction * arrowForce, ForceMode.Impulse);        // 矢を発射する
        //Debug.Log("arrowForce" + arrowForce);
    }
    #endregion

    #region collision
    private void OnCollisionEnter(Collision collision)
    {
        // 地面に衝突したら矢を消滅させる
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
    #endregion
    /*
    * @brief 何番目の矢かどうかを設定する
    * @param num 指定する番号
    * @sa 矢を撃った時
    */
    #region set num
    public void setNum(int index)
    {
        nArrowNum = index;
    }
    #endregion

    /*
     * @brief 攻撃力を伝える
     * @return int 攻撃力
     * @sa 矢が当たった時
   */
    #region get arrow atk
    public int GetArrowAtk()
    {
        return nArrowAtk;
    }
    #endregion

    /*
    * @brief エフェクトの色を変更する
    * @param GameObject エフェクトを格納するオブジェクト
    * @param EffekseerEffectAsset[]　再生するエフェクトアセット配列
    * @param int 再生するカラー番号
    * @sa CBow::Update()
    */
    #region change effect color
    private void ChangeEffectColor(GameObject objEff, EffekseerEffectAsset[] effect, int num)
    {
        Vector3 pos = objEff.transform.position;
        objEff.GetComponent<EffekseerEmitter>().effectAsset = effect[num];
    }
    #endregion 

}
