using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArrow : MonoBehaviour
{
    #region variable
    private Rigidbody rb;           // 矢の剛体
    private Vector3 arrowForce = new Vector3(0.0f,0.0f,0.0f);     // 矢を放つ力
    private GameObject objBow;
    private CBow scBow;             // 弓のスクリプト
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // 弓のオブジェクトを取得
        objBow = GameObject.FindWithTag("Weapon");
        scBow = objBow.GetComponent<CBow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    * @brief 矢を発射する
    * @param chargeTime チャージされた時間
    * @sa CBow::Update()
    */
    #region shoot
    public void Shot(int chargeTime)
    {
        rb.useGravity = true;
        arrowForce.z = chargeTime * 1000.0f;
        rb.AddForce(arrowForce);        // 矢を発射する
        Debug.Log("arrowForce" + arrowForce.z);
    }
    #endregion
}
